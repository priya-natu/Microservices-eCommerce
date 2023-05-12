using AutoMapper;

namespace ECommerce.Api.Customers.Profiles
{
    public class CustomerMapper : AutoMapper.Profile
    {
        public CustomerMapper() {
            CreateMap<DB.Customer, Models.Customer>();
        }
    }
}
