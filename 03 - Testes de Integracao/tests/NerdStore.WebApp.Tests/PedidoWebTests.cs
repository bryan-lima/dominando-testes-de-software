using AngleSharp.Html.Parser;
using NerdStore.WebApp.MVC;
using NerdStore.WebApp.Tests.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NerdStore.WebApp.Tests
{
    [Collection(nameof(IntegrationWebTestsFixtureCollection))]
    public class PedidoWebTests
    {
        private readonly IntegrationTestsFixture<StartupWebTests> _testsFixture;

        public PedidoWebTests(IntegrationTestsFixture<StartupWebTests> testsFixture)
        {
            _testsFixture = testsFixture;
        }

        [Fact(DisplayName = "Adicionar Item em Novo Pedido")]
        [Trait("Categoria", "Integração Web - Pedido")]
        public async Task AdicionarItem_NovoPedido_DeveAtualizarValorTotal()
        {
            // Arrange
            var produtoId = new Guid("A1BD239D-DF19-4305-B413-41F7C8CAC7E9");
            const int quantidade = 2;

            var initialResponse = await _testsFixture.Client.GetAsync($"/produto-detalhe/{produtoId}");
            initialResponse.EnsureSuccessStatusCode();

            var formData = new Dictionary<string, string>
            {
                { "id", produtoId.ToString() },
                { "quantidade", quantidade.ToString() }
            };

            await _testsFixture.RealizarLoginWeb();

            var postRequest = new HttpRequestMessage(HttpMethod.Post, "/meu-carrinho")
            {
                Content = new FormUrlEncodedContent(formData)
            };

            // Act
            var postResponse = await _testsFixture.Client.SendAsync(postRequest);

            // Assert
            postResponse.EnsureSuccessStatusCode();

            var html = new HtmlParser()
                .ParseDocumentAsync(await postResponse.Content.ReadAsStringAsync())
                .Result
                .All;

            var formQuantidade = html?.FirstOrDefault(c => c.Id == "quantidade")?.GetAttribute("value")?.ApenasNumeros();
            var valorUnitario = html?.FirstOrDefault(c => c.Id == "valorUnitario")?.TextContent.Split(".")[0]?.ApenasNumeros();
            var valorTotal = html?.FirstOrDefault(c => c.Id == "valorTotal")?.TextContent.Split(".")[0]?.ApenasNumeros();

            Assert.Equal(valorTotal, formQuantidade * valorUnitario);
        }
    }
}
