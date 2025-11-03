using CarAssignment.Core.Data;
using CarAssignment.Infrastructure;
using CarAssignment.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace CarAssignment.Tests;

public abstract class TestBase : IDisposable
{
    protected readonly ParkingDbContext DbContext;
    private readonly IServiceProvider _serviceProvider;

    protected TestBase()
    {
        _serviceProvider = new ServiceCollection()
            .AddEntityFrameworkInMemoryDatabase()
            .BuildServiceProvider();

        var options = new DbContextOptionsBuilder<ParkingDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .UseInternalServiceProvider(_serviceProvider)
            .Options;

        DbContext = new ParkingDbContext(options);
    }

    /// <summary>
    /// Creates a generic mock repository that uses the shared in-memory DbContext.
    /// </summary>
    protected Mock<IRepository<T>> CreateRepositoryMock<T>() where T : class
    {
        var mock = new Mock<IRepository<T>>();

        mock.Setup(x => x.Query()).Returns(DbContext.Set<T>());
        mock.Setup(x => x.AddAsync(It.IsAny<T>()))
            .Callback<T>(entity =>
            {
                DbContext.Set<T>().Add(entity);
                DbContext.SaveChanges();
            })
            .Returns(Task.CompletedTask);

        mock.Setup(x => x.Delete(It.IsAny<T>()))
            .Callback<T>(entity =>
            {
                DbContext.Set<T>().Remove(entity);
                DbContext.SaveChanges();
            });

        mock.Setup(x => x.GetByIdAsync(It.IsAny<string>()))
            .ReturnsAsync((string id) => DbContext.Set<T>().Find(id));

        return mock;
    }

    /// <summary>
    /// Seeds entities of any type into the in-memory context.
    /// </summary>
    protected void SeedEntities<T>(params T[] entities) where T : class
    {
        DbContext.Set<T>().AddRange(entities);
        DbContext.SaveChanges();
    }
    
    /// <summary>
    /// Seeds entities of any type into the in-memory context.
    /// </summary>
    protected void ClearEntities<T>() where T : class
    {
        var set = DbContext.Set<T>();
        set.RemoveRange(set);
        DbContext.SaveChanges();
    }

    public virtual void Dispose()
    {
        DbContext.Dispose();
        if (_serviceProvider is IDisposable disposable)
            disposable.Dispose();
    }
}
