using Commons.Consts;
using Commons.DatabaseUtils;
using Commons.ExceptionHandling.Exceptions;
using System.Collections.Generic;
using DepartmentMicroservice.Consts;
using DepartmentMicroservice.Domain;
using DepartmentMicroservice.DTO.User;
using DepartmentMicroservice.DTO.User.Request;
using DepartmentMicroservice.Mappers;

namespace DepartmentMicroservice.Services.Implementation {
    public class UserService : IUserService {

        private readonly QueryExecutor _queryExecutor;

        private readonly ModelMapper _modelMapper;

        public UserService(QueryExecutor queryExecutor, ModelMapper modelMapper) {
            this._queryExecutor = queryExecutor;
            this._modelMapper = modelMapper;
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
            //List<Instrument> k = this._queryExecutor.Execute<Instrument>(DatabaseConsts.USER_SCHEMA, "select * from tblInstrument;");
            

            // koristicemo ovakve mappere:
            var b = this._queryExecutor.Execute<Instrument>(DatabaseConsts.USER_SCHEMA, "select * from tblInstrument where InstrumentID = 123;", this._modelMapper.mapToInstrument);
            var a = this._queryExecutor.Execute<List<Instrument>>(DatabaseConsts.USER_SCHEMA, "select * from tblInstrument;", this._modelMapper.mapToInstruments);

            throw new EntityNotFoundException($"User with uuid: {uuid} does not exist!", GeneralConsts.MICROSERVICE_NAME);
        }

        public UserResponseDTO Update(UpdateUserRequestDTO requestDTO) {
            // todo
            return new UserResponseDTO();
        }       
    }

}
