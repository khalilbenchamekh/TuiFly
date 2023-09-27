using Microsoft.AspNetCore.Mvc;
using TuiFly.Domain.Interfaces.Service.Flight;
using TuiFly.Domain.Models.Response;
using TuiFly.Domain.Models.ViewModels;

namespace TuiFly.Api.Controllers
{
    /// <summary>
    /// </summary>
    /// <seealso cref="FlightController{T}" />
    [Route("api/[controller]")]
    [ApiController]
    public class FlightController : BaseController<FlightController>
    {
        private readonly IFlightService _flightService;
        public FlightController(ILoggerFactory factory, IFlightService flightService) : base(factory)
        {
            _flightService = flightService;
        }

        [HttpPost("save")]
        public async Task<ActionResult<MainResponse<bool>>> save([FromBody] ReservationViewModel model)
        {
            var res = new MainResponse<bool>();
            var message = "success"; ;
            try
            {
                this.Logger.LogInformation("FlightController try to save");
                var response = await _flightService.save(model);
                if (response == false) message = "notSaved";
                res.Response = response;
            }
            catch (Exception e)
            {
                message = "errorSavingadminstrative";
                this.Logger.LogError("error save adminstrative ", e);
            }
            res.Message = message;
            return Ok(res);
        }

        [HttpPost("findAll")]
        public async Task<ActionResult<MainResponse<FlightResponse>>> findAll([FromBody] FlightViewModel model)
        {
            var res = new MainResponse<IEnumerable<FlightResponse>>();
            var message = "success";
            try
            {
                this.Logger.LogInformation("FlightController try to searching by id" + model.Id.ToString());
                var response = await _flightService.findAll(model);
                if (response == null) message = "emptyData";
                res.Response = response;
            }
            catch (Exception e)
            {
                message = "errorgetFilght";
                res.Error = true;
                this.Logger.LogError("error while searching into Filght " + model.Id.ToString(), e);
            }
            res.Message = message;
            return Ok(res);
        }
    }
}
