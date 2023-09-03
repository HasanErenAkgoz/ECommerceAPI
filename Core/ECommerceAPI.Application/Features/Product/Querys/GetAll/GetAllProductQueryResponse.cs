using ECommerceAPI.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Product.Querys.GetAll
{
    public class GetAllProductQueryResponse
    {
        public int TotalCount { get; set; }
        public List<ProductDTO> Products { get; set; }
    }
}
