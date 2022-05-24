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

            //Kullanıcıya ait önceden oluşturulmuş bir token var mı kontrol edilir.
            if (dbContext.Tokens.Count(x => x.UserId == appUser.Id) > 0)
            {
                //İlgili token bilgileri bulunur.
                userTokens = dbContext.Tokens.FirstOrDefault(x => x.UserId == appUser.Id);

                //Expire olmuş ise yeni token oluşturup günceller.
                if (userTokens.ExpireDate <= DateTime.Now)
                {
                    //Create new token
                    tokenInfo = GenerateToken();

                    userTokens.ExpireDate = tokenInfo.ExpireDate;
                    userTokens.Value = tokenInfo.Token;

                    dbContext.Tokens.Update(userTokens);
                }
            }
            else
            {
                //Create new token
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
                //Kullanıcıya ait önceden oluşturulmuş bir token var mı kontrol edilir.
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

        /// <summary>
        /// Yeni token oluşturur.
        /// </summary>
        /// <returns></returns>
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
                    //Claim tanımları yapılır. Burada en önemlisi Id ve emaildir.
                    //Id üzerinden, aktif kullanıcıyı buluyor olacağız.
                    new Claim(ClaimTypes.NameIdentifier, appUser.Id),
                    new Claim(ClaimTypes.Name, appUser.FullName),
                    new Claim(ClaimTypes.Email, appUser.Email)
                }),

                //ExpireDate
                Expires = expireDate,

                //Şifreleme türünü belirtiyoruz: HmacSha256Signature
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
