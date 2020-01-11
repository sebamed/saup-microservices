using Commons.DTO;

namespace TeamMicroservice.DTO.External
{
    public class StudentDTO: BaseDTO
    {
        public string name { get; set; }
        public string surname { get; set; }
        public string email { get; set; }
        public string indexNumber { get; set; }
    }
}
