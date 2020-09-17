using AutoMapper;
using Ecommerce.Api.Products.Db;

namespace Ecommerce.Api.Products.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, Models.Product>();
        }
    }
}
