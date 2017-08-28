using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Travels.Models
{
    public class Tours_Excursions
    {
        public int ID { get; set; }
        public int ToursID { get; set; }
        public int Excursion_SightsID { get; set; }

        public Tours Tours { get; set; }
        public Excursion_Sights Excursion_Sights { get; set; }
    }
}
