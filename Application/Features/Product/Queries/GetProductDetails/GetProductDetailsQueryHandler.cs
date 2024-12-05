using Application.Contracts.Logging;
using Application.Contracts.Persistence;
using AutoMapper;
using Domain.Dtos.Product;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using System.Net;

namespace Application.Features.Product.Queries.GetProductDetails;

public class GetProductDetailsQueryHandler : IRequestHandler<GetProductDetailsQuery, ProductVm>
{
    private readonly IApplicationLogger<GetProductDetailsQueryHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IProductRepository _productRepository;

    public GetProductDetailsQueryHandler(IApplicationLogger<GetProductDetailsQueryHandler> logger,IMapper mapper, IProductRepository productRepository)
    {
        _logger = logger;
        _mapper = mapper;
        _productRepository = productRepository;
    }

    public IMapper Mapper { get; }
    public IProductRepository ProductRepository { get; }

    public async Task<ProductVm> Handle(GetProductDetailsQuery request, CancellationToken cancellationToken)
    {
        var instance = await _productRepository.GetByIdAsync(request.id);

        if (instance == null)
        {
            _logger.LogInformation($"\n {nameof(instance)} - {request.id} not found | {DateTime.UtcNow} \n");
            throw new EcommerceNotFoundException(nameof(instance), request.id, HttpStatusCode.NotFound.ToString());
        }

        var data = _mapper.Map<ProductVm>(instance);

        return data;
    }
}
