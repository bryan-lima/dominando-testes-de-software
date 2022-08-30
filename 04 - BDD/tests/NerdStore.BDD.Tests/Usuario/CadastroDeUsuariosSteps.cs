using System;
using TechTalk.SpecFlow;

namespace NerdStore.BDD.Tests.Usuario
{
    [Binding]
    public class CadastroDeUsuariosSteps
    {
        [When(@"Ele clicar em registrar")]
        public void WhenEleClicarEmRegistrar()
        {
            throw new PendingStepException();
        }

        [When(@"Preencher os dados do formulário")]
        public void WhenPreencherOsDadosDoFormulario(Table table)
        {
            throw new PendingStepException();
        }

        [When(@"Clicar no botão registrar")]
        public void WhenClicarNoBotaoRegistrar()
        {
            throw new PendingStepException();
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