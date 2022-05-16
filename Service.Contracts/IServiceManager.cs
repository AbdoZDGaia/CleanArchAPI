namespace Service.Contracts
{
    public interface IServiceManager
    {
        IRestaurantService RestaurantService { get; }
        ICustomerService CustomerService { get; }
        IAuthenticationService AuthenticationService { get; }
    }
}
