using Xunit;

namespace UmbracoFood.Tests.Repositories.DatabaseFixtures
{
    [CollectionDefinition("Database collection")]
    public class DatabaseFixtureCollection : ICollectionFixture<OrdersDatabaseFixture>
    {
    }
}
