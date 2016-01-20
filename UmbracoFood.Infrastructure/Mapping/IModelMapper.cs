namespace UmbracoFood.Infrastructure.Mapping
{
    public interface IModelMapper<Domain, Poco>
    {
        Domain MapToDomain(Poco poco);
        Poco MapToPoco(Domain domain);

    }
}