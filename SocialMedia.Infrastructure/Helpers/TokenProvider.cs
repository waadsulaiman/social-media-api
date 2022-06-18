﻿using Microsoft.IdentityModel.Tokens;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Core.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SocialMedia.Infrastructure.Helpers
{
    /// <summary>
    /// A service for token related operations.
    /// </summary>
    public class TokenProvider : ITokenProvider
    {
        #region GenerateToken
        /// <inheritdoc cref="ITokenProvider.GenerateToken(User)"/>
        public Token GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(AppSettings.JwtSecurityKey);
            DateTime expires = DateTime.UtcNow.AddDays(30);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = AppSettings.JwtIssuer,
                Audience = AppSettings.JwtAudience,
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Name, user.UserName),
                }),
                Expires = expires,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return new Token()
            {
                Content = tokenString,
                ExpirationDate = expires,
            };
        }
        #endregion
    }
}
