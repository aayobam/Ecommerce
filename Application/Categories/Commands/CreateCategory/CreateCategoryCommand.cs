using System;
using System.Windows.Input;
using Domain.Dtos.Category;
using Domain.Entities;
using MediatR;

namespace Application.Categories.Commands.CreateCategory;

public sealed class CreateCategoryCommand : IRequest<CreateCategoryVm>
{
    public Guid CategoryId { get; set; }
    public Category Category { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public string ShortDescription { get; set; }
    public string Description { get; set; }
    public bool Available { get; set; } = true;
}
 