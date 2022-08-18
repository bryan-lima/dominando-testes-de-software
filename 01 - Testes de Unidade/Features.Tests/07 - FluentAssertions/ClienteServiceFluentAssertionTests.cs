using Features.Clientes;
using FluentAssertions;
using FluentAssertions.Extensions;
using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Features.Tests
{
    [Collection(nameof(ClienteAutoMockerCollection))]
    public class ClienteServiceFluentAssetionTests
    {
        readonly ClienteTestsAutoMockerFixture _clienteTestsAutoMockerFixture;
        private readonly ClienteService _clienteService;

        public ClienteServiceFluentAssetionTests(ClienteTestsAutoMockerFixture clienteTestsFixture)
        {
            _clienteTestsAutoMockerFixture = clienteTestsFixture;
            _clienteService = _clienteTestsAutoMockerFixture.ObterClienteService();
        }

        [Fact(DisplayName = "Adicionar Cliente com Sucesso")]
        [Trait("Categoria", "Cliente Service Fluent Assertion Tests")]
        public void ClienteService_Adicionar_DeveExecutarComSucesso()
        {
            // Arrange
            var cliente = _clienteTestsAutoMockerFixture.GerarClienteValido();

            // Act
            _clienteService.Adicionar(cliente);

            // Assert
            //Assert.True(cliente.EhValido());

            // Assert
            cliente.EhValido().Should().BeTrue();

            _clienteTestsAutoMockerFixture.Mocker.GetMock<IClienteRepository>().Verify(expression: r => r.Adicionar(cliente), times: Times.Once);
            _clienteTestsAutoMockerFixture.Mocker.GetMock<IMediator>().Verify(expression: m => m.Publish(It.IsAny<INotification>(), CancellationToken.None), times: Times.Once);
        }

        [Fact(DisplayName = "Adicionar Cliente com Falha")]
        [Trait("Categoria", "Cliente Service Fluent Assertion Tests")]
        public void ClienteService_Adicionar_DeveFalharDevidoClienteInvalido()
        {
            // Arrange
            var cliente = _clienteTestsAutoMockerFixture.GerarClienteInvalido();

            // Act
            _clienteService.Adicionar(cliente);

            // Assert
            //Assert.False(cliente.EhValido());

            // Assert
            cliente.EhValido().Should().BeFalse(because: "possui incosistências");
            cliente.ValidationResult.Errors.Should().HaveCountGreaterThanOrEqualTo(expected: 1);

            _clienteTestsAutoMockerFixture.Mocker.GetMock<IClienteRepository>().Verify(expression: r => r.Adicionar(cliente), times: Times.Never);
            _clienteTestsAutoMockerFixture.Mocker.GetMock<IMediator>().Verify(expression: m => m.Publish(It.IsAny<INotification>(), CancellationToken.None), times: Times.Never);
        }

        [Fact(DisplayName = "Obter Clientes Ativos")]
        [Trait("Categoria", "Cliente Service Fluent Assertion Tests")]
        public void ClienteService_ObterTodosAtivos_DeveRetornarApenasClientesAtivos()
        {
            // Arrange

            _clienteTestsAutoMockerFixture.Mocker.GetMock<IClienteRepository>().Setup(c => c.ObterTodos())
                .Returns(_clienteTestsAutoMockerFixture.ObterClientesVariados());

            // Act
            var clientes = _clienteService.ObterTodosAtivos();

            // Assert
            //Assert.True(condition: clientes.Any());
            //Assert.False(condition: clientes.Count(c => !c.Ativo) > 0);

            // Assert
            clientes.Should().HaveCountGreaterThanOrEqualTo(expected: 1).And.OnlyHaveUniqueItems(); // Valida se a lista tem 1 ou mais resultados e que não tenha resultados iguais
            clientes.Should().NotContain(predicate: c => !c.Ativo);


            _clienteTestsAutoMockerFixture.Mocker.GetMock<IClienteRepository>().Verify(expression: r => r.ObterTodos(), times: Times.Once);

            // Esse tipo de validação talvez seja melhor ficar dentro de um teste de integração, está aqui apenas para exemplo/aprendizado
            _clienteService.ExecutionTimeOf(c => c.ObterTodosAtivos())
                .Should()
                .BeLessThanOrEqualTo(maxDuration: 50.Milliseconds(), because: "é executado milhares de vezes por segundo");
        }
    }
}