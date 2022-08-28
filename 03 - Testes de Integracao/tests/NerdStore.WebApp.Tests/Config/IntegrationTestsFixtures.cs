using Bogus;
using Microsoft.AspNetCore.Mvc.Testing;
using NerdStore.WebApp.MVC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        public string AntiForgeryFieldName = "__RequestVerificationToken";

        public string UsuarioEmail;
        public string UsuarioSenha;

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

        public void GerarUserSenha()
        {
            var faker = new Faker("pt_BR");
            UsuarioEmail = faker.Internet.Email().ToLower();
            UsuarioSenha = faker.Internet.Password(length: 8, memorable: false, regexPattern: "", prefix: "@1Ab_");
        }

        public string ObterAntiForgeryToken(string htmlBody)
        {
            var requestVerificationTokenMatch = Regex.Match(htmlBody, $@"\<input name=""{AntiForgeryFieldName}"" type=""hidden"" value=""([^""]+)"" \/\>");

            if (requestVerificationTokenMatch.Success)
            {
                return requestVerificationTokenMatch.Groups[1].Captures[0].Value;
            }

            throw new ArgumentException($"Anti forgery token '{AntiForgeryFieldName}' não encontrado no HTML", nameof(htmlBody));
        }

        public void Dispose()
        {
            Client.Dispose();
            Factory.Dispose();
        }
    }
}
