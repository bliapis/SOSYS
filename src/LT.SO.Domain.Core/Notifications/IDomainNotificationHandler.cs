using System.Collections.Generic;
using LT.SO.Domain.Core.Interfaces;

namespace LT.SO.Domain.Core.Notifications
{
    public interface IDomainNotificationHandler<T> : IHandler<T> where T : Message
    {
        bool HasNotifications();
        List<T> GetNotifications();
    }
}