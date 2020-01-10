using Commons.Domain;
using System;

namespace FileMicroservice.Domain
{
    public class File : BaseEntity
    {
        public int id { get; set; }
        public string filePath { get; set; }
    }
}
