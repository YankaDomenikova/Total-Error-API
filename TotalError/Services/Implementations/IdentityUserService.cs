using System;
using System.Collections;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;

using Data;

using Infrastructure.DtoModels;
using Infrastructure.Interfaces;

using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using Models.Entities;

namespace Services.Implementations
{
    public class IdentityUserService : IIdentityUser
    {
        private readonly TokenModel _tokenManagement;
        private ApplicationDbContext dbContext;
        private readonly IMapper mapper;
        private User User;

        public IdentityUserService(ApplicationDbContext data, IOptions<TokenModel> tokenManagement, IMapper mapper)
        {
            this.dbContext = data;
            this.mapper = mapper;
            this._tokenManagement = tokenManagement.Value;
        }
        /// <summary>
        /// Validate current user attempt to login
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private bool IsValidUser(UserVM model)
        {
            var currentUser = this.dbContext.Users
                .SingleOrDefault(x => x.UserName == model.UserName);

            if (currentUser != null)
            {
                var res = this.VerifyHashedPassword(currentUser.PasswordHash, model.Password);
                User = currentUser;

                return res;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Checks if user is already register. Default value false.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        private bool isRegistered(string userName)
        {
            var check = this.dbContext.Users.SingleOrDefault(x => x.UserName == userName);
            if (check != null)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// Generates user token after successful register/login
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private string GenerateUserToken(RequestTokenModel request)
        {
            string token = string.Empty;

            var claim = new List<Claim>()
            {
              new Claim(ClaimTypes.NameIdentifier, request.UserName),

            };


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenManagement.Secret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var jwtToken = new JwtSecurityToken(
                _tokenManagement.Issuer,
                _tokenManagement.Audience,
                claim,
                expires: DateTime.Now.AddMinutes(_tokenManagement.AccessExpiration),
                signingCredentials: credentials
            );

            token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return token;
        }
        /// <summary>
        /// Login user to the system
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<string> Login(UserVM model)
        {
            if (this.IsValidUser(model))
            {
                var token = this.GenerateUserToken(new RequestTokenModel() { UserName = model.UserName });
                if (token.Length > 0)
                {
                    return token;
                }
            }

            return "";
        }
        /// <summary>
        /// Adds new user to Db. If model is not valid return false
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<string> Register(UserVM model)
        {
            if (this.isRegistered(model.UserName) == false)
            {
                User = new User();

                var token = GenerateUserToken(new RequestTokenModel() { UserName = model.UserName });

                User = mapper.Map<User>(model);

                User.PasswordHash = HashPassword(model.Password);

                this.dbContext.Users.Add(User);
                this.dbContext.SaveChanges();

                return token;

            }
            return "";
        }

        private bool VerifyHashedPassword(string hashedPassword, string password)
        {
            byte[] buffer4;
            if (hashedPassword == null)
            {
                return false;
            }
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            byte[] src = Convert.FromBase64String(hashedPassword);
            if ((src.Length != 0x31) || (src[0] != 0))
            {
                return false;
            }
            byte[] dst = new byte[0x10];
            Buffer.BlockCopy(src, 1, dst, 0, 0x10);
            byte[] buffer3 = new byte[0x20];
            Buffer.BlockCopy(src, 0x11, buffer3, 0, 0x20);
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, dst, 0x3e8))
            {
                buffer4 = bytes.GetBytes(0x20);
            }
            return ByteArraysEqual(buffer3, buffer4);
        }

        private static bool ByteArraysEqual(byte[] buffer3, byte[] buffer4)
        {
            var res = StructuralComparisons.StructuralEqualityComparer.Equals(buffer3, buffer4);
            return res;
        }

        private string HashPassword(string password)
        {
            byte[] salt;
            byte[] buffer2;
            if (password == null)
            {
                throw new ArgumentNullException("Password is empty");
            }
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, 0x10, 0x3e8))
            {
                salt = bytes.Salt;
                buffer2 = bytes.GetBytes(0x20);
            }
            byte[] dst = new byte[0x31];
            Buffer.BlockCopy(salt, 0, dst, 1, 0x10);
            Buffer.BlockCopy(buffer2, 0, dst, 0x11, 0x20);
            return Convert.ToBase64String(dst);
        }
    }
}
