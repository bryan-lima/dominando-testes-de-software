using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Tests
{
    public class AssertStringsTests
    {
        [Fact]
        public void StringsTools_UnirNomes_RetornarNomeCompleto()
        {
            // Arrange
            var sut = new StringsTools();

            // Act
            var nomeCompleto = sut.Unir("Bryan", "Lima");

            // Assert
            Assert.Equal(expected: "Bryan Lima", actual: nomeCompleto);
        }



        [Fact]
        public void StringsTools_UnirNomes_DeveIgnorarCase()
        {
            // Arrange
            var sut = new StringsTools();

            // Act
            var nomeCompleto = sut.Unir("Bryan", "Lima");

            // Assert
            Assert.Equal(expected: "BRYAN LIMA", actual: nomeCompleto, ignoreCase: true);
        }



        [Fact]
        public void StringsTools_UnirNomes_DeveConterTrecho()
        {
            // Arrange
            var sut = new StringsTools();

            // Act
            var nomeCompleto = sut.Unir("Bryan", "Lima");

            // Assert
            Assert.Contains(expectedSubstring: "yan", actualString: nomeCompleto);
        }


        [Fact]
        public void StringsTools_UnirNomes_DeveComecarCom()
        {
            // Arrange
            var sut = new StringsTools();

            // Act
            var nomeCompleto = sut.Unir("Bryan", "Lima");

            // Assert
            Assert.StartsWith(expectedStartString: "Bry", actualString: nomeCompleto);
        }


        [Fact]
        public void StringsTools_UnirNomes_DeveAcabarCom()
        {
            // Arrange
            var sut = new StringsTools();

            // Act
            var nomeCompleto = sut.Unir("Bryan", "Lima");

            // Assert
            Assert.EndsWith(expectedEndString: "ima", actualString: nomeCompleto);
        }


        [Fact]
        public void StringsTools_UnirNomes_ValidarExpressaoRegular()
        {
            // Arrange
            var sut = new StringsTools();

            // Act
            var nomeCompleto = sut.Unir("Bryan", "Lima");

            // Assert
            Assert.Matches(expectedRegexPattern: "[A-Z]{1}[a-z]+ [A-Z]{1}[a-z]+", actualString: nomeCompleto);
        }
    }
}
