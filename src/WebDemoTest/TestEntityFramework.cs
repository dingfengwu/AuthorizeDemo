using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.TestHost;
using Microsoft.Extensions.Logging;
using WebApplication3;
using Xunit;

namespace WebDemoTest
{
    // This project can output the Class library as a NuGet Package.
    // To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".
    public class TestEntityFramework
    {
        TestServer _server;
        Startup _start;
        public TestEntityFramework()
        {
            CreateServer();
        }

        [Fact]
        public void TestBlukInsert()
        {

        }

        private void CreateServer()
        {
            _start = new Startup(null);
            _server = TestServer.Create((IApplicationBuilder builder) =>
            {
                var env = builder.ApplicationServices.GetService<IHostingEnvironment>();
                var logger = builder.ApplicationServices.GetService<ILoggerFactory>();
                _start.Configure(builder, env, logger);
            },
                _start.ConfigureServices);
        }

    }
}
