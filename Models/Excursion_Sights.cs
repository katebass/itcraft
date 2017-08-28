using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Travels.Models
{
    public class Excursion_Sights
    {
        public int ID { get; set; }
        public string ExcursionName { get; set; }

        public ICollection<Tours_Excursions> Tours_Excursions { get; set; }
    }
}
