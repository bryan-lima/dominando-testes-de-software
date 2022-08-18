using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Features.Tests
{
    [Collection(nameof(ClienteAutoMockerCollection))]
    public class ClienteFluentAssertionTests
    {
        private readonly ClienteTestsAutoMockerFixture _clienteTestsFixture;

        public ClienteFluentAssertionTests(ClienteTestsAutoMockerFixture clienteTestsFixture)
        {
            _clienteTestsFixture = clienteTestsFixture;
        }


        [Fact(DisplayName = "Novo Cliente Válido")]
        [Trait("Categoria", "Cliente Fluent Assertion Testes")]
        public void Cliente_NovoCliente_DeveEstarValido()
        {
            // Arrange
            var cliente = _clienteTestsFixture.GerarClienteValido();

            // Act
            var result = cliente.EhValido();

            // Assert
            //Assert.True(result);
            //Assert.Equal(0, cliente.ValidationResult.Errors.Count);

            // Assert
            result.Should().BeTrue();
            cliente.ValidationResult.Errors.Should().HaveCount(expected: 0);
        }

        [Fact(DisplayName = "Novo Cliente Inválido")]
        [Trait("Categoria", "Cliente Fluent Assertion Testes")]
        public void Cliente_NovoCliente_DeveEstarInvalido()
        {
            // Arrange
            var cliente = _clienteTestsFixture.GerarClienteInvalido();

            // Act
            var result = cliente.EhValido();

            // Assert 
            //Assert.False(result);
            //Assert.NotEqual(0, cliente.ValidationResult.Errors.Count);

            // Assert
            result.Should().BeFalse();
            cliente.ValidationResult.Errors.Should().HaveCountGreaterThanOrEqualTo(expected: 1, because: "deve possuir erros de validação");
        }
    }
}