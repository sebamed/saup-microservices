using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TeamMicroservice.Domain;

namespace TeamMicroservice.Mappers {
    public class ModelMapper {

        public List<Instrument> mapToInstruments(IDataReader reader) {
            List<Instrument> instruments = new List<Instrument>();

            while(reader.Read()) {
                instruments.Add(new Instrument() {
                    InstrumentID = Convert.ToInt32(reader["InstrumentID"]),
                    InstrumentNaziv = Convert.ToString(reader["InstrumentNaziv"])
                });
            }

            return instruments;
        }

        public Instrument mapToInstrument(IDataReader reader) {
            while (reader.Read()) {
                return new Instrument() {
                    InstrumentID = Convert.ToInt32(reader["InstrumentID"]),
                    InstrumentNaziv = Convert.ToString(reader["InstrumentNaziv"])
                };
            }

            return null;
        }
    }
}
