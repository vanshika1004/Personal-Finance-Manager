using Moq;
using PersonalFinanceManager.Enums;
using PersonalFinanceManager.Repositories.Interfaces;
using PersonalFinanceManager.Services;
using PersonalFinanceManager.Validators;

namespace PersonalFinanceManager.Tests.Services
{
    public class CategoryServiceTests
    {
        private readonly Mock<ICategoryRepository>
            _categoryRepositoryMock;

        private readonly CategoryService
            _categoryService;

        public CategoryServiceTests()
        {
            _categoryRepositoryMock =
                new Mock<ICategoryRepository>();

            var validator =
                new CategoryValidator();

            _categoryService =
                new CategoryService(
                    _categoryRepositoryMock.Object,
                    validator);
        }

        [Fact]
        public void Exists_Should_Return_True_When_Category_Already_Exists()
        {
            // Arrange

            string name = "Food";

            CategoryType type =
                CategoryType.Expense;

            _categoryRepositoryMock
                .Setup(x => x.Exists(name, type))
                .Returns(true);

            // Act

            bool result =
                _categoryRepositoryMock.Object
                .Exists(name, type);

            // Assert

            Assert.True(result);
        }

        [Fact]
        public void Exists_Should_Return_False_When_Category_Does_Not_Exist()
        {
            // Arrange

            string name = "Travel";

            CategoryType type =
                CategoryType.Expense;

            _categoryRepositoryMock
                .Setup(x => x.Exists(name, type))
                .Returns(false);

            // Act

            bool result =
                _categoryRepositoryMock.Object
                .Exists(name, type);

            // Assert

            Assert.False(result);
        }
    }
}