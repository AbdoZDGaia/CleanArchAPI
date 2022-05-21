namespace Contracts
{
    public interface IRepositoryManager
    {
        IRestaurantRepository Restaurant { get; }
        ICustomerRepository Customer { get; }
        IAuthenticationRepository Authentication { get; }
        Task SaveAsync();
    }
}
