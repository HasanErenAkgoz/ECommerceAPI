﻿using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Application.ViewModels.Products;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Product.Commands.Create
{
    public class CreateProductCommandRequest : IRequest<CreateProductCommandResponse>
    {
        public string? Name { get; set; }
        public int Stock { get; set; }
        public float Price { get; set; }
    }
}
