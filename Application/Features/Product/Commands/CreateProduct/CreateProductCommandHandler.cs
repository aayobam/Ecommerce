using Application.Contracts.Persistence;
using Application.Exceptions;
using AutoMapper;
using Domain.Dtos.Product;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Application.Features.Product.Commands.CreateProduct;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductVm>
{
    private readonly ILogger<CreateProductCommandHandler> _logger;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public CreateProductCommandHandler(ILogger<CreateProductCommandHandler> logger, IProductRepository productRepository, IMapper mapper)
    {
        _logger = logger;
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<ProductVm> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateProductCommandValidator();
        var validationResult = validator.Validate(request);
        if (validationResult.Errors.Any())
        {
            _logger.LogInformation($"\n bad request | {DateTimeOffset.Now} \n");
            throw new EcommerceBadRequestException(validationResult, HttpStatusCode.BadRequest.ToString());
        }
        var model = _mapper.Map<Domain.Entities.Product>(request);
        await _productRepository.CreateAsync(model);
        var instance = _mapper.Map<ProductVm>(request);
        return instance;
    }
}
