using Application.Commands.Create;
using Application.Commands.Update;
using Application.Queries;
using Domain.Costumers;
using MediatR;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;


namespace MySolution.WebAPi.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Microsoft.AspNetCore.Mvc.Route("clients")]
    public class Controller : ControllerBase
    {
        #region Members

        private readonly IMediator _mediator;
        private readonly ILogger<Controller> _logger;


        #endregion

        #region Ctor

        public Controller(IMediator mediator, ILogger<Controller> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        #endregion


        #region Create
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] Client input)
        {

            if(!input.DocumentIsValid() || !input.IsValid())
            {
                return BadRequest(input.Error);
            }

            var response = await _mediator.Send(new CreateClientCommand { Client = input });

            if(response.Error.Message.Any())
            {
                return BadRequest(response.Error);
            }

            return Created("Client", new ClientResponse() { Created = true });
        }
        #endregion

        #region GetClient
        [HttpGet("{document}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetClient(string document) {

            Client cli = new();
            cli.Document = document;

            if (!cli.DocumentIsValid())
            {
                return BadRequest(cli.Error);

            }
            var result = await _mediator.Send(new GetClientByDocumentQuery { Document = document});

            if(result.Error != null) {

                return NotFound(result.Error);
            }

            return Ok(result);
        }
        #endregion
        #region Update
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(string id, Client updatedClient)
        {

            updatedClient._id = id;

            if (!updatedClient.IsValid()|| !updatedClient.DocumentIsValid() || !updatedClient.IdIsValid())
            {
                return BadRequest(updatedClient.Error);

            }

            updatedClient = await _mediator.Send(new UpdateClientCommand { Id = id , Client = updatedClient});

            if (updatedClient.Error != null)
            {

                return BadRequest(updatedClient.Error);
            }

            return Accepted();
        }
        #endregion
    }
}
