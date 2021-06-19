using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using akowe_CRUD.Dtos.AttributeValidation;
using Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace akowe_CRUD.Dtos
{
    public class AcademicCredentialToAddDto
    {
        [Required(AllowEmptyStrings = false)]
        public string Firstname { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string Lastname { get; set; }
        [Required(AllowEmptyStrings = false)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public DateTime StartYear { get; set; }
        public DateTime EndYear { get; set; }

        [Required, Range(0, 1, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public Gender Gender { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string Course { get; set; }
        [Required, Range(0, 100, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public decimal GPA { get; set; }
        [FileSignatureValidation]
        // Max File Size 3MB
        [MaxFileSize(3000000)]
        public IFormFile TranscriptDoc { get; set; }
    }

}
