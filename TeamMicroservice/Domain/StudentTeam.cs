using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamMicroservice.Domain.External;

namespace TeamMicroservice.Domain
{
    public class StudentTeam
    {
        public Team team { get; set; }
        public Student student { get; set; }
    }
}
