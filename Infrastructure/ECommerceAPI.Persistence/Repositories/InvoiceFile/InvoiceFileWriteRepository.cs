using ECommerceAPI.Application.Repositories.InvoiceFile;
using ECommerceAPI.Application.Repositories.ProductImageFile;
using ECommerceAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Persistence.Repositories.InvoiceFile
{
    public class InvoiceFileWriteRepository : WriteRepository<Domain.Entities.File.InvoiceFile>, IInvoiceFileWriteRepository
    {
        public InvoiceFileWriteRepository(ECommerceAPIDbContext context) : base(context)
        {
        }
    }
}
