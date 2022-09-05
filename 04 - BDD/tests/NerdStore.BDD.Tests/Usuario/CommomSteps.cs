using NerdStore.BDD.Tests.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace NerdStore.BDD.Tests.Usuario
{
    [Binding]
    [CollectionDefinition(nameof(AutomacaoWebFixtureCollection))]
    public class CommomSteps
    {
        private readonly CadastroDeUsuarioTela _cadastroUsuarioTela;
        private readonly AutomacaoWebTestsFixture _testsFixture;

        public CommomSteps(AutomacaoWebTestsFixture testsFixture)
        {
            _testsFixture = testsFixture;
            _cadastroUsuarioTela = new CadastroDeUsuarioTela(testsFixture.BrowserHelper);
        }

        [Given(@"Que o visitante está acessando o site da loja")]
        public void GivenQueOVisitanteEstaAcessandoOSiteDaLoja()
        {
            // Act
            _cadastroUsuarioTela.AcessarSiteLoja();

            // Assert
            Assert.Contains(_testsFixture.Configuration.DomainUrl, _cadastroUsuarioTela.ObterUrl());
        }

        [Then(@"Ele será redirecionado para a vitrine")]
        public void ThenEleSeraRedirecionadoParaAVitrine()
        {
            Assert.Equal(_testsFixture.Configuration.VitrineUrl, _cadastroUsuarioTela.ObterUrl());
        }

        [Then(@"Uma saudação com seu e-mail será exibida no menu superior")]
        public void ThenUmaSaudacaoComSeuE_MailSeraExibidaNoMenuSuperior()
        {
            // Assert
            Assert.True(_cadastroUsuarioTela.ValidarSaudacaoUsuarioLogado(_testsFixture.Usuario));
        }
    }
}
