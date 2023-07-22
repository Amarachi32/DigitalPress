using AutoMapper;
using PressCore.Entities;
using PressInfrastructure.DTO.ResponseDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PressInfrastructure.Services
{
    internal class ProductService : Profile
    {
        public ProductService()
        {
                CreateMap<Product, ProductReturnDto>();
        }
    }
}
