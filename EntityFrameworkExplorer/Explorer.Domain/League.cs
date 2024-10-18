using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Domain
{
    public class League:BaseDomainModel
    {
        public string Name { get; set; }  
        public bool IsDeleted { get; set; } 
        public virtual List<Team>? Teams { get; set; } = new List<Team>() { };
    }
}
