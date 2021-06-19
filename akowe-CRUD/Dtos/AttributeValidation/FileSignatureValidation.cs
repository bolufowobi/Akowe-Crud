using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace akowe_CRUD.Dtos.AttributeValidation
{
    public class FileSignatureValidationAttribute : ValidationAttribute
    {
        private static readonly Dictionary<string, List<byte[]>> _fileSignature =
              new Dictionary<string, List<byte[]>>
            {
                  { ".jpeg", new List<byte[]>
                      {
                          new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 },
                          new byte[] { 0xFF, 0xD8, 0xFF, 0xE2 },
                          new byte[] { 0xFF, 0xD8, 0xFF, 0xE3 },
                          new byte[] { 0xFF, 0xD8, 0xFF, 0xE8 },
                          new byte[] { 0xFF, 0xD8, 0xFF, 0xE1 },
                          new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 },
                          new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0a },
                          new byte[] { 0x42, 0x4D}
                      }
                  },
               }; 
    protected override ValidationResult IsValid(
        object value, ValidationContext validationContext)
        {
            if(value == null)
            { return  ValidationResult.Success; }

            var file = value as IFormFile;
           
            using (var reader = new BinaryReader(file.OpenReadStream()))
            {
                var signatures = _fileSignature[".jpeg"];
                var headerBytes = reader.ReadBytes(signatures.Max(m => m.Length));

                if (signatures.Any(signature =>
                    headerBytes.Take(signature.Length).SequenceEqual(signature)))
                {
                    return ValidationResult.Success;
                }
            }
            return new ValidationResult(GetErrorMessage());
        }

        public string GetErrorMessage()
        {
            return $"File uploaded is not of the required format, allowed format(.jpeg only).";
        }
    }
}
