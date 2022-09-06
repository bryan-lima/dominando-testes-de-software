using NerdStore.BDD.Tests.Config;
using System;
using TechTalk.SpecFlow;

namespace NerdStore.BDD.Tests.Usuario
{
    [Binding]
    [CollectionDefinition(nameof(AutomacaoWebFixtureCollection))]
    public class CadastroDeUsuariosSteps
    {
        private readonly CadastroDeUsuarioTela _cadastroUsuarioTela;
        private readonly AutomacaoWebTestsFixture _testsFixture;

        public CadastroDeUsuariosSteps(AutomacaoWebTestsFixture testsFixture)
        {
            _testsFixture = testsFixture;
            _cadastroUsuarioTela = new CadastroDeUsuarioTela(_testsFixture.BrowserHelper);
        }

        [When(@"Ele clicar em registrar")]
        public void WhenEleClicarEmRegistrar()
        {
            // Act
            _cadastroUsuarioTela.ClicarNoLinkRegistrar();

            // Assert
            Assert.Contains(_testsFixture.Configuration.RegisterUrl, _cadastroUsuarioTela.ObterUrl());
        }

        [When(@"Preencher os dados do formulário")]
        public void WhenPreencherOsDadosDoFormulario(Table table)
        {
            // Arrange
            _testsFixture.GerarDadosUsuario();
            var usuario = _testsFixture.Usuario;

            // Act
            _cadastroUsuarioTela.PreencherFormularioRegistro(usuario);

            // Assert
            Assert.True(_cadastroUsuarioTela.ValidarPreenchimentoFormularioRegistro(usuario));

        }

        [When(@"Clicar no botão registrar")]
        public void WhenClicarNoBotaoRegistrar()
        {
            _cadastroUsuarioTela.ClicarNoBotaoRegistrar();
        }

        [When(@"Preencher os dados do formulário com uma senha sem maiúsculas")]
        public void WhenPreencherOsDadosDoFormularioComUmaSenhaSemMaiusculas(Table table)
        {
            // Arrange
            _testsFixture.GerarDadosUsuario();
            var usuario = _testsFixture.Usuario;
            usuario.Senha = "teste*123";

            // Act
            _cadastroUsuarioTela.PreencherFormularioRegistro(usuario);

            // Assert
            Assert.True(_cadastroUsuarioTela.ValidarPreenchimentoFormularioRegistro(usuario));
        }

        [Then(@"Ele receberá uma mensagem de erro que a senha precisa conter uma letra maiúscula")]
        public void ThenEleReceberaUmaMensagemDeErroQueASenhaPrecisaConterUmaLetraMaiuscula()
        {
            Assert.True(_cadastroUsuarioTela.ValidarMensagemDeErroFormulario("Passwords must have at least one uppercase ('A'-'Z')"));
        }

        [When(@"Preencher os dados do formulário com uma senha sem caractere especial")]
        public void WhenPreencherOsDadosDoFormularioComUmaSenhaSemCaractereEspecial(Table table)
        {
            // Arrange
            _testsFixture.GerarDadosUsuario();
            var usuario = _testsFixture.Usuario;
            usuario.Senha = "Teste123";

            // Act
            _cadastroUsuarioTela.PreencherFormularioRegistro(usuario);

            // Assert
            Assert.True(_cadastroUsuarioTela.ValidarPreenchimentoFormularioRegistro(usuario));
        }

        [Then(@"Ele receberá uma mensagem de erro que a senha precisa conter um caractere especial")]
        public void ThenEleReceberaUmaMensagemDeErroQueASenhaPrecisaConterUmCaractereEspecial()
        {
            Assert.True(_cadastroUsuarioTela.ValidarMensagemDeErroFormulario("Passwords must have at least one non alphanumeric character"));
        }

        [When(@"Ele será redirecionado para página de confirmação de conta")]
        public void WhenEleSeraRedirecionadoParaPaginaDeConfirmacaoDeConta()
        {
            Assert.Contains("/Identity/Account/RegisterConfirmation", _cadastroUsuarioTela.ObterUrl());
        }

        [When(@"Clicar no link para confirmar conta")]
        public void WhenClicarNoLinkParaConfirmarConta()
        {
            // Act
            _cadastroUsuarioTela.ClicarNoLinkConfirmaConta();

            // Assert
            Assert.Contains("Identity/Account/ConfirmEmail", _cadastroUsuarioTela.ObterUrl());
        }

        [Then(@"Será exibido uma mensagem de conta confirmada")]
        public void ThenSeraExibidoUmaMensagemDeContaConfirmada()
        {
            var mensagem = _testsFixture.BrowserHelper.ObterElementoPorClasse("alert-success");

            Assert.Contains("Thank you for confirming your email", mensagem.GetAttribute("innerText"));
        }

    }
}
