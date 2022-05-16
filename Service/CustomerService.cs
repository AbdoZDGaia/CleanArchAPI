using AutoMapper;
using Contracts;
using Service.Contracts;

namespace Service
{
    public class CustomerService : ICustomerService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly ILoggerManager _loggerManager;
        private readonly IMapper _mapper;

        public CustomerService(IRepositoryManager repositoryManager
            , ILoggerManager loggerManager
            , IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _loggerManager = loggerManager;
            _mapper = mapper;
        }
    }
}
