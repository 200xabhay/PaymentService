using AutoMapper;
using PaymentServices.Application.DTO;
using PaymentServices.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentServices.Application.Mapping
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<CreatePaymentDTO, LoanPayment>().ReverseMap();

            CreateMap<LoanPayment, PaymentResponseDTO>().ReverseMap();



        }
    }
}
