using System;
using System.Net.Http;
using BddWithReqnroll.GeekPizza.Web;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BddWithReqnroll.GeekPizza.Specs.Support
{
    public class AppHostingContext : IDisposable
    {
        class TestAppFactory : WebApplicationFactory<Startup>
        {
            protected override void ConfigureWebHost(IWebHostBuilder builder)
            {
                base.ConfigureWebHost(builder);
                builder.ConfigureLogging((_, loggingBuilder) =>
                {
                    loggingBuilder.ClearProviders();
                    loggingBuilder.AddDebug();
                });
            }
        }

        private static WebApplicationFactory<Startup> _webApplicationFactory;

        public HttpClient CreateClient()
        {
            StartApp();

            Assert.IsNotNull(_webApplicationFactory, "the app should be running");
            return _webApplicationFactory.CreateClient();
        }

        public void Dispose()
        {
            //nop
        }

        public void StartApp()
        {
            if (_webApplicationFactory == null)
            {
                Console.WriteLine("Starting Web Application...");
                _webApplicationFactory = new TestAppFactory();
            }
        }

        public static void StopApp()
        {
            if (_webApplicationFactory != null)
            {
                Console.WriteLine("Shutting down Web Application...");
                _webApplicationFactory.Dispose();
                _webApplicationFactory = null;
            }
        }
    }
}
