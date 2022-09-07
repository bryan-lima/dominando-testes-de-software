﻿using NerdStore.BDD.Tests.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NerdStore.BDD.Tests.Usuario
{
    public class LoginUsuarioTela : BaseUsuarioTela
    {
        public LoginUsuarioTela(SeleniumHelper helper) : base(helper)
        {

        }

        public void ClicarNoLinkLogin()
        {
            Helper.ClicarLinkPorTexto("Login");
        }

        public void PreencherFormularioLogin(Usuario usuario)
        {
            Helper.PreencherTextBoxPorId("Input_Email", usuario.Email);
            Helper.PreencherTextBoxPorId("Input_Password", usuario.Senha);
        }

        public bool ValidarPreenchimentoFormularioLogin(Usuario usuario)
        {
            if (Helper.ObterValorTextBoxPorId("Input_Email") != usuario.Email) return false;
            if (Helper.ObterValorTextBoxPorId("Input_Password") != usuario.Senha) return false;

            return true;
        }

        public void ClicarNoBotaoLogin()
        {
            Helper.ClicarBotaoPorId("login-submit");
        }

        public bool Login(Usuario usuario)
        {
            AcessarSiteLoja();
            ClicarNoLinkLogin();
            PreencherFormularioLogin(usuario);
            if (!ValidarPreenchimentoFormularioLogin(usuario)) return false;
            ClicarNoBotaoLogin();
            if (!ValidarSaudacaoUsuarioLogado(usuario)) return false;

            return true;
        }
    }
}
