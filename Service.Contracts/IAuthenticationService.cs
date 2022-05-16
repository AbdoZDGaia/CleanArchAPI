using Shared.Models;

namespace Service.Contracts
{
    public interface IAuthenticationService
    {
        string Login(LoginModel model);
    }
}
