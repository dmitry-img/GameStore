﻿using System;
using System.Configuration;
using System.Data.Entity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using GameStore.BLL.DTOs.Auth;
using GameStore.BLL.Exceptions;
using GameStore.BLL.Interfaces;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;
using log4net;
using Microsoft.IdentityModel.Tokens;

namespace GameStore.BLL.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILog _logger;
        private readonly string _jwtSecret = ConfigurationManager.AppSettings["JwtSecret"];

        public AuthService(IUnitOfWork unitOfWork, ILog logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task RegisterAsync(RegistrationDTO registrationDTO)
        {
            var userRole = await _unitOfWork.Roles
                .GetQuery()
                .FirstOrDefaultAsync(r => r.Name == "User");

            if (userRole == null)
            {
                throw new NotFoundException(nameof(userRole), "User");
            }

            var user = new User
            {
                Username = registrationDTO.Username,
                Email = registrationDTO.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(registrationDTO.Password),
                Role = userRole
            };

            _unitOfWork.Users.Create(user);

            await _unitOfWork.SaveAsync();

            _logger.Info($"User({user.Username}) has been registered!");
        }

        public async Task<AuthResponseDTO> LoginAsync(LoginDTO model)
        {
            var user = await _unitOfWork.Users
                .GetQuery()
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Username == model.Username);

            if (user == null || !BCrypt.Net.BCrypt.Verify(model.Password, user.Password))
            {
                throw new BadRequestException("Invalid username or password.");
            }

            var accessToken = GenerateAccessToken(user);
            var refreshToken = GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpirationDate = DateTime.UtcNow.AddDays(1);

            _unitOfWork.Users.Update(user);
            await _unitOfWork.SaveAsync();

            _logger.Info($"User({user.Username}) has been logged in!");

            return new AuthResponseDTO
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }

        public async Task LogoutAsync(string userObjectId)
        {
            var user = await _unitOfWork.Users
                .GetQuery()
                .FirstOrDefaultAsync(u => u.ObjectId == userObjectId);

            if (user == null)
            {
                throw new NotFoundException(nameof(user), userObjectId);
            }

            user.RefreshToken = null;
            user.RefreshTokenExpirationDate = null;

            _unitOfWork.Users.Update(user);

            _logger.Info($"User({user.Username}) has been logged out!");

            await _unitOfWork.SaveAsync();
        }

        public async Task<AuthResponseDTO> RefreshAsync(string refreshToken)
        {
            var user = await _unitOfWork.Users
                .GetQuery()
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);

            if (user == null || user.RefreshTokenExpirationDate < DateTime.UtcNow)
            {
                throw new BadRequestException("Invalid refresh token.");
            }

            var accessToken = GenerateAccessToken(user);
            var newRefreshToken = GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpirationDate = DateTime.UtcNow.AddDays(1);

            _unitOfWork.Users.Update(user);
            await _unitOfWork.SaveAsync();

            return new AuthResponseDTO
            {
                AccessToken = accessToken,
                RefreshToken = newRefreshToken
            };
        }

        private string GenerateAccessToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSecret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.NameIdentifier, user.ObjectId),
                    new Claim(ClaimTypes.Role, user.Role != null ? user.Role.Name : string.Empty),
                }),
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
            }

            return Convert.ToBase64String(randomNumber).Replace('+', '-').Replace('/', '_').TrimEnd('=');
        }
    }
}