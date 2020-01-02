using System;
using System.Collections.Generic;
using System.Text;

namespace Commons.Domain {
    public class BaseEntity {

        public string uuid { get; set; } = Guid.NewGuid().ToString();

    }
}
