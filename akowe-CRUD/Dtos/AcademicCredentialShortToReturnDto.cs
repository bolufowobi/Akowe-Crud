using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace akowe_CRUD.Dtos
{
    public class AcademicCredentialShortToReturnDto
    {
        public Guid Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string TranscriptUrl { get; set; }
        public string Course { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
