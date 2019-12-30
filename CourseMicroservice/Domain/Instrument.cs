using Commons.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseMicroservice.Domain {
    public class Instrument : BaseEntity {

        public int InstrumentID { get; set; }

        public string InstrumentNaziv { get; set; }

    }
}
