using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _repositoryContext;
        private readonly Lazy<IRestaurantRepository> _restaurantRepository;
        private readonly Lazy<ICustomerRepository> _customerRepository;
        private readonly Lazy<IAuthenticationRepository> _authRepository;

        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
            _restaurantRepository = new Lazy<IRestaurantRepository>(
                () => new RestaurantRepository(_repositoryContext));
            _customerRepository = new Lazy<ICustomerRepository>(
                () => new CustomerRepository(_repositoryContext));
            _authRepository = new Lazy<IAuthenticationRepository>(
                () => new AuthenticationRepository());
        }

        public IRestaurantRepository Restaurant => _restaurantRepository.Value;
        public ICustomerRepository Customer => _customerRepository.Value;
        public IAuthenticationRepository Authentication => _authRepository.Value;

        public async Task SaveAsync() => await _repositoryContext.SaveChangesAsync();
    }
}
