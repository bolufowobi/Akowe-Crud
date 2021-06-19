using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Domain.Interface
{
    public interface IAcademicCredentialService
    {
        Task<AcademicCredential> GetAcademicCredentialAsync(Guid academicCredentialId);
        Task<List<AcademicCredential>> GetAcademicCredentialsAsync();
        Task<AcademicCredential> AddAcademicCredentialAsync(AcademicCredential academicCredentialToAdd, IFormFile file, string ip);
        Task<bool> DeleteAcademicCredentialAsync(Guid academicCredentialId, string ip);
        Task<AcademicCredential> UpdateAcademicCredentialAsync(Guid academicCredentialId, AcademicCredential academicCredentialToUpdateDto, string ip);
        Task<AcademicCredential> UpdateAcademicCredentialTranscriptFileAsync(Guid academicCredentialId, IFormFile file, string ip);
    }
}
