using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cross_Kamil.Models
{
    public class Business
    {
        public long BusinessmenId { get; set; }
        public Businessmen Businessmen { get; set; }
        public long CompanyId { get; set; }
        public Company Company { get; set; }
    }
}
