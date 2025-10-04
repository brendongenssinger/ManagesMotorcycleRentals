using ManagesMotorcycleRentals.Domain.Shared;

namespace ManagesMotorcycleRentals.Application
{
    public abstract class ServiceBase
    {
        private readonly Notify _notification;
        protected ServiceBase(Notify notification)
        {
            _notification = notification;
        }

        public Notify GetNotification() => _notification;

        public void AddNotification(string key, string message) => _notification.AddNotification(key, message);

    }
}
