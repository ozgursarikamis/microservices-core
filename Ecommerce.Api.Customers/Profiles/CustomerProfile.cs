using AutoMapper;
using Ecommerce.Api.Customers.Db;

namespace Ecommerce.Api.Customers.Profiles
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<Customer, Models.Customer>();
        }
    }
}
