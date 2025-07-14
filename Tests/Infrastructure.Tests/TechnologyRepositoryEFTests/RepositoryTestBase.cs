using AutoMapper;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;

namespace Infrastructure.Tests;

/// <summary>
/// Base class for repository tests – sets up an in‑memory AbsanteeContext and a mocked IMapper.
/// </summary>
public abstract class RepositoryTestBase : IDisposable
{
    protected readonly AbsanteeContext Context;
    protected readonly Mock<IMapper> Mapper;

    protected RepositoryTestBase()
    {
        var options = new DbContextOptionsBuilder<AbsanteeContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()) // unique DB per test
            .Options;

        Context = new AbsanteeContext(options);
        Mapper = new Mock<IMapper>();
    }

    public void Dispose()
    {
        Context.Dispose();
    }
}
