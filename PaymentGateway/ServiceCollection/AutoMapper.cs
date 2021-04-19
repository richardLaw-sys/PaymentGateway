using AutoMapper;
using BusinessObject.DTO;
using BusinessObject.UIModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentGateway.ServiceCollection
{
    public class AutoMapper
    {
        public static void Configure()
        {
            Mapper.Initialize(config =>
            {
                config.AddProfile<MapperProfile>();
            });
        }
    }
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<PaymentProcessRequestUIModel, PaymentDTO>();
            CreateMap<PaymentDTO, PaymentProcessResponseUIModel>();
        }
    }
}
