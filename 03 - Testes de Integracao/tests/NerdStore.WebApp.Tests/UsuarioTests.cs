using NerdStore.WebApp.MVC;
using NerdStore.WebApp.Tests.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace NerdStore.WebApp.Tests
{
    [Collection(nameof(IntegrationWebTestsFixtureCollection))]
    public class UsuarioTests
    {
        private readonly IntegrationTestsFixtures<StartupWebTests> _testsFixture;

        public UsuarioTests(IntegrationTestsFixtures<StartupWebTests> testsFixture)
        {
            _testsFixture = testsFixture;
        }

        [Fact(DisplayName = "Realizar Cadastro com Sucesso")]
        [Trait("Categoria", "Integração Web - Usuário")]
        public async Task Usuario_RealizarCadastro_DeveExecutarComSucesso()
        {
            // Arrange
            var initialResponse = await _testsFixture.Client.GetAsync("/Identity/Account/Register");
            initialResponse.EnsureSuccessStatusCode();

            var email = "teste2@teste.com";

            var formData = new Dictionary<string, string> 
            {
                { "Input.Email", email },
                { "Input.Password", "Teste*123" },
                { "Input.ConfirmPassword", "Teste*123" }
            };

            var postRequest = new HttpRequestMessage(HttpMethod.Post, "/Identity/Account/Register")
            {
                Content = new FormUrlEncodedContent(formData)
            };

            // Act
            var postResponse = await _testsFixture.Client.SendAsync(postRequest);

            // Assert
            var responseString = await postRequest.Content.ReadAsStringAsync();

            postResponse.EnsureSuccessStatusCode();
            Assert.Contains($"Hello {email}!", responseString);
        }
    }
}
