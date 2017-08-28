using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Travels.Models
{
    public class Tours
    {
        public int ID { get; set; }
        public string TourName { get; set; }
        public DateTime Date { get; set; }

        public ICollection<Tours_Excursions> Tours_Excursions { get; set; }
        public ICollection<Tours_Clients> Tours_Clients { get; set; }
    }
}
