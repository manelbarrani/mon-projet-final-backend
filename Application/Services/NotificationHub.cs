using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Memory;

namespace Application.Services
{
    public class NotificationHub : Hub
    {
        private IMemoryCache _cache;
        private IHubContext<NotificationHub> _hubContext;

        public NotificationHub(IMemoryCache cache, IHubContext<NotificationHub> hubContext)
        {
            _cache = cache;
            _hubContext = hubContext;
        }
    }
}
