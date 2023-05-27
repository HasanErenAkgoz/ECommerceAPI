using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Application.Repositories.InvoiceFile;
using ECommerceAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Persistence.Repositories.InvoiceFile
{
    public class InvoiceFileReadRepository : ReadRepository<Domain.Entities.File.InvoiceFile>, IInvoiceFileReadRepository
    {
        public InvoiceFileReadRepository(ECommerceAPIDbContext context) : base(context)
        {
        }
    }
}
