using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;

namespace Rent.Web.App_Start
{
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {
            // RentPayment Mapping
        }

        protected override void Configure()
        {
            Mapper.CreateMap<Entities.RentPayment, Rent.Web.ViewModels.RentPaymentViewModel>();
            Mapper.CreateMap<Rent.Web.ViewModels.RentPaymentViewModel, Entities.RentPayment>();
        }
    }
}