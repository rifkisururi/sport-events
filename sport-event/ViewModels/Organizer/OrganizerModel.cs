namespace sport_event.ViewModels.Organizer
{
    public class OrganizerDto
    {
        public int id { get; set; }
        public string organizerName { get; set; }
        public string imageLocation { get; set; }
        public string? Message { get; set; }
        public Dictionary<string, List<string>>? Errors { get; set; }
        public int? StatusCode { get; set; }
    }

    public class PaginationDto
    {
        public int Total { get; set; }
        public int Count { get; set; }
        public int PerPage { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public PaginationLinksDto Links { get; set; }
    }

    public class PaginationLinksDto
    {
        public string Next { get; set; }
    }

    public class OrganizersResponseDto
    {
        public List<OrganizerDto> Data { get; set; }
        public MetaDto Meta { get; set; }
    }

    public class MetaDto
    {
        public PaginationDto Pagination { get; set; }
    }
}
