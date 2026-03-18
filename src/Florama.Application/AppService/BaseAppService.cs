using Florama.Domain.Abstractions;
using Florama.Domain.Notifications;
using FluentValidation;

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

        protected void NotificarErro(Exception ex)
        {
            _notifications.Handle(new DomainNotification(ex.GetType().Name, ex.Message));
        }

        protected bool Validar<TEntity>(IValidator<TEntity> validator, TEntity entity)
            where TEntity : class
        {
            var result = validator.Validate(entity);

            if (result.IsValid)
                return true;

            foreach (var error in result.Errors)
            {
                NotificarErro(error.ErrorMessage);
            }

            return false;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
