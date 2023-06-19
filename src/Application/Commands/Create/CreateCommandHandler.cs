using Application.Commands.Create;
using Domain.Costumers;
using MediatR;
using MongoDB.Bson;
using MySolution.Application.Services;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

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
            var response = _clientsService.GetAsync(request.Client.Document);

            //if (response.Result != null && response.Result.Document.Any()) {

            //    response.Result.Error = "CPF já esta cadastrado";
            //    return response.Result;

            //}
            await _clientsService.CreateAsync(request.Client);
            return request.Client;
        }
    }
}
