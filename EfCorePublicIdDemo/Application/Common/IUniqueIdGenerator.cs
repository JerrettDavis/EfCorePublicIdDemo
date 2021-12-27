namespace EfCorePublicIdDemo.Application.Common;

public interface IUniqueIdGenerator
{
    /// <summary>
    /// Creates a globally-unique ID.
    /// </summary>
    /// <returns>A string representing a globally-unique ID</returns>
    string CreateId();
}