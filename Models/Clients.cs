using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Travels.Models
{
    public class Clients
    {
        public int ID { get; set; }
        public string ClientName { get; set; }

        public ICollection<Tours_Clients> Tours_Clients { get; set; }
    }
}
