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
        private readonly IMessages _messages;        

        public ClientController(IMessages messages)
        {
            _messages = messages;
        }


        [HttpGet]
        public async Task<IActionResult> GetListAsync()
        {
            var result = await _messages.Dispatch(new GetClientListAsyncQuery());

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] NewClientDto dto)
        {
            var command = new AddClientCommand(dto.Name,dto.Email);

            Result result = await _messages.Dispatch(command);

            return FromResult(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(Guid id)
        {
            Result result = await _messages.Dispatch(new RemoveClientCommand(id));

            return FromResult(result);
        }
    }
}
