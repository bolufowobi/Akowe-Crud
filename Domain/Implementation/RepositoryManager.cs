using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Domain.Data;
using Domain.Interface;

namespace Domain.Implementation
{
    public class RepositoryManager : IRepositoryManager
    {
        private IAcademicCredentialRepository _academicCredential;
        private readonly DataContext _context;

        public RepositoryManager(DataContext context)
        {
            _context = context;
        }


        public IAcademicCredentialRepository AcademicCredential =>
            _academicCredential ?? new AcademicCredentialRepository(_context);



        public async Task<bool> Save( string ip) => ((await _context.SaveChangesAsync(ip)) > 0);

       
        public bool HasChanges()
        {
            _context.ChangeTracker.DetectChanges();
            var changes = _context.ChangeTracker.HasChanges();
            return changes;
        }

    }
}
