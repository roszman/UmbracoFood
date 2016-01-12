using Umbraco.Core;
using Umbraco.Core.Persistence;

namespace UmbracoFood.Infrastructure.Repositories
{
    public class BaseRepository
    {
        protected readonly UmbracoDatabase db;

        public BaseRepository()
        {
            db = ApplicationContext.Current.DatabaseContext.Database;
        }
    }
}