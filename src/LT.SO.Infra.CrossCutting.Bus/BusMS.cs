

using System;
using System.Threading.Tasks;
using LT.SO.Domain.Core;
using LT.SO.Domain.Core.Bus;
using RawRabbit;

namespace LT.SO.Infra.CrossCutting.Bus
{
    public class BusMS : IBusMS
    {
        private readonly IBusClient _bus;

        public BusMS(IBusClient bus)
        {
            _bus = bus;
        }

        public async Task PublishAsync<T>(T message) where T : Message
        {
            try
            {
                await _bus.PublishAsync(message);
            }
            catch (Exception ex)
            {
                var e = ex;
                var m = ex.Message;
            }
        }
    }
}