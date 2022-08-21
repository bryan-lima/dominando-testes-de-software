using MediatR;
using NerdStore.Vendas.Application.Events;
using NerdStore.Vendas.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NerdStore.Vendas.Application.Commands
{
    public class PedidoCommandHandler
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IMediator _mediator;

        public PedidoCommandHandler(IPedidoRepository pedidoRepository, IMediator mediator)
        {
            _pedidoRepository = pedidoRepository;
            _mediator = mediator;
        }

        public bool Handle(AdicionarItemPedidoCommand message)
        {
            _pedidoRepository.Adicionar(Pedido.PedidoFactory.NovoPedidoRascunho(message.ClienteId));
            _mediator.Publish(new PedidoItemAdicionadoEvent());

            return true;
        }
    }
}
