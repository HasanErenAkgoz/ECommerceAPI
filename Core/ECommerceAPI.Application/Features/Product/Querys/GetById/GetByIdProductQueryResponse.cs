using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities = ECommerceAPI.Domain.Entities;

namespace ECommerceAPI.Application.Features.Product.Querys.GetById
{
    public class GetByIdProductQueryResponse
    {
        public Entities.Product Product { get; set; }
    }
}
