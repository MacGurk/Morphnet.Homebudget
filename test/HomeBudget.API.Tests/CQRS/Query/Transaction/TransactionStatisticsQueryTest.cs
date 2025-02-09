using AutoMapper;
using HomeBudget.API.CQRS.Query.Transaction;
using HomeBudget.API.Entities;
using HomeBudget.API.Profiles;
using HomeBudget.API.Services.Repositories;
using Moq;

namespace HomeBudget.API.Tests.CQRS.Query.Transaction;

public class TransactionStatisticsQueryTest
{
    [Fact]
    public async Task Handle_GetStatistics()
    {
        var contributingUsers = new List<User>
        {
            new()
            {
                Id = 1,
                Name = "John",
                Email = ""
            },
            new()
            {
                Id = 2,
                Name = "Kate",
                Email = ""
            }
        };

        var transactions = new List<Entities.Transaction>
        {
            new()
            {
                Id = 1,
                User = contributingUsers[0],
                Price = 100,
                Date = new DateOnly(2021, 1, 1)
            },
            new()
            {
                Id = 2,
                User = contributingUsers[0],
                Price = 50,
                Date = new DateOnly(2021, 1, 2)
            },
            new()
            {
                Id = 3,
                User = contributingUsers[0],
                Price = 200,
                Date = new DateOnly(2021, 2, 1)
            },
            new()
            {
                Id = 4,
                User = contributingUsers[1],
                Price = 300,
                Date = new DateOnly(2021, 1, 1)
            },
            new()
            {
                Id = 5,
                User = contributingUsers[1],
                Price = 400,
                Date = new DateOnly(2021, 2, 1)
            }
        };

        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new TransactionProfile());
            cfg.AddProfile(new UserProfile());
        });
        var mapper = config.CreateMapper();

        var transactionRepositoryMock = new Mock<ITransactionRepository>();
        transactionRepositoryMock.Setup(x => x.GetTransactionsAsync(It.IsAny<string?>(), It.IsAny<int?>(), It.IsAny<int?>())).ReturnsAsync(transactions);

        var userRepositoryMock = new Mock<IUserRepository>();
        userRepositoryMock.Setup(x => x.GetUsersAsync(true)).ReturnsAsync(contributingUsers);

        var handler =
            new GetTransactionStatisticsQueryHandler(transactionRepositoryMock.Object, userRepositoryMock.Object, mapper);
        var result = (await handler.Handle(new GetTransactionStatisticsQuery(2021), CancellationToken.None)).ToList();

        Assert.Equal(2, result.Count);
        Assert.Equal(12, result[0].MonthlyTotal.Length);
        Assert.Equal(12, result[1].MonthlyTotal.Length);
        Assert.Equal(150, result[0].MonthlyTotal[0]);
        Assert.Equal(200, result[0].MonthlyTotal[1]);
        Assert.Equal(300, result[1].MonthlyTotal[0]);
        Assert.Equal(400, result[1].MonthlyTotal[1]);
    }
}