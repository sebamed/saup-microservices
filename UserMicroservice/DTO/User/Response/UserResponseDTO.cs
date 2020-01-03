using Commons.DTO;
using UserMicroservice.DTO.User.Response;

namespace UserMicroservice.DTO.User {
    public class UserResponseDTO : BaseDTO {

        public string name { get; set; }

        public string surname { get; set; }

        public string email { get; set; }

        public string phone { get; set; }

        public RoleResponseDTO role { get; set; }

    }
}
