using System.Security.Claims;

namespace Nettbutikk.Data.SessionData
{
    public interface ICallContext
    {
        public string ConnectionId { get; }
        public string? UserIdentifier { get; }
        public ClaimsPrincipal? User { get; }
    }
}
