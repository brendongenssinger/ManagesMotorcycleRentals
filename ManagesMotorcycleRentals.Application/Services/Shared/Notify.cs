namespace ManagesMotorcycleRentals.Domain.Shared
{
    public class Notify
    {
        private readonly List<NotificationDomain> _notifications = new();
        public int StatusCode { get; set; }

        public IReadOnlyCollection<NotificationDomain> Notifications => _notifications.AsReadOnly();

        public bool HasNotifications => _notifications.Any();

        public void AddNotification(string key, string message)
        {
            _notifications.Add(new NotificationDomain(key, message));
        }

        public void AddNotifications(IEnumerable<NotificationDomain> notifications)
        {
            _notifications.AddRange(notifications);
        }
    }
}
