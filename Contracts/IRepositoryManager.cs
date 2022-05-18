namespace Contracts
{
    public interface IRepositoryManager
    {
        IRestaurantRepository Restaurant { get; }
        ICustomerRepository Customer { get; }
        IAuthenticationRepository Authentication { get; }
        void Save();
    }
}
