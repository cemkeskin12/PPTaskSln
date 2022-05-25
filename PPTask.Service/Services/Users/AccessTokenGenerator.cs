using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PPTask.Data.Context;
using PPTask.Entity.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PPTask.Service.Services.Users
{
    public class AccessTokenGenerator
    {
        private readonly AppDbContext dbContext;
        private readonly IConfiguration config;
        private readonly AppUser appUser;

        public AccessTokenGenerator(AppDbContext dbContext, IConfiguration config, AppUser appUser)
        {
            this.dbContext = dbContext;
            this.config = config;
            this.appUser = appUser;
        }
        public AppUserTokens GetToken()
        {
            AppUserTokens userTokens = null;
            TokenInfo tokenInfo = null;

            if (dbContext.Tokens.Count(x => x.UserId == appUser.Id) > 0)
            {
                userTokens = dbContext.Tokens.FirstOrDefault(x => x.UserId == appUser.Id);

                if (userTokens.ExpireDate <= DateTime.Now)
                {
                    tokenInfo = GenerateToken();

                    userTokens.ExpireDate = tokenInfo.ExpireDate;
                    userTokens.Value = tokenInfo.Token;

                    dbContext.Tokens.Update(userTokens);
                }
            }
            else
            {
                tokenInfo = GenerateToken();

                userTokens = new AppUserTokens();

                userTokens.UserId = appUser.Id;
                userTokens.LoginProvider = "SystemAPI";
                userTokens.Name = appUser.FullName;
                userTokens.ExpireDate = tokenInfo.ExpireDate;
                userTokens.Value = tokenInfo.Token;

                dbContext.Tokens.Add(userTokens);
            }

            dbContext.SaveChangesAsync();

            return userTokens;
        }
        public async Task<bool> DeleteToken()
        {
            bool ret = true;

            try
            {
                if (dbContext.Tokens.Count(x => x.UserId == appUser.Id) > 0)
                {
                    AppUserTokens userTokens = userTokens = dbContext.Tokens.FirstOrDefault(x => x.UserId == appUser.Id);

                    dbContext.Tokens.Remove(userTokens);
                }

                await dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                ret = false;
            }

            return ret;
        }
        private TokenInfo GenerateToken()
        {
            DateTime expireDate = DateTime.Now.AddSeconds(50);
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(config["Application:Secret"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Audience = config["Application:Audience"],
                Issuer = config["Application:Issuer"],
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, appUser.Id),
                    new Claim(ClaimTypes.Name, appUser.FullName),
                    new Claim(ClaimTypes.Email, appUser.Email)
                }),

                Expires = expireDate,

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            TokenInfo tokenInfo = new TokenInfo();

            tokenInfo.Token = tokenString;
            tokenInfo.ExpireDate = expireDate;

            return tokenInfo;
        }
    }
}
