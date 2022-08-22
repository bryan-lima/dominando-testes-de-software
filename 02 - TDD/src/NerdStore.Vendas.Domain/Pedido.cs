using FluentValidation.Results;
using NerdStore.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NerdStore.Vendas.Domain
{
    public class Pedido : Entity, IAggregateRoot
    {
        public static int MAX_UNIDADES_ITEM => 15;
        public static int MIN_UNIDADES_ITEM => 1;

        public int Codigo { get; private set; }
        public Guid ClienteId { get; private set; }
        public Guid? VoucherId { get; private set; }
        public decimal ValorTotal { get; private set; }
        public decimal Desconto { get; private set; }
        public PedidoStatus PedidoStatus { get; private set; }
        public DateTime DataCadastro { get; private set; }
        public bool VoucherUtilizado { get; private set; }
        public Voucher Voucher { get; private set; }

        private readonly List<PedidoItem> _pedidoItems;
        public IReadOnlyCollection<PedidoItem> PedidoItems => _pedidoItems;

        protected Pedido()
        {
            _pedidoItems = new List<PedidoItem>();
        }

        public ValidationResult AplicarVoucher(Voucher voucher)
        {
            var validationResult = voucher.ValidarSeAplicavel();
            if (!validationResult.IsValid) return validationResult;

            Voucher = voucher;
            VoucherUtilizado = true;
            CalcularValorPedido();

            return validationResult;
        }

        public void CalcularValorTotalDesconto()
        {
            if (!VoucherUtilizado) return;

            decimal desconto = 0;
            var valor = ValorTotal;

            if (Voucher.TipoDescontoVoucher == TipoDescontoVoucher.Valor)
            {
                if (Voucher.ValorDesconto.HasValue)
                {
                    desconto = Voucher.ValorDesconto.Value;
                    valor -= desconto;
                }
            }
            else
            {
                if (Voucher.PercentualDesconto.HasValue)
                {
                    desconto = (ValorTotal * Voucher.PercentualDesconto.Value) / 100;
                    valor -= desconto;
                }
            }

            ValorTotal = valor < 0 ? 0 : valor;
            Desconto = desconto;
        }

        private void CalcularValorPedido()
        {
            ValorTotal = PedidoItems.Sum(i => i.CalcularValor());
            CalcularValorTotalDesconto();
        }

        public bool PedidoItemExistente(PedidoItem item)
        {
            return _pedidoItems.Any(p => p.ProdutoId == item.ProdutoId);
        }

        private void ValidarPedidoItemInexistente(PedidoItem item)
        {
            if (!PedidoItemExistente(item)) throw new DomainException($"O item não pertence ao pedido");
        }

        private void ValidarQuantidadeItemPermitida(PedidoItem item)
        {
            var quantidadeItens = item.Quantidade;

            if (PedidoItemExistente(item))
            {
                var itemExistente = _pedidoItems.FirstOrDefault(p => p.ProdutoId == item.ProdutoId);
                quantidadeItens += itemExistente.Quantidade;
            }

            if (quantidadeItens > MAX_UNIDADES_ITEM) throw new DomainException($"Máximo de {MAX_UNIDADES_ITEM} unidades por produto");
        }

        public void AdicionarItem(PedidoItem pedidoItem)
        {
            ValidarQuantidadeItemPermitida(pedidoItem);

            pedidoItem.AssociarPedido(Id);

            if (PedidoItemExistente(pedidoItem))
            {
                var itemExistente = _pedidoItems.FirstOrDefault(p => p.ProdutoId == pedidoItem.ProdutoId);
                itemExistente.AdicionarUnidades(pedidoItem.Quantidade);
                pedidoItem = itemExistente;

                _pedidoItems.Remove(itemExistente);
            }

            _pedidoItems.Add(pedidoItem);
            CalcularValorPedido();
        }

        public void AtualizarItem(PedidoItem pedidoItem)
        {
            ValidarPedidoItemInexistente(pedidoItem);
            ValidarQuantidadeItemPermitida(pedidoItem);
            pedidoItem.AssociarPedido(Id);

            var itemExistente = PedidoItems.FirstOrDefault(p => p.ProdutoId == pedidoItem.ProdutoId);

            _pedidoItems.Remove(itemExistente);
            _pedidoItems.Add(pedidoItem);

            CalcularValorPedido();
        }

        public void RemoverItem(PedidoItem pedidoItem)
        {
            ValidarPedidoItemInexistente(pedidoItem);

            _pedidoItems.Remove(pedidoItem);
            CalcularValorPedido();
        }

        public void AtualizarUnidades(PedidoItem pedidoItem, int unidades)
        {
            pedidoItem.AtualizarUnidades(unidades);
            AtualizarItem(pedidoItem);
        }

        public void TornarRascunho()
        {
            PedidoStatus = PedidoStatus.Rascunho;
        }

        public static class PedidoFactory
        {
            public static Pedido NovoPedidoRascunho(Guid clienteId)
            {
                var pedido = new Pedido
                {
                    ClienteId = clienteId
                };

                pedido.TornarRascunho();
                return pedido;
            }
        }
    }
}
