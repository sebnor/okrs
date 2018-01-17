
namespace OKRs
{
    public class AppConfiguration
    {
        public DataConfiguration Database { get; set; }
    }

    public class DataConfiguration
    {
        public string ConnectionString { get; set; }
        public string Name { get; set; }
        public string ObjectivesCollection { get; set; }
        public string UserCollection { get; set; }
    }
}
