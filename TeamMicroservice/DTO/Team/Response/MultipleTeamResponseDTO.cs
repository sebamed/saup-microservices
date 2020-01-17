using Commons.DTO;

namespace TeamMicroservice.DTO.Team.Response
{
    public class MultipleTeamResponseDTO : BaseDTO
    {
        public string name { get; set; }
        public string description { get; set; }
        public string teacherUUID { get; set; }
        public string courseUUID { get; set; }
    }
}
