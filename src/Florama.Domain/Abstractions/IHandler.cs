namespace Florama.Domain.Abstractions
{
    public interface IHandler<T>
    {
        void Handle(T message);
        bool HasNotifications();
        List<T> GetNotifications();
    }
}
