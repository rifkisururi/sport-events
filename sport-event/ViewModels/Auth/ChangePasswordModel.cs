namespace sport_event.ViewModels.Auth
{
    public class ChangePasswordModel
    {
        public string oldPassword { get; set; }
        public string newPassword { get; set; }
        public string repeatPassword { get; set; }
    }
}
