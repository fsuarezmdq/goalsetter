using CSharpFunctionalExtensions;
using Goalsetter.AppServices.Clients;
using Goalsetter.AppServices.Dtos;
using Goalsetter.AppServices.Utils;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Goalsetter.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientController: BaseController
    {
        private readonly IClientService _clientService;        

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }


        [HttpGet]
        public async Task<IActionResult> GetListAsync()
        {
            return Ok
            (
                await _clientService.GetAsync()
            );
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] NewClientDto dto)
        {
            return FromResult
            (
                await _clientService.AddClientAsync(dto)
            );
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(Guid id)
        {
            return FromResult
            (
                await _clientService.RemoveClientAsync(id)
            );
        }
    }
}
