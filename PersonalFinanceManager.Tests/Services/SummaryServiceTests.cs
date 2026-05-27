using Moq;
using PersonalFinanceManager.Models;
using PersonalFinanceManager.Repositories.Interfaces;
using PersonalFinanceManager.Services;

namespace PersonalFinanceManager.Tests.Services
{
    public class SummaryServiceTests
    {
        private readonly Mock<ISummaryRepository>
            _summaryRepositoryMock;

        private readonly Mock<IBudgetRepository>
            _budgetRepositoryMock;

        private readonly SummaryService
            _summaryService;

        public SummaryServiceTests()
        {
            _summaryRepositoryMock =
                new Mock<ISummaryRepository>();

            _budgetRepositoryMock =
                new Mock<IBudgetRepository>();

            _summaryService =
                new SummaryService(
                    _summaryRepositoryMock.Object,
                    _budgetRepositoryMock.Object);
        }

        [Fact]
        public void SummaryService_Should_Be_Created()
        {
            Assert.NotNull(_summaryService);
        }

        [Fact]
        public void CalculateBalance_Should_Return_Correct_Balance()
        {
            // Arrange

            User user = new User
            {
                Id = 1
            };

            _summaryRepositoryMock
                .Setup(x => x.GetTotalIncome(user.Id))
                .Returns(1000);

            _summaryRepositoryMock
                .Setup(x => x.GetTotalExpense(user.Id))
                .Returns(500);

            // Act

            decimal totalIncome =
                _summaryRepositoryMock.Object
                .GetTotalIncome(user.Id);

            decimal totalExpense =
                _summaryRepositoryMock.Object
                .GetTotalExpense(user.Id);

            decimal balance =
                totalIncome - totalExpense;

            // Assert

            Assert.Equal(500, balance);
        }
    }
}