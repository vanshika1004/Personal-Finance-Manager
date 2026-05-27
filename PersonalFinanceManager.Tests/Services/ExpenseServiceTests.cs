using Moq;
using PersonalFinanceManager.Repositories.Interfaces;
using PersonalFinanceManager.Services;
using PersonalFinanceManager.Validators;

namespace PersonalFinanceManager.Tests.Services
{
    public class ExpenseServiceTests
    {
        private readonly Mock<IExpenseRepository>
            _expenseRepositoryMock;

        private readonly Mock<ICategoryRepository>
            _categoryRepositoryMock;

        private readonly ExpenseService
            _expenseService;

        public ExpenseServiceTests()
        {
            _expenseRepositoryMock =
                new Mock<IExpenseRepository>();

            _categoryRepositoryMock =
                new Mock<ICategoryRepository>();

            var validator =
                new ExpenseValidator();

            _expenseService =
                new ExpenseService(
                    _expenseRepositoryMock.Object,
                    _categoryRepositoryMock.Object,
                    validator);
        }

        [Fact]
        public void ExpenseService_Should_Be_Created()
        {
            Assert.NotNull(_expenseService);
        }
    }
}