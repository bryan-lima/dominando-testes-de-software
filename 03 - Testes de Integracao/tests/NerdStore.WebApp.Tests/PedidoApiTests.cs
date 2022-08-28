using Features.Tests;
using NerdStore.WebApp.MVC;
using NerdStore.WebApp.MVC.Models;
using NerdStore.WebApp.Tests.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace NerdStore.WebApp.Tests
{
    [TestCaseOrderer("Features.Tests.PriorityOrderer", "Features.Tests")]
    [Collection(nameof(IntegrationApiTestsFixtureCollection))]
    public class PedidoApiTests
    {
        private readonly IntegrationTestsFixture<StartupApiTests> _testsFixture;

        public PedidoApiTests(IntegrationTestsFixture<StartupApiTests> testsFixture)
        {
            _testsFixture = testsFixture;
        }

        [Fact(DisplayName = "Adicionar Item em Novo Pedido"), TestPriority(1)]
        [Trait("Categoria", "Integração API - Pedido")]
        public async Task AdicionarItem_NovoPedido_DeveRetornarComSucesso()
        {
            // Arrange
            var itemInfo = new ItemViewModel
            {
                Id = new Guid("A1BD239D-DF19-4305-B413-41F7C8CAC7E9"),
                Quantidade = 2
            };

            await _testsFixture.RealizarLoginApi();
            _testsFixture.Client.AtribuirToken(_testsFixture.UsuarioToken);

            // Act
            var postResponse = await _testsFixture.Client.PostAsJsonAsync("api/carrinho", itemInfo);

            // Assert
            postResponse.EnsureSuccessStatusCode();
        }

        [Fact(DisplayName = "Remover Item em Pedido Existente"), TestPriority(2)]
        [Trait("Categoria", "Integração API - Pedido")]
        public async Task RemoverItem_PedidoExistente_DeveRetornarComSucesso()
        {
            // Arrange
            var produtoId = new Guid("A1BD239D-DF19-4305-B413-41F7C8CAC7E9");
            await _testsFixture.RealizarLoginApi();
            _testsFixture.Client.AtribuirToken(_testsFixture.UsuarioToken);

            // Act
            var deleteResponse = await _testsFixture.Client.DeleteAsync($"api/carrinho/{produtoId}");

            // Assert
            deleteResponse.EnsureSuccessStatusCode();
        }
    }
}
