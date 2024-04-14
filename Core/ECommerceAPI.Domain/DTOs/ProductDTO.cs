using ECommerceAPI.Domain.Entities.Common;
using ECommerceAPI.Domain.Entities.File;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Domain.DTOs
{
    public class ProductDTO : BaseEntity
    {
        public string Name { get; set; }
        public int Stock { get; set; }
        public float Price { get; set; }
        public List<ProductImageFile> ProductImageFiles { get; set; }
    }
}
