using AutoMapper;
using TuiFly.Domain.Commands.Customer.Commands;
using TuiFly.Domain.Commands.Flight.Commands;
using TuiFly.Domain.Commands.Flight.Validation;
using TuiFly.Domain.Models.ViewModels;

namespace TuiFly.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<ReservationViewModel, MakeReservationCommandValidation>();
            CreateMap<CustomerViewModel, RegisterNewCustomerCommand>()
                .ConstructUsing(c => new RegisterNewCustomerCommand(c.LastName, c.FirstName, c.Email));

            CreateMap<PassengerViewModel, RegisterNewCustomerCommand>()
          .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
          .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
          .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));

            CreateMap<ReservationViewModel, MakeReservationCommand>()
                .ForMember(dest => dest.FlightId, opt => opt.MapFrom(src => src.FlightId))
                .ForMember(dest => dest.Passengers, opt => opt.MapFrom(src => src.Passengers));
        }
    }
}
