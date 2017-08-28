using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Travels.Models.TravelViewModels
{
    public class DateGroup
    {
        [DataType(DataType.Date)]
        public DateTime? Date { get; set; }

        public int ToursCount { get; set; }
    }
}
