using InsuranceBrokerSystem.Application.DTOs.Client;
using InsuranceBrokerSystem.Application.Features.Clients.Commands;
using InsuranceBrokerSystem.Application.Features.Clients.Queries;
using InsuranceBrokerSystem.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceBrokerSystem.Api.Controllers.Clients
{
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ClientController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet(ApiRoutes.Clients.GetAllClients)]
        public async Task<IActionResult> GetAllClientsAsync()
        {
            var result = await _mediator.Send(new GetAllClientsQuery());
            if (result.Succeeded)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpGet(ApiRoutes.Clients.GetClientById)]
        public async Task<IActionResult> GetClientByIdAsync(int id)
        {
            var result = await _mediator.Send(new GetClientByIdQuery(id));
            if (result.Succeeded)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpPost(ApiRoutes.Clients.AddClient)]
        public async Task<IActionResult> AddClientAsync(AddClientDTO dto)
        {
            var result = await _mediator.Send(new AddClientCommand(dto));
            if (result.Succeeded)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpPut(ApiRoutes.Clients.UpdateClient)]
        public async Task<IActionResult> UpdateClientAsync(UpdateClientDTO dto)
        {
            var result = await _mediator.Send(new UpdateClientCommand(dto));
            if (result.Succeeded)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpDelete(ApiRoutes.Clients.DeleteClient+"{id}")]
        public async Task<IActionResult> DeleteClientAsync(int id)
        {
            var result = await _mediator.Send(new DeleteClientCommand(id));
            if (result.Succeeded)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpPut(ApiRoutes.Clients.ApproveClient+"/{id}")]
        public async Task<IActionResult> ApproveClientAsync(int id, ApproveClientDTO dto)
        {
            var result = await _mediator.Send(new ApproveClientCommand(id, dto));
            if (result.Succeeded)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpPut(ApiRoutes.Clients.RejectClient)]
        public async Task<IActionResult> RejectClientAsync(int id)
        {
            var result = await _mediator.Send(new RejectClientCommand(id));
            if (result.Succeeded)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpPut(ApiRoutes.Clients.BlockClient)]
        public async Task<IActionResult> BlockClientAsync(int id)
        {
            var result = await _mediator.Send(new BlockClientCommand(id));
            if (result.Succeeded)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }
    }
}
