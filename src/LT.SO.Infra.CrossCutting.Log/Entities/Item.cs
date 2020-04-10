using System;
using LT.SO.Domain.Core.Models;

namespace LT.SO.Infra.CrossCutting.Log.Entities
{
    public abstract class Item : Entity<Item>
    {
        public Item(string key, string value)
        {
            Key = key;
            Value = value;
        }

        public string Key { get; set; }
        public string Value { get; set; }
    }
}