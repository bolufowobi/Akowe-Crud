using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interface
{
    public interface IRepositoryManager
    {
        IAcademicCredentialRepository AcademicCredential { get; }
        bool HasChanges();
        Task<bool> Save(string ip);
    }
}
