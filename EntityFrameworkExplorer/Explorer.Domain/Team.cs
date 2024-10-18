using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Domain
{
    public class Team : BaseDomainModel
    {
        public string? Name { get; set; }   
        public virtual League? League { get; set; }  
        public int? LeagueId {  get; set; }  
        public virtual Coach Coach { get; set; }
        public int CoachId {  get; set; }

        // Specific to SQL Server
        //[Timestamp]
        //public byte[] Version { get; set; }
        public virtual List<Match> HomeMatches { get; set; } = new List<Match>() { };
        public virtual List<Match> AwayMatches { get; set; } = new List<Match>() { };   
    }
}
