namespace AMMAAPI.Database
{
    public class AMMADatabaseSettings
    {
        public required string ConnectionString { get; set; }
        public required string DatabaseName { get; set; }
        public required CollectionName CollectionName { get; set; }
    }
}
