using Domain.Costumers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Create
{
    public class CreateClientCommand : IRequest<Client>
    {
        public Client Client { get; set; }
    }
}
