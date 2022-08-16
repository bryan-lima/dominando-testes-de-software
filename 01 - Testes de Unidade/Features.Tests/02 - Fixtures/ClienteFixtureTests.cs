﻿using Features.Clientes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Features.Tests
{
    public class ClienteFixtureTests
    {
        public Cliente ClienteValido;

        public ClienteFixtureTests()
        {
            ClienteValido = new Cliente(
                Guid.NewGuid(),
                "Bryan",
                "Lima",
                DateTime.Now.AddYears(-25),
                "bryan@lima.com",
                true,
                DateTime.Now);
        }

        [Fact(DisplayName = "Novo Cliente Válido")]
        [Trait("Categoria", "Cliente Fixture Testes")]
        public void Cliente_NovoCliente_DeveEstarValido()
        {
            var result = ClienteValido.EhValido();

            // Assert
            Assert.True(condition: result);
            Assert.Equal(expected: 0, actual: ClienteValido.ValidationResult.Errors.Count);
        }

        [Fact(DisplayName = "Novo Cliente Inválido")]
        [Trait("Categoria", "Cliente Fixture Testes")]
        public void Cliente_NovoCliente_DeveEstarInValido()
        {
            // Arrange
            var cliente = new Cliente(
                Guid.NewGuid(),
                "",
                "",
                DateTime.Now,
                "bryan2lima.com",
                true,
                DateTime.Now);

            // Act
            var result = cliente.EhValido();

            // Assert
            Assert.False(condition: result);
            Assert.NotEqual(expected: 0, actual: cliente.ValidationResult.Errors.Count);
        }
    }
}
