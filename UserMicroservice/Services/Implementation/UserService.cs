using Commons.Consts;
using Commons.DatabaseUtils;
using Commons.ExceptionHandling.Exceptions;
using System.Collections.Generic;
using UserMicroservice.Consts;
using UserMicroservice.DTO.User;
using UserMicroservice.DTO.User.Request;

namespace UserMicroservice.Services.Implementation {
    public class UserService : IUserService {

        private readonly QueryExecutor _queryExecutor;

        public UserService(QueryExecutor queryExecutor) {
            this._queryExecutor = queryExecutor;
        }

        public UserResponseDTO Create(CreateUserRequestDTO requestDTO) {
            // todo
            return new UserResponseDTO();
        }

        public List<UserResponseDTO> GetAll() {
            // todo
            return new List<UserResponseDTO>();
        }
        public UserResponseDTO GetOneByUuid(string uuid) {
            // todo

            // *1
            List<Instrument> k = this._queryExecutor.Execute<Instrument>(DatabaseConsts.USER_SCHEMA, "select * from tblInstrument;");

            throw new EntityNotFoundException($"User with uuid: {uuid} does not exist!", GeneralConsts.MICROSERVICE_NAME);
        }

        public UserResponseDTO Update(UpdateUserRequestDTO requestDTO) {
            // todo
            return new UserResponseDTO();
        }
    }

    // *1
    // Ovo je samo primer;
    // U folderu Models cemo drzati modele koje cemo prosledjivati
    class Instrument { 
        public string InstrumentNaziv { get; set; }
    }

}
