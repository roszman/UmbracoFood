using Umbraco.Core.Persistence;

namespace UmbracoFood.Infrastructure.Repositories
{
    public interface IDatabaseProvider
    {
        UmbracoDatabase Db { get; set; }
    }
}