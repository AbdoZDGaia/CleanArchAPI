namespace Contracts
{
    public interface IRepositoryManager
    {
        IRestaurantRepository Restaurant { get; }
        ICustomerRepository Customer { get; }
        void Save();
    }
}
