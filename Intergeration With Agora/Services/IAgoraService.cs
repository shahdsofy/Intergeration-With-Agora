namespace Intergeration_With_Agora.Services
{
    public interface IAgoraService
    {
          string GenerateRtcToken(string channelName, string userId);
          string GenerateChannelName(Guid sessionId);
    }
}
