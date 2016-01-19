namespace UmbracoFood.ViewModels
{
    public class EditRestaurantViewModel 
    {
       public int ID { get; set; }
       public string Name { get; set; }
       public string Phone { get; set; }
       public string WebsiteUrl { get; set; }
       public string MenuUrl { get; set; }
       public bool IsActive { get; set; }
    }
}