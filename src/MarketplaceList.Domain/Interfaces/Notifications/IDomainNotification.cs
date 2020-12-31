using System.Collections.Generic;
using FluentValidation.Results;
using MarketplaceList.Domain.Notifications;

namespace MarketplaceList.Domain.Interfaces.Notifications
{
   public interface IDomainNotification
    {
        IReadOnlyCollection<NotificationMessage> Notifications { get; }
        bool HasNotifications { get; }
        void AddNotification(string key, string message);
        void AddNotifications(IEnumerable<NotificationMessage> notifications);
        void AddNotifications(ValidationResult validationResult);
    }
}