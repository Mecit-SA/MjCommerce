﻿using AutoMapper;
using MjCommerce.Shared.Models;

namespace MjCommerce.Shared.MapperProfiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, Product>();
        }
    }
}