using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Application.Repositories.File;
using ECommerceAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Persistence.Repositories.File
{
    public class FileWriteRepository : WriteRepository<Domain.Entities.File.File>, IFileWriteRepository
    {
        public FileWriteRepository(ECommerceAPIDbContext context) : base(context)
        {
        }
    }
}
