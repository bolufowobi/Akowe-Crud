using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace akowe_CRUD.Dtos
{
    public class AcademicCredentialToReturnDto
    {
        public Guid Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public DateTime StartYear { get; set; }
        public DateTime EndYear { get; set; }
        public string TranscriptUrl { get; set; }
        public Gender Gender { get; set; }
        public string Course { get; set; }
        public decimal GPA { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
