using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TuiFly.Domain.Models.ViewModels
{
    public class ReservationViewModel
    {
        [Required(ErrorMessage = "The FlightId is required.")]
        [DisplayName("FlightId")]
        public Guid FlightId { get; set; }
        public List<PassengerViewModel> Passengers { get; set; }
    }
}
