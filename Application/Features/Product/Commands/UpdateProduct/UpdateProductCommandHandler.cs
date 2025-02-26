using AutoMapper;
using Domain.Dtos.Product;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Features.Product.Commands.UpdateProduct;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ProductVm>
{
    private readonly ILogger<UpdateProductCommandHandler> _logger;
    private readonly IMapper _mapper;

    public UpdateProductCommandHandler(ILogger<UpdateProductCommandHandler> logger, IMapper mapper)
    {
        _logger = logger;
        _mapper = mapper;
    }

    public Task<ProductVm> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
