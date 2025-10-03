using ManagesMotorcycleRentals.Domain.Shared;

namespace ManagesMotorcycleRentals.Application.Services.Validator
{
    public class UserServiceValidator : ValidatorBase
    {
        public UserServiceValidator(Notifiable notifiable) : base(notifiable)
        {
          
        }
        public UserServiceValidator ValidUserId(int userId)
        {
            if (userId <= 0)
            {
                _notifiable.AddNotification("UserId", "User ID must be greater than zero.");
            }

            return this;
        }

        public UserServiceValidator ValidRole(string role)
        {
            if(role !=  "admin" && role != "user")
                _notifiable.AddNotification("Role", "Role must be either 'admin' or 'user'.");
            return this;
        }
    }
}
