using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interface;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;

namespace akowe_CRUD.Services
{
   public class AcademicCredentialService : IAcademicCredentialService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IWebHostEnvironment _env;

        public AcademicCredentialService( IRepositoryManager repositoryManager, IWebHostEnvironment env)
        {
            _repositoryManager = repositoryManager;
            _env = env;
        }


        public async Task<AcademicCredential> GetAcademicCredentialAsync(Guid academicCredentialId)
        {
            var academicCredentialToReturn = await _repositoryManager
                .AcademicCredential.FindBy((x => x.IsDeleted == false && x.Id == academicCredentialId), false)
                .FirstOrDefaultAsync();

            return academicCredentialToReturn;
        }


        public async Task<List<AcademicCredential>> GetAcademicCredentialsAsync()
        {
            var academicCredentialsToReturn = await _repositoryManager.AcademicCredential.GetAll(false).Where(x => !x.IsDeleted)
                .ToListAsync();
            return academicCredentialsToReturn;
        }


        public async Task<AcademicCredential> AddAcademicCredentialAsync(AcademicCredential academicCredentialToAdd, IFormFile file , string ip)
        {
            if (file != null && file.Length != 0)
            {
                academicCredentialToAdd.TranscriptUrl = await SaveToDiskAsync(file);
            }


            _repositoryManager.AcademicCredential.Add(academicCredentialToAdd);


            _repositoryManager.AcademicCredential.Add(academicCredentialToAdd);

            await _repositoryManager.Save(ip);

            return academicCredentialToAdd;
        }


        public async Task<bool> DeleteAcademicCredentialAsync(Guid academicCredentialId, string ip)
        {

            var academicCredentialToDelete = await _repositoryManager
                .AcademicCredential.FindBy((x => x.IsDeleted == false && x.Id == academicCredentialId), true)
                .FirstOrDefaultAsync();

            if (academicCredentialToDelete == null)
            {
                return false;
            }

            academicCredentialToDelete.IsDeleted = true;
            academicCredentialToDelete.DateModified = DateTime.UtcNow;
            
            return await _repositoryManager.Save(ip);
        }


        public async Task<AcademicCredential> UpdateAcademicCredentialAsync(Guid academicCredentialId, AcademicCredential academicCredentialToUpdateDto, string ip)
        {

            var academicCredentialToUpdate = await _repositoryManager
                .AcademicCredential.FindBy((x => x.IsDeleted == false && x.Id == academicCredentialId), true)
                .FirstOrDefaultAsync();

            if (academicCredentialToUpdate == null)
            {
                return null;
            }

            academicCredentialToUpdate.Firstname = academicCredentialToUpdateDto.Firstname;
            academicCredentialToUpdate.Lastname = academicCredentialToUpdateDto.Lastname;
            academicCredentialToUpdate.Course = academicCredentialToUpdateDto.Course;
            academicCredentialToUpdate.Email = academicCredentialToUpdateDto.Email;
            academicCredentialToUpdate.EndYear = academicCredentialToUpdateDto.EndYear;
            academicCredentialToUpdate.StartYear = academicCredentialToUpdateDto.StartYear;
            academicCredentialToUpdate.Gender = academicCredentialToUpdateDto.Gender;
            academicCredentialToUpdate.GPA = academicCredentialToUpdateDto.GPA;
            academicCredentialToUpdate.DateModified = DateTime.UtcNow;

            await _repositoryManager.Save(ip);

            return academicCredentialToUpdate;
        }


        public async Task<AcademicCredential> UpdateAcademicCredentialTranscriptFileAsync(Guid academicCredentialId, IFormFile file, string ip)
        {

            var academicCredentialToUpdate = await _repositoryManager
                .AcademicCredential.FindBy((x => x.IsDeleted == false && x.Id == academicCredentialId), true)
                .FirstOrDefaultAsync();

            if (academicCredentialToUpdate == null)
            {
                return null;
            }


            var newFilePath = await SaveToDiskAsync(file);


            DeleteFromDisk(academicCredentialToUpdate.TranscriptUrl);


            academicCredentialToUpdate.TranscriptUrl = newFilePath;

            academicCredentialToUpdate.DateModified = DateTime.UtcNow;

            await _repositoryManager.Save(ip);

            return academicCredentialToUpdate;
        }




         private static async Task<string> SaveToDiskAsync(IFormFile file)
         {  
            if (file.Length > 0)
            {
                var fileName = Path.GetRandomFileName() + Path.GetExtension(file.FileName);
                var filePath = Path.Combine("Contents/Transcripts", fileName);
                await using var fileStream = new FileStream(filePath, FileMode.Create);
                await file.CopyToAsync(fileStream);

                return filePath;

            }

            return null;
        }

        private static void DeleteFromDisk(string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }
}
