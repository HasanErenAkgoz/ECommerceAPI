using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Application.Repositories.ProductImageFile;
using ECommerceAPI.Domain.Entities;
using ECommerceAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Persistence.Repositories.ProductImageFile
{
    public class ProductImageFileReadRepository : ReadRepository<Domain.Entities.File.ProductImageFile>, IProductImageFileReadRepository
    {
        public ProductImageFileReadRepository(ECommerceAPIDbContext context) : base(context)
        {
        }
    }
}
