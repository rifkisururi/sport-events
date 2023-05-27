using sport_event.ViewModels.Organizer;

namespace sport_event.ViewModels
{
    public class EventModel
    {
        public int organizerId { get; set; }
        public OrganizerDto organizer { get; set; }
        public DateOnly eventDate { get; set; }
        public string eventType { get; set; }
        public string eventName { get; set; }

    }
}
