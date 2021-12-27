using EfCorePublicIdDemo.Application.Common;
using IdGen;
using Microsoft.Extensions.Options;

namespace EfCorePublicIdDemo.Infrastructure.Services;

public class UniqueIdGenerator : IUniqueIdGenerator
{
    private readonly IdGenerator _generator;

    public UniqueIdGenerator(IOptions<IdGeneratorSettings> settings)
    {
        _generator = new IdGenerator(settings.Value.RunnerId);
    }

    public string CreateId() => _generator.CreateId().ToString();
}

public class IdGeneratorSettings
{
    public int RunnerId { get; set; }
}