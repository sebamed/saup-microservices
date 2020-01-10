using Commons.Domain;
using TeamMicroservice.Domain.External;

namespace TeamMicroservice.Domain
{
    public class Team : BaseEntity
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public Teacher teacher { get; set; }
        public Course course { get; set; }
    }
}
