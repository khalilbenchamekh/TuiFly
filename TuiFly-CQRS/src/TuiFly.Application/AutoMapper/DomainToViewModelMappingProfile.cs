using AutoMapper;
using TuiFly.Domain.Models.Entities;
using TuiFly.Domain.Models.Response;
using TuiFly.Domain.Models.ViewModels;

namespace TuiFly.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<CustomerEntity, CustomerViewModel>();
            CreateMap<FlightEntity, FlightResponse>();
            CreateMap<PlanEntity, PlanResponse>();
            CreateMap<ReservationEntity, ReservationResponse>().ForMember(dest => dest.PassengerName, opt => opt.Ignore()); ;
        }
    }
}
