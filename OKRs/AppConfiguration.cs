
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
        //public string HostUrl { get; set; }
        //public string Password { get; set; }
        //public string UserCollection { get; set; }
    }
}
