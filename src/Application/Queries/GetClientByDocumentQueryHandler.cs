using Domain.Costumers;
using MediatR;
using MySolution.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Queries
{
    public class GetClientByDocumentQueryHandler : IRequestHandler<GetClientByDocumentQuery, Client>
    {
        private readonly ClientService _clientsService;

        public GetClientByDocumentQueryHandler(ClientService clientsService)
        {

            _clientsService = clientsService;
        }

        public async Task<Client> Handle(GetClientByDocumentQuery request, CancellationToken cancellationToken)
        {
            var result = await _clientsService.GetAsync(request.Document);

            if(result == null) {

                result = new()
                {
                    Error = new() { Message = "Este Cliente Não foi Encontrado. Por Favor Informe um cliente cadastrado !" }
                };
            }

            return result;
        }
    }
}
