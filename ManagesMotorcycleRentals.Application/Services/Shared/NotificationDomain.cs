namespace ManagesMotorcycleRentals.Domain.Shared
{
    public class NotificationDomain
    {
       
        public string Key { get; set; }
        public string Message { get; set; }

        public NotificationDomain(string key, string message)
        {
            Key = key;
            Message = message;
        }
    }
}
