using Entities;
using System.Reflection;
using System.Linq.Dynamic.Core;
using System.Text;
using Repository.Extensions.Utility;

namespace Repository.Extensions
{
    public static class CustomerRepositoryExtensions
    {
        public static IQueryable<Customer> FilterCustomers(this IQueryable<Customer> customers, uint minAge, uint maxAge)
        {
            if (minAge > 0 && maxAge > 0)
                customers = customers.Where(c => c.Age >= minAge && c.Age <= maxAge);
            else if (minAge > 0)
                customers = customers.Where(c => c.Age >= minAge);
            else if (maxAge > 0)
                customers = customers.Where(c => c.Age <= maxAge);

            return customers;
        }

        public static IQueryable<Customer> Search(this IQueryable<Customer> customers, string? searchTerm)
        {
            if (!string.IsNullOrWhiteSpace(searchTerm))
                customers = customers.Where(c => c.Name!.ToLower().Contains(searchTerm.ToLower()));

            return customers;
        }

        public static IQueryable<Customer> Sort(this IQueryable<Customer> customers, string? orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return customers.OrderBy(c => c.Name);

            var orderQuery = OrderQueryBuilder.CreateOrderQuery<Customer>(orderByQueryString);

            if (string.IsNullOrWhiteSpace(orderQuery))
                return customers.OrderBy(c => c.Name);

            return customers.OrderBy(orderQuery);
        }
    }
}
