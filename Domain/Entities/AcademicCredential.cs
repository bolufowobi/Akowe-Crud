using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class AcademicCredential : BaseEntity
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public DateTime StartYear { get; set; }
        public DateTime EndYear { get; set; }
        public string TranscriptUrl { get; set; }
        public Gender Gender { get; set; }
        public string Course { get; set; }
        public decimal GPA { get; set; }

    }

    public enum Gender
    {
        Female = 0,
        Male = 1
    }
}
