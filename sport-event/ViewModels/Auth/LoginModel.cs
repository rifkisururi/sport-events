namespace sport_event.ViewModels.Auth
{
    public class LoginModel
    {
        public string email { get; set; }
        public string password { get; set; }
        public string repeatPassword { get; set; }
        public string oldPassword { get; set; }
        public string newPassword { get; set; }
    }
}
