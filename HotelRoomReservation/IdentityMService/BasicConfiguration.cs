using DataModel;

namespace IdentityMService
{
    public class BasicConfiguration : BaseConfiguration
    {
        public string ConnectionString { get; private set; }

        public BasicConfiguration(IConfiguration configuration) : base(configuration) { }
    }
}
