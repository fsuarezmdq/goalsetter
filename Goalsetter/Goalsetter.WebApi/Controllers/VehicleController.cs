using CSharpFunctionalExtensions;
using Goalsetter.AppServices.Dtos;
using Goalsetter.AppServices.Utils;
using Goalsetter.AppServices.Vehicles;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Goalsetter.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VehicleController: BaseController
    {
        private readonly IMessages _messages;        

        public VehicleController(IMessages messages)
        {
            _messages = messages;
        }


        [HttpGet]
        public async Task<IActionResult> GetListAsync()
        {
            var result = await _messages.Dispatch(new GetVehicleListAsyncQuery());

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] NewVehicleDto dto)
        {
            var command = new AddVehicleCommand(dto.Makes,dto.Model,dto.Year,dto.RentalPrice);

            Result result = await _messages.Dispatch(command);

            return FromResult(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(Guid id)
        {
            Result result = await _messages.Dispatch(new RemoveVehicleCommand(id));

            return FromResult(result);
        }
    }
}
