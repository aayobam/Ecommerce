﻿using Application.DTOs.Category;
using Application.DTOs.Color;
using Domain.Dtos.Product;
using MediatR;

namespace Application.Features.Product.Commands.UpdateProduct;

public class UpdateProductCommand : IRequest<ProductVm>
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Discount { get; set; }
    public int Quantity { get; set; }
    public string ShortDescription { get; set; }
    public string Description { get; set; }
    public bool Available { get; set; } = true;
    public Guid CategoryId { get; set; }
    public Guid VendorId { get; set; }
    public CategoryVm Category { get; set; }
    public List<ColorVm> Colors { get; set; }
}