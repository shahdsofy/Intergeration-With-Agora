using Intergeration_With_Agora.Constants;
using System.ComponentModel.DataAnnotations.Schema;

namespace Intergeration_With_Agora.Models
{
    public class LiveSession
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string ChannelName { get; set; } = string.Empty;
        public DateTime ScheduledStart { get; set; }
        public int Duration { get; set; }
        public LiveSessionStatus Status { get; set; } = LiveSessionStatus.Scheduled;
        public DateTime CreatedAt { get; set; }
        public Guid CourseId { get; set; }
        public Course Course { get; set; }
        public List<SessionRecording> Recordings { get; set; }

        public string InstructorId { get; set; }
        public User Instructor { get; set; }
    }
}
