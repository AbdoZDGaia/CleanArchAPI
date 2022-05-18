using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts;

namespace Service
{
    public class AuthenticationService : IAuthenticationService
    {

        private readonly IRepositoryManager _repository;

        public AuthenticationService(IRepositoryManager repository)
        {
            _repository = repository;
        }

        public string Login(LoginModel user)
        {
            if (user is null || user.UserName is null || user.Password is null)
            {
                throw new CredentialsMissingException();
            }

            if (!_repository.Authentication.userExists(user))
            {
                throw new InvalidCredentialsException();
            }

            var tokenString = _repository.Authentication.Authenticate(user);

            return tokenString;
        }
    }
}
