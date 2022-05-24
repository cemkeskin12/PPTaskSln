using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PPTask.Data.Context;
using PPTask.Entity.DTOs;
using PPTask.Entity.Models;
using PPTask.Service.Services.Users;

namespace PPTask.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext dbContext;
        private readonly IConfiguration config;
        private readonly SignInManager<AppUser> signInManager;
        private readonly UserManager<AppUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;


        public UserController(AppDbContext dbContext,
                              IConfiguration config,
                              SignInManager<AppUser> signInManager,
                              UserManager<AppUser> userManager,
                              RoleManager<IdentityRole> roleManager)
        {
            this.dbContext = dbContext;
            this.config = config;
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult> Register([FromBody] RegisterDto registerDto)
        {
            AppUser user = new AppUser();

            user.FullName = registerDto.FullName;
            user.Email = registerDto.Email.Trim();
            user.UserName = registerDto.Email.Trim();

            IdentityResult result = await userManager.CreateAsync(user, registerDto.Password.Trim());

            if (result.Succeeded)
            {
                bool roleExists = await roleManager.RoleExistsAsync(config["Roles:User"]);

                if (!roleExists)
                {
                    IdentityRole role = new IdentityRole(config["Roles:User"]);
                    role.NormalizedName = config["Roles:User"];

                    roleManager.CreateAsync(role).Wait();
                }
                userManager.AddToRoleAsync(user, config["Roles:User"]).Wait();
            }
            return Ok();
        }

        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult> Login([FromBody] LoginDto loginDto)
        {
            AppUser user = await userManager.FindByNameAsync(loginDto.Email);
            if (user == null)
            {
                return Unauthorized();
            }

            Microsoft.AspNetCore.Identity.SignInResult signInResult =
                await signInManager.PasswordSignInAsync(user,loginDto.Password,false,false);


            AppUser applicationUser = dbContext.Users.FirstOrDefault(x => x.Id == user.Id);

            AccessTokenGenerator accessTokenGenerator = new AccessTokenGenerator(dbContext, config, applicationUser);
            AppUserTokens userTokens = accessTokenGenerator.GetToken();
            var token = new TokenInfo
            {
                Token = userTokens.Value,
                ExpireDate = userTokens.ExpireDate
            };
            return Ok(token);
        }
    }
}
