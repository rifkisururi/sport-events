namespace sport_event.ViewModels.Auth
{
    public class UserModel : LoginModel
    {
        public int id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        
    }
}
