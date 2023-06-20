using Domain.Costumers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Update
{
    public class UpdateClientCommand : IRequest<Client>
    {
        public string Id { get; set; }
        
        public Client Client { get; set; }  
    }
}
