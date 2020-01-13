using MessagingMicroservice.DTO.Message;
using MessagingMicroservice.Services;
using System.Collections.Generic;

namespace MessagingtMicroservice.Services
{
    public interface IMessagingService : ICrudService<MessageResponseDTO> {
        MessageResponseDTO Create(CreateMessageRequestDTO requestDTO);
        MessageResponseDTO Delete(string uuid);
        public List<MessageResponseDTO> GetMessagesByRecipents(string recipentUUIDs);
    }
}
