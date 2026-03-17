using Florama.Domain.Abstractions;
using Florama.Domain.Notifications;

namespace Florama.Application.Notifications
{
    public class DomainNotificationHandler : IHandler<DomainNotification>, IDisposable
    {
        private readonly List<DomainNotification> _notifications = new();

        public void Handle(DomainNotification message)
        {
            _notifications.Add(message);
        }

        public bool HasNotifications()
        {
            return _notifications.Any();
        }

        public List<DomainNotification> GetNotifications()
        {
            return _notifications;
        }

        public void Dispose()
        {
            _notifications.Clear();
        }
    }
}
