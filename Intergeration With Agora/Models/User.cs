using Microsoft.AspNetCore.Identity;

namespace Intergeration_With_Agora.Models
{
    public class User:IdentityUser
    {
        public ICollection<LiveSession> ManagedSessions { get; set; } = new List<LiveSession>();
    }


}
