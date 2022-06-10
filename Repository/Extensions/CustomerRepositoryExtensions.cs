using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
