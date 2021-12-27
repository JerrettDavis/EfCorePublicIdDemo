using System.Text.Json;
using EfCorePublicIdDemo.Domain.Common;
using JetBrains.Annotations;

namespace EfCorePublicIdDemo.Domain.Entities;

[PublicAPI]
public class MyEntity : IPublicEntity, IEquatable<MyEntity>
{

#pragma warning disable CS0649
    private readonly int _id;
#pragma warning restore CS0649
    
    public MyEntity()
    {
        Name = Guid.NewGuid();
    }

    public Guid Name { get; private set; }
    public string PublicId { get; } = null!;

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }

    public bool Equals(MyEntity? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return _id == other._id;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        return obj.GetType() == GetType() && Equals((MyEntity)obj);
    }

    public override int GetHashCode()
    {
        return _id;
    }

    public static bool operator ==(MyEntity? left, MyEntity? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(MyEntity? left, MyEntity? right)
    {
        return !Equals(left, right);
    }
}