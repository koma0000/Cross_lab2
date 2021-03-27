using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cross_Kamil.Models
{
    public class Businessmen
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public long age { get; set; }

        public List<Business> Companies { get; set; }

        public Businessmen()
        {
            Companies = new List<Business>();
        }
    }
}
