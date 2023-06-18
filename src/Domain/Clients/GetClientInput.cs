using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Costumers
{
    public class GetClientInput
    {
        public string Name { get; set; }

        public DateTime BirthDay { get; set; }

        [JsonIgnore]
        public string Error { get; set; }
    }
}
