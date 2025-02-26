using Application.Contracts.Persistence;
using Domain.Entities;
using Moq;

namespace UnitTests.Moqs; 

public class MockCategoryRepository
{
    public static Mock<ICategoryRepository> GetCategoryMockRepository()
    {
        var categories = new List<Category>()
        {
            new Category() {Id=Guid.Parse("176AF8C5-9A26-43EB-AD43-2AF4987BAF74"), Name="Mobile"},
            new Category() {Id=Guid.Parse("DA9C1063-299B-4B4A-BDCF-30F28E065827"), Name="Fashion"},
            new Category() {Id=Guid.Parse("224953BA-EAA8-469D-A0E9-EE7C75A84196"), Name="Automobile"},
            new Category() {Id=Guid.Parse("F57D5D04-5AA2-4480-8868-87A5B892F731"), Name="Electronics"} 
        };
        var mockRepository = new Mock<ICategoryRepository>();
        mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(categories);
        mockRepository.Verify(repo => repo.CreateAsync(It.IsAny<Category>()), Times.Once);
        return mockRepository;
    }
}
