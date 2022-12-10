namespace Minimal.Api.Configurations
{
    public class Config : IConfig
    {
        private readonly IConfiguration _configuration;
        public Config(IConfiguration configuration) 
        {
            _configuration = configuration;
        }

        public string GetConnectionString()
        {
            return _configuration.GetConnectionString("Default");
        }
    }
}
