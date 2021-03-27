using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cross_Kamil.Models
{
    public class Company
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long Proceeds { get; set; }
        public long Profit { get; set; }

        public List<Business> Businessmens { get; set; }

        public Company()
        {
            Businessmens = new List<Business>();
        }

    }
}
