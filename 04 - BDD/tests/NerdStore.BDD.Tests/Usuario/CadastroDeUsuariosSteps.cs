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
            throw new PendingStepException();
        }

        [Then(@"Ele receberá uma mensagem de erro que a senha precisa conter uma letra maiúscula")]
        public void ThenEleReceberaUmaMensagemDeErroQueASenhaPrecisaConterUmaLetraMaiuscula()
        {
            throw new PendingStepException();
        }

        [When(@"Preencher os dados do formulário com uma senha sem caractere especial")]
        public void WhenPreencherOsDadosDoFormularioComUmaSenhaSemCaractereEspecial(Table table)
        {
            throw new PendingStepException();
        }

        [Then(@"Ele receberá uma mensagem de erro que a senha precisa conter um caractere especial")]
        public void ThenEleReceberaUmaMensagemDeErroQueASenhaPrecisaConterUmCaractereEspecial()
        {
            throw new PendingStepException();
        }
    }
}
