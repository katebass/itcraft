using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Travels.Models
{
    public class Tours_Clients
    {
        public int ID { get; set; }
        public int ToursID { get; set; }
        public int ClientsID { get; set; }

        public Tours Tours { get; set; }
        public Clients Clients { get; set; }
    }
}
