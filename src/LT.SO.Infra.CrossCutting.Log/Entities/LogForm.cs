namespace LT.SO.Infra.CrossCutting.Log.Entities
{
    public class LogForm
    {
        public LogForm(string key, string value)
        {
            Key = key;
            Value = value;
        }

        public string Key { get; set; }
        public string Value { get; set; }
    }
}
