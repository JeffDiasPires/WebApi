using Application.Commands.Create;
using Domain.Costumers;
using MediatR;
using MongoDB.Bson;
using MySolution.Application.Services;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tavis.UriTemplates;

namespace Application.Commands
{
    public class CreateCommandHandler : IRequestHandler<CreateClientCommand, Client>
    {
        private readonly ClientService _clientsService;

        public CreateCommandHandler(ClientService clientsService) { 
            
            _clientsService = clientsService;
        }

        public async Task<Client> Handle(CreateClientCommand request, CancellationToken cancellationToken)
        {
            var result = await _clientsService.GetAsync(request.Client.Document);

            if (result != null)
            {

                result = new()
                {
                    Error = new() { Message = "Cliente Já Possui Cadastro. Por Favor Informe um novo cliente!" }
                };

                return result;
            }

            await _clientsService.CreateAsync(request.Client);
            return request.Client;
        }
    }
}
