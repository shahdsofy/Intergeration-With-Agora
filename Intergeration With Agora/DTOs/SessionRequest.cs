namespace Intergeration_With_Agora.DTOs
{
    public class SessionRequest
    {
        public string Title { get; set; } = string.Empty;
        public Guid CourseId { get; set; }
        public DateTime ScheduledStart { get; set; }
        public int DurationInMinutes { get; set; }
    }
}
