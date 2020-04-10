using System;

namespace LT.SO.Infra.CrossCutting.Log.Entities
{
    public class LogCookie
    {
        public LogCookie(string key, string value)
        {
            Key = key;
            Value = value;
        }

        public string Key { get; set; }
        public string Value { get; set; }
    }
}