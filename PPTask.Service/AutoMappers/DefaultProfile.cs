using AutoMapper;
using PPTask.Entity.DTOs;
using PPTask.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTask.Service.AutoMappers
{
    public class DefaultProfile : Profile
    {
        public DefaultProfile()
        {
            CreateMap<InvoiceDto, Invoice>().ReverseMap();
            CreateMap<InvoiceListDto, Invoice>().ReverseMap();
            CreateMap<InvoicePayDto, Invoice>().ReverseMap();
            CreateMap<InvoiceTypeDto, InvoiceType>().ReverseMap();
            CreateMap<SubscriberDto, Subscriber>().ReverseMap();
            CreateMap<SubscriberListDto, Subscriber>().ReverseMap();
            CreateMap<SubscriberAddDto, Subscriber>().ReverseMap();
            CreateMap<SubscriberDeleteDto, Subscriber>().ReverseMap();
        }
    }
}
