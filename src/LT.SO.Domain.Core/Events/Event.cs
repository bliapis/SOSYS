using System;

namespace LT.SO.Domain.Core.Events
{
    public abstract class Event : Message
    {
        public DateTime TimeStamp { get; private set; }

        protected Event()
        {
            TimeStamp = DateTime.Now;
        }
    }
}