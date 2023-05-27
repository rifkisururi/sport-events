namespace sport_event.ViewModels.Auth
{
    public class CallBackLoginModel
    {
        public int? id { get; set; }
        public string? email { get; set; }
        public string? token { get; set; }
        public string? Message { get; set; }
        public Dictionary<string, List<string>>? Errors { get; set; }
        public int? StatusCode { get; set; }
        public string? error { get; set;}
    }
}
