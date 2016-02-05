namespace UmbracoFood.Interfaces
{
    public interface IUserDetailsService
    {
        string GetUserKey(string userName);
        string GetUserName(string userKey);
    }
}