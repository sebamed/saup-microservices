using AutoMapper;
using MessagingMicroservice.Domain;
using MessagingMicroservice.DTO;
using MessagingMicroservice.DTO.Message;

namespace MessagingMicroservice.Mappers
{
    public class MappingProfile: Profile {

        public MappingProfile() {
            CreateMap<Message, MessageResponseDTO>();
            CreateMap<File, FileDTO>();
            CreateMap<User, UserDTO>();
        }
    }
}
