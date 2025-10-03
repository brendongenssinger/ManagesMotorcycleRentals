using ManagesMotorcycleRentals.Domain.Shared;

namespace ManagesMotorcycleRentals.Application
{
    public abstract class ServiceBase
    {
        private readonly Notifiable _notification;
        protected ServiceBase(Notifiable notification)
        {
            _notification = notification;
        }

        public Notifiable GetNotifiable() => _notification;

        public void AddNotification(string key, string message) => _notification.AddNotification(key, message);

    }
}
