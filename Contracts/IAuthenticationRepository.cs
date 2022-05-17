using Entities;
using Entities.Models;

namespace Contracts
{
    public interface IAuthenticationRepository
    {
        string Authenticate(LoginModel user);
        bool userExists(LoginModel user);
    }
}
