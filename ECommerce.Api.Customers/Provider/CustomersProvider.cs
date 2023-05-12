using AutoMapper;
using ECommerce.Api.Customers.DB;
using ECommerce.Api.Customers.Interfaces;
using ECommerce.Api.Customers.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Api.Customers.Provider
{
    public class CustomersProvider : ICustomerProvider
    {
        private readonly CustomersDBContext customersDB;
        private readonly IMapper mapper;
        private readonly ILogger<CustomersProvider> logger;

        public CustomersProvider(CustomersDBContext customersDB, IMapper mapper, ILogger<CustomersProvider> logger)
        {
            this.customersDB = customersDB;
            this.mapper = mapper;
            this.logger = logger;

            SeedData();
        }

        private void SeedData()
        {
            if (!customersDB.Customers.Any())
            {
                customersDB.Customers.Add(new DB.Customer { Id = 1, Name = "abc", Address = "abc address" });
                customersDB.Customers.Add(new DB.Customer { Id = 2, Name = "pqr", Address = "pqr address" });
                customersDB.Customers.Add(new DB.Customer { Id = 3, Name = "pri", Address = "pri address" });
                customersDB.Customers.Add(new DB.Customer { Id = 4, Name = "xyz", Address = "xyz address" });

                customersDB.SaveChanges();
            }
        }

        public async Task<(bool isSuccess, IEnumerable<Models.Customer> customers, string errorMessage)> GetCustomersAsync()
        {
            try
            {
                var result = await customersDB.Customers.ToListAsync();
                if (result != null && result.Any())
                {
                    var customers = mapper.Map<IEnumerable<Models.Customer>>(result);
                    return (true, customers, null);
                }
                return (false, null, "not found");
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.Message.ToString());
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool isSuccess, Models.Customer customer, string errorMessage)> GetCustomerAsync(int id)
        {
            try
            {
                var result = await customersDB.Customers.FirstOrDefaultAsync(x => x.Id == id);
                if (result != null)
                {
                    var customer = mapper.Map<Models.Customer>(result);
                    return (true, customer, null);
                }
                return (false, null, "not found");
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.Message.ToString());
                return (false, null, ex.Message);
            }
        }
    }
}
