using Microsoft.AspNetCore.Mvc.Testing;
using NerdStore.WebApp.MVC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NerdStore.WebApp.Tests.Config
{
    [CollectionDefinition(nameof(IntegrationWebTestsFixtureCollection))]
    public class IntegrationWebTestsFixtureCollection : ICollectionFixture<IntegrationTestsFixtures<StartupWebTests>> 
    { 

    }

    [CollectionDefinition(nameof(IntegrationApiTestsFixtureCollection))]
    public class IntegrationApiTestsFixtureCollection : ICollectionFixture<IntegrationTestsFixtures<StartupApiTests>>
    {

    }

    public class IntegrationTestsFixtures<TStartup> : IDisposable where TStartup : class
    {
        public readonly LojaAppFactory<TStartup> Factory;
        public HttpClient Client;

        public IntegrationTestsFixtures()
        {
            var clienteOptions = new WebApplicationFactoryClientOptions
            {

            };

            Factory = new LojaAppFactory<TStartup>();
            Client = Factory.CreateClient(clienteOptions);
        }

        public void Dispose()
        {
            Client.Dispose();
            Factory.Dispose();
        }
    }
}
