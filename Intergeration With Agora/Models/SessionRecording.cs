using Intergeration_With_Agora.Constants;
using System.ComponentModel.DataAnnotations;

namespace Intergeration_With_Agora.Models
{
    public class SessionRecording
    {
        public Guid Id { get; set; }
        public string ResourceId { get; set; } = string.Empty;
        
        public string Sid { get; set; } = string.Empty;
        public RecordingStatus  Status { get; set; }
        // S3 data
        public string? FileName { get; set; } 
        public string? FileUrl { get; set; } 
        public long? Size { get; set; }
        public int? Duration { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid SessionId { get; set; }
        public LiveSession Session { get; set; }
    }
}
