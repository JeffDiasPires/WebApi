using Domain.Costumers;
using MediatR;
using MySolution.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Commands.Update
{
    public class UpdateClientCommandHandler : IRequestHandler<UpdateClientCommand, Client>
    {
        private readonly ClientService _clientsService;

        public UpdateClientCommandHandler(ClientService clientsService)
        {

            _clientsService = clientsService;
        }

        public async  Task<Client> Handle(UpdateClientCommand request, CancellationToken cancellationToken)
        {

            var result = await _clientsService.UpdateAsync(request.Id, request.Client);

            if(result.MatchedCount == 0)
            {
                request.Client.Error = new()
                {
                    Message = "Cliente Não Foi Atualizado. Por Favor verifique os dados e envie novamente."
                };
                

            }
            return request.Client;
        }
    }
}
