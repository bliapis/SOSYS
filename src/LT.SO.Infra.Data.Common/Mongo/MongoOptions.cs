namespace LT.SO.Infra.Data.Common.Mongo
{
    public class MongoOptions
    {
        public string ConnectionString { get; set; }
        public string Database { get; set; }
        public bool Seed { get; set; }
    }
}