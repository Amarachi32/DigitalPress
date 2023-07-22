using AutoMapper;
using Microsoft.Extensions.Configuration;
using PressCore.Entities;
using PressInfrastructure.DTO.ResponseDto;


namespace PressInfrastructure.Helper
{
    public class ProductUrlResolver : IValueResolver<Product, ProductReturnDto, string>
    {
        private readonly IConfiguration _Config;

        public ProductUrlResolver(IConfiguration config)
        {
            _Config = config;
        }

        public string Resolve(Product source, ProductReturnDto destination, string destMember, ResolutionContext context)
        {
            if(!string.IsNullOrEmpty(source.PictureUrl))
            {
                return _Config["ApiUrl"] + source.PictureUrl;
            }
            return null;
        }
    }
}
