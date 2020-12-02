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
        private readonly IVehicleService _vehicleService;        

        public VehicleController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }


        [HttpGet]
        public async Task<IActionResult> GetListAsync()
        {
            return Ok
            (
                await _vehicleService.GetAsync()
            );
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] NewVehicleDto dto)
        {
            return FromResult
            (
                await _vehicleService.AddVehicleAsync(dto)
            );
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(Guid id)
        {
            return FromResult
            (
                await _vehicleService.RemoveVehicleAsync(id)
            );
        }
    }
}
