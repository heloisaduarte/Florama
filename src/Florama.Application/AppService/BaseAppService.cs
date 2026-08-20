using Florama.Domain.Abstractions;
using Florama.Domain.Notifications;

namespace Florama.Application.AppService
{
    public abstract class BaseAppService : IDisposable
    {
        protected readonly IUnitOfWork _uow;
        protected readonly IHandler<DomainNotification> _notifications;

        protected BaseAppService(
            IUnitOfWork uow,
            IHandler<DomainNotification> notifications)
        {
            _uow = uow;
            _notifications = notifications;
        }

        public async Task<bool> CommitAsync()
        {
            if (PossuiNotificacoes())
                return false;

            await _uow.CommitAsync();
            return true;
        }

        protected bool PossuiNotificacoes()
        {
            return _notifications.HasNotifications();
        }

        protected void NotificarErro(string mensagem)
        {
            _notifications.Handle(new DomainNotification("Error", mensagem));
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}