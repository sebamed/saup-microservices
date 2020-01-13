using MessagingMicroservice.DTO.Message;
using MessagingMicroservice.Services;
using System.Collections.Generic;

namespace MessagingtMicroservice.Services
{
    public interface IMessagingService : ICrudService<MessageResponseDTO> {
        MessageResponseDTO Create(CreateMessageRequestDTO requestDTO);
        MessageResponseDTO Update(UpdateMessageRequestDTO requestDTO);
        MessageResponseDTO Delete(string uuid);
    }
}
