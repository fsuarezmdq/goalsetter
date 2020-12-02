using CSharpFunctionalExtensions;
using Goalsetter.AppServices;
using Goalsetter.AppServices.Dtos;
using Goalsetter.AppServices.Rentals;
using Goalsetter.DataAccess;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Goalsetter.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RentalController: BaseController
    {
        private readonly IRentalService _rentalService;

        public RentalController(IRentalService rentalService)
        {
            _rentalService = rentalService;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] NewRentalDto dto)
        {
            return FromResult
            (
                await _rentalService.AddRentalAsync(dto)
            );
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(Guid id)
        {
            return FromResult
            (
                await _rentalService.RemoveRentalAsync(id)
            );
        }
    }
}
