using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace NerdStore.Vendas.Domain.Tests
{
    public class VoucherTests
    {
        [Fact(DisplayName = "Validar Voucher Tipo Valor Válido")]
        [Trait("Categoria", "Vendas - Voucher")]
        public void Voucher_ValidarVoucherTipoValor_DeveEstarValido()
        {
            // Arrange
            var voucher = new Voucher
            {
                Codigo = "PROMO-15-REAIS",
                ValorDesconto = 15,
                PercentualDesconto = null,
                Quantidade = 1,
                DataValidade = DateTime.Now,
                Ativo = true,
                Utilizado = false
            };

            // Act
            var result = voucher.ValidarSeAplicavel();

            // Assert
            Assert.True(result);
        }
    }
}
