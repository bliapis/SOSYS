using System;
using LT.SO.Domain.Core.Events;

namespace LT.SO.Domain.Core.Notifications
{
    public class DomainNotification : Event
    {
        public Guid DomainNotificationId { get; private set; }
        public string Key { get; private set; }
        public string Value { get; private set; }
        public int Version { get; private set; }

        public DomainNotification(string key, string value, Guid aggregateId)
        {
            DomainNotificationId = Guid.NewGuid();
            Key = key;
            Value = value;
            Version = 1;
            AggregateId = aggregateId;
        }

        public DomainNotification(string key, string value)
        {
            DomainNotificationId = Guid.NewGuid();
            Key = key;
            Value = value;
            Version = 1;
        }
    }
}