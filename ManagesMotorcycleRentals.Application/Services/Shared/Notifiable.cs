namespace ManagesMotorcycleRentals.Domain.Shared
{
    public class Notifiable
    {
        private readonly List<Notification> _notifications = new();
        public int StatusCode { get; set; }

        public IReadOnlyCollection<Notification> Notifications => _notifications.AsReadOnly();

        public bool HasNotifications => _notifications.Any();

        public void AddNotification(string key, string message)
        {
            _notifications.Add(new Notification(key, message));
        }

        public void AddNotifications(IEnumerable<Notification> notifications)
        {
            _notifications.AddRange(notifications);
        }
    }
}
