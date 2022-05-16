using Entities.Models;

namespace Service.Contracts
{
    public interface IAuthenticationService
    {
        string Login(LoginModel model);
    }
}
