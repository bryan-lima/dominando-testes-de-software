using NerdStore.BDD.Tests.Config;
using NerdStore.BDD.Tests.Usuario;
using OpenQA.Selenium.Chrome;
using System;
using TechTalk.SpecFlow;

namespace NerdStore.BDD.Tests.Pedido
{
    [Binding]
    [CollectionDefinition(nameof(AutomacaoWebFixtureCollection))]
    public class Pedido_AdicionarItemAoCarrinhoSteps
    {
        private readonly AutomacaoWebTestsFixture _testsFixture;
        private readonly PedidoTela _pedidoTela;
        private readonly LoginUsuarioTela _loginUsuarioTela;
        private string _urlProduto;

        public Pedido_AdicionarItemAoCarrinhoSteps(AutomacaoWebTestsFixture testsFixture)
        {
            _testsFixture = testsFixture;
            _pedidoTela = new PedidoTela(testsFixture.BrowserHelper);
            _loginUsuarioTela = new LoginUsuarioTela(testsFixture.BrowserHelper);
        }

        [Given(@"O usuário esteja logado")]
        public void GivenOUsuarioEstejaLogado()
        {
            // Arrange
            var usuario = new Usuario.Usuario
            {
                Email = "teste@email.com",
                Senha = "Teste*123"
            };

            _testsFixture.Usuario = usuario;

            // Act
            var login = _loginUsuarioTela.Login(usuario);

            // Assert
            Assert.True(login);
        }

        [Given(@"Que um produto esteja na vitrine")]
        public void GivenQueUmProdutoEstejaNaVitrine()
        {
            // Arrange
            _pedidoTela.AcessarVitrineDeProdutos();

            // Act
            _pedidoTela.ObterDetalhesProduto();
            _urlProduto = _pedidoTela.ObterUrl();

            // Assert
            Assert.True(_pedidoTela.ValidarProdutoDisponivel());
        }

        [Given(@"Esteja disponível no estoque")]
        public void GivenEstejaDisponivelNoEstoque()
        {
            // Assert
            Assert.True(_pedidoTela.ObterQuantidadeNoEstoque() > 0);
        }

        [Given(@"Não tenha nenhum produto adicionado ao carrinho")]
        public void GivenNaoTenhaNenhumProdutoAdicionadoAoCarrinho()
        {
            // Act
            _pedidoTela.NavegarParaCarrinhoDeCompras();
            _pedidoTela.ZerarCarrinhoDeCompras();

            // Assert
            Assert.Equal(0, _pedidoTela.ObterValorTotalCarrinho());

            _pedidoTela.NavegarParaUrl(_urlProduto);
        }


        [When(@"O usuário adicionar uma unidade ao carrinho")]
        public void WhenOUsuarioAdicionarUmaUnidadeAoCarrinho()
        {
            // Act
            _pedidoTela.ClicarEmComprarAgora();
        }

        [Then(@"O usuário será redirecionado ao resumo da compra")]
        public void ThenOUsuarioSeraRedirecionadoAoResumoDaCompra()
        {
            // Assert
            Assert.True(_pedidoTela.ValidarSeEstaNoCarrinhoDeCompras());
        }

        [Then(@"O valor total do pedido será exatamente o valor do item adicionado")]
        public void ThenOValorTotalDoPedidoSeraExatamenteOValorDoItemAdicionado()
        {
            // Arrange
            var valorUnitario = _pedidoTela.ObterValorUnitarioProdutoCarrinho();
            var valorCarrinho = _pedidoTela.ObterValorTotalCarrinho();

            // Assert
            Assert.Equal(valorUnitario, valorCarrinho);
        }

        [When(@"O usuário adicionar um item acima da quantidade máxima permitida")]
        public void WhenOUsuarioAdicionarUmItemAcimaDaQuantidadeMaximaPermitida()
        {
            // Arrange
            _pedidoTela.ClicarAdicionarQuantidadeItens(Vendas.Domain.Pedido.MAX_UNIDADES_ITEM + 1);

            // Act
            _pedidoTela.ClicarEmComprarAgora();

            // Assert
        }

        [Then(@"Receberá uma mensagem de erro mencionando que foi ultrapassada a quantidade limite")]
        public void ThenReceberaUmaMensagemDeErroMencionandoQueFoiUltrapassadaAQuantidadeLimite()
        {
            // Arrange
            var mensagem = _pedidoTela.ObterMensagemDeErroProduto();

            // Assert
            Assert.Contains($"A quantidade máxima de um item é {Vendas.Domain.Pedido.MAX_UNIDADES_ITEM}", mensagem);
        }

        [Given(@"O mesmo produto já tenha sido adicionado ao carrinho anteriormente")]
        public void GivenOMesmoProdutoJaTenhaSidoAdicionadoAoCarrinhoAnteriormente()
        {
            // Act
            _pedidoTela.NavegarParaCarrinhoDeCompras();
            _pedidoTela.ZerarCarrinhoDeCompras();
            _pedidoTela.AcessarVitrineDeProdutos();
            _pedidoTela.ObterDetalhesProduto();
            _pedidoTela.ClicarEmComprarAgora();

            // Assert
            Assert.True(_pedidoTela.ValidarSeEstaNoCarrinhoDeCompras());

            _pedidoTela.VoltarNavegacao();
        }

        [Then(@"A quantidade de itens daquele produto terá sido acrescida em uma unidade a mais")]
        public void ThenAQuantidadeDeItensDaqueleProdutoTeraSidoAcrescidaEmUmaUnidadeAMais()
        {
            // Assert
            Assert.True(_pedidoTela.ObterQuantidadeDeItensPrimeiroProdutoCarrinho() > 1);
        }

        [Then(@"O valor total do pedido será a multiplicação da quantidade de itens pelo valor unitário")]
        public void ThenOValorTotalDoPedidoSeraAMultiplicacaoDaQuantidadeDeItensPeloValorUnitario()
        {
            // Arrange
            var valorUnitario = _pedidoTela.ObterValorUnitarioProdutoCarrinho();
            var valorCarrinho = _pedidoTela.ObterValorTotalCarrinho();
            var quantidadeUnidades = _pedidoTela.ObterQuantidadeDeItensPrimeiroProdutoCarrinho();

            // Assert
            Assert.Equal(valorUnitario * quantidadeUnidades, valorCarrinho);
        }

        [When(@"O usuário adicionar a quantidade máxima permitida ao carrinho")]
        public void WhenOUsuarioAdicionarAQuantidadeMaximaPermitidaAoCarrinho()
        {
            // Arrange
            _pedidoTela.ClicarAdicionarQuantidadeItens(Vendas.Domain.Pedido.MAX_UNIDADES_ITEM);

            // Act
            _pedidoTela.ClicarEmComprarAgora();

            // Assert
        }
    }
}
