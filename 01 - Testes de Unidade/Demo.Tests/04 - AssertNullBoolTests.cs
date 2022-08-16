using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Tests
{
    public class AssertNullBoolTests
    {
        [Fact]
        public void Funcionario_Nome_NaoDeveSerNuloOuVazio()
        {
            // Arrange & Act
            var funcionario = new Funcionario("", 1000);

            // Assert
            Assert.False(condition: string.IsNullOrEmpty(funcionario.Nome));
        }

        [Fact]
        public void Funcionario_Apelido_NaoDeveTerApelido()
        {
            // Arrange & Act
            var funcionario = new Funcionario("Bryan", 1000);

            // Assert
            Assert.Null(@object: funcionario.Apelido);

            // Assert Bool
            Assert.True(condition: string.IsNullOrEmpty(funcionario.Apelido));
            Assert.False(condition: funcionario.Apelido?.Length > 0);
        }
    }
}
