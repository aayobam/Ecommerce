using Application.Contracts.Logging;
using Application.Contracts.Persistence;
using Application.DTOs.Category;
using Application.Features.Category.Queries.GetAllCategory;
using Application.Mapping;
using AutoMapper;
using Moq;
using Shouldly;
using UnitTests.Moqs;

namespace UnitTests.Features.Category.Queries;

public class GetCategoryListHandlerTests
{
    private readonly Mock<ICategoryRepository> _mockRepo;
    private readonly IMapper _mapper;
    private readonly Mock<IApplicationLogger<GetCategoriesQueryHandler>> _mockAppLogger;

    public GetCategoryListHandlerTests()
    {
        _mockRepo = MockCategoryRepository.GetCategoryMockRepository();

        var mapperConfig = new MapperConfiguration(c =>
        {
            c.AddProfile<AutomapperProfile>();
        });

        _mapper = mapperConfig.CreateMapper();

        _mockAppLogger = new Mock<IApplicationLogger<GetCategoriesQueryHandler>>();
    }

    [Fact]
    public async Task GetCategoryListTest()
    {
        var handler = new GetCategoriesQueryHandler(_mockAppLogger.Object, _mapper, _mockRepo.Object);
        var result = await handler.Handle(new GetCategoriesQuery(), CancellationToken.None);
        result.ShouldBeOfType<List<CategoryVm>>();
        result.Count.ShouldBe(4);
    }
}
