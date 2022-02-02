using Microsoft.Extensions.Options;

namespace OKRs.Web.Services
{
    public class ProgramVersion
    {
        private readonly OKRsConfiguration configuration;

        public ProgramVersion(IOptions<OKRsConfiguration> configuration)
        {
            this.configuration = configuration.Value;
        }
        public string Version()
        {
            return configuration.ProgramVersion;
        }
    }
}
