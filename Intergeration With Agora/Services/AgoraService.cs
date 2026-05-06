

using Intergeration_With_Agora.Models;
using Microsoft.Extensions.Options;

namespace Intergeration_With_Agora.Services
{
    public class AgoraService(IOptions<IgoreSettings>options) : IAgoraService
    {
       
        public string GenerateChannelName(Guid sessionId)
        {
            return $"course-session-{sessionId}";
        }

        public string GenerateRtcToken(string channelName, string userId)
        {

            uint expirationTimeInSeconds = 3600;
            uint currentTimeStamp = (uint)DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            uint privilegeExpiredTs = currentTimeStamp + expirationTimeInSeconds;

            return AgoraTokenBuilder.BuildToken(
                options.Value.AppId,
                options.Value.AppCertificate,
                channelName,
                userId,
                privilegeExpiredTs);
        }

       
    }
}
