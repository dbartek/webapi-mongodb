using SimpleWebApi.Core.Models.Authorization;
using SimpleWebApi.Core.Repositories.Users;
using SimpleWebApi.Web.Utilities;
using FluentValidation;
using System.Web;
using FluentValidation.Results;
using System.Collections.Generic;
using System;
using SimpleWebApi.Core.Utilities;

namespace SimpleWebApi.Core.Services.Authorization
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly ITokenProvider _tokenProvider;
        private readonly IValidator<LoginRequest> _loginModelValidator;
        private readonly IValidator<RegisterRequest> _registerModelValidator;

        public AuthorizationService(
            IUsersRepository usersRepository,
            ITokenProvider tokenProvider,
            IValidator<LoginRequest> loginModelValidator, 
            IValidator<RegisterRequest> registerModelValidator)
        {
            _usersRepository = usersRepository;
            _tokenProvider = tokenProvider;
            _loginModelValidator = loginModelValidator;
            _registerModelValidator = registerModelValidator;
        }

        public AccessTokenResponse Login(LoginRequest model)
        {
            var validationResult = _loginModelValidator.Validate(model);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var userData = _usersRepository.GetByUsername(model.Username);
            if (userData == null || IsNotAuthorized(model.Password, userData))
            {
                throw new HttpException(401, "Login failed");
            }

            var tokenValue = _tokenProvider.GenerateToken(new AccessTokenData
            {
                UserKey = userData.Id,
                Username = userData.Username
            });
            return new AccessTokenResponse()
            {
                Token = tokenValue,
                Type = "Bearer"
            };
        }

        public bool Register(RegisterRequest model)
        {
            var validationResult = _registerModelValidator.Validate(model);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            EnsureUserNotExist(model);

            var securityTuple = CryptoHelper.HashPassword(model.Password);

            return _usersRepository.Create(new UserEntity
            {
                Username = model.Username,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                PasswordHash = securityTuple.Item1,
                PasswordSalt = securityTuple.Item2
            });
        }

        private void EnsureUserNotExist(RegisterRequest model)
        {
            var existingUser = _usersRepository.GetByUsername(model.Username);
            if (existingUser != null)
            {
                var validationList = new List<ValidationFailure>
                {
                    new ValidationFailure("Username", $"User '{model.Username}' already exist")
                };
                throw new ValidationException(validationList);
            }
        }

        private bool IsNotAuthorized(string password, UserEntity userData)
        {
            return !CryptoHelper.IsAuthorized(password, userData.PasswordSalt, userData.PasswordHash);
        }
    }
}
