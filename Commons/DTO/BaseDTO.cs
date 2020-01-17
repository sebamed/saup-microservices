using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Commons.DTO {
    public class BaseDTO {

        [Required]
        public string uuid { get; set; }

    }
}
