using System;
using System.Collections.Generic;
using System.Text;
using Domain.Data;
using Domain.Entities;
using Domain.Interface;

namespace Domain.Implementation
{
    public class AcademicCredentialRepository : RepositoryBase<AcademicCredential>, IAcademicCredentialRepository
    {
        public AcademicCredentialRepository(DataContext context) : base(context)
        {
        }


    }
}
