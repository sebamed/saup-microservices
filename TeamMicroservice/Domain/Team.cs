using Commons.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeamMicroservice.Domain
{
    public class Team : BaseEntity
    {
        public int id { get; set; }
        public string uuid { get; set; }
        public string name { get; set; }
        public string description { get; set; }
    }
}
