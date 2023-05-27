namespace sport_event.ViewModels.Event
{
    public class OrganizerDTO
    {
        public int Id { get; set; }
        public string OrganizerName { get; set; }
        public string ImageLocation { get; set; }
    }

    public class EventDTO
    {
        public int? Id { get; set; }
        public DateTime? EventDate { get; set; }
        public string? EventName { get; set; }
        public string? EventType { get; set; }
        public OrganizerDTO? Organizer { get; set; }
        public string? Message { get; set; }
        public Dictionary<string, List<string>>? Errors { get; set; }
        public int? StatusCode { get; set; }
    }

    public class PaginationDTO
    {
        public int Total { get; set; }
        public int Count { get; set; }
        public int PerPage { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public object Links { get; set; }
    }

    public class EventsResponseDTO
    {
        public List<EventDTO> Data { get; set; }
        public PaginationDTO Meta { get; set; }
    }
}
