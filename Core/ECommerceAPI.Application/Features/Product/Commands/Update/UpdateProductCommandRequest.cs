using ECommerceAPI.Application.ViewModels.Products;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Product.Commands.Update
{
    public class UpdateProductCommandRequest : IRequest<UpdateProductCommandResponse>
    {
        public VM_Update_Product Product { get; set; }
    }
}
