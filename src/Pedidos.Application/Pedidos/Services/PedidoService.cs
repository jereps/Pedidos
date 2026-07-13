using Pedidos.Application.Pedidos.Dtos;
using Pedidos.Domain.Entities;
using Pedidos.Domain.Repositories;

namespace Pedidos.Application.Pedidos.Services;

public class PedidoService : IPedidoService
{
    private readonly IPedidosRepository _repository;

    public PedidoService(IPedidosRepository repository)
    {
        _repository = repository;
    }

    public async Task<long> CriarPedidoAsync(CriarPedidoDto dto, CancellationToken cancellationToken)
    {
        var produto = new Pedido(dto.Nome,dto.Item,dto.Total);
        await _repository.AdicionarAsync(produto,cancellationToken);
        return produto.Id;
    }


        public async Task<IEnumerable<PedidoDetalheDto>> ObterTodosOsPedidosAsync(CancellationToken cancellationToken)
    {
                var pedidos = await _repository.ObterTodosPedidosAsync(cancellationToken);
                
                return pedidos.Select(p => new PedidoDetalheDto(p.Id, p.Nome,p.Item, p.Total));
    }

    public async Task<PedidoDetalheDto?> ObterPedidoPorIdAsync(long id, CancellationToken cancellationToken)
    {
                var pedido = await _repository.ObterPorIdAsync(id, cancellationToken); 

                if (pedido is null)
                    return null;
                
                return new PedidoDetalheDto(pedido.Id,pedido.Nome,pedido.Item,pedido.Total);
    }


    public async Task<PedidoDetalheDto?> AtualizarPedidoAsync(long id, AtualizarPedidoDto dto, CancellationToken cancellationToken)
    {
                var pedido = await _repository.ObterPorIdAsync(id, cancellationToken);
                if (pedido is null) return null;

                pedido.AtualizarPedido(dto.Nome,dto.Item,dto.Total);
                _repository.AtualizarPedidoAsync(pedido);

                return new PedidoDetalheDto(pedido.Id,pedido.Nome,pedido.Item,pedido.Total);
    }

    public async Task<bool> DeletarPedidoAsync(long id, CancellationToken cancellationToken)
    {
                var pedido = await _repository.ObterPorIdAsync(id, cancellationToken);
                if (pedido is null) return false;

                _repository.DeletarPedidoAsync(pedido);
                return true;

    }

}