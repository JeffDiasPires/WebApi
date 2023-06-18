using Domain.Costumers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries
{
    public class GetClientCommand : IRequest<Client>
    {
        
        public string Document { get; set; }

    }
}
