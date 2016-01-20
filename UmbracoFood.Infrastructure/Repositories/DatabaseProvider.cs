using Umbraco.Core;
using Umbraco.Core.Persistence;

namespace UmbracoFood.Infrastructure.Repositories
{
    public class DatabaseProvider : IDatabaseProvider
    {

        public DatabaseProvider()
        {
            Db = ApplicationContext.Current.DatabaseContext.Database;
        }

        public UmbracoDatabase Db { get; set; }
    }
}