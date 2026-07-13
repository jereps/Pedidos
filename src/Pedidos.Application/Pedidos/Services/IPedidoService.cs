using Pedidos.Application.Pedidos.Dtos;

namespace Pedidos.Application.Pedidos.Services;

public interface IPedidoService
{
    Task<long> CriarPedidoAsync(CriarPedidoDto dto, CancellationToken cancellationToken);

    Task<PedidoDetalheDto?> ObterPedidoPorIdAsync(long id, CancellationToken cancellationToken);

    Task<IEnumerable<PedidoDetalheDto>> ObterTodosOsPedidosAsync(CancellationToken cancellationToken);
    Task<PedidoDetalheDto?> AtualizarPedidoAsync(long id, AtualizarPedidoDto dto, CancellationToken cancellationToken);
    Task<bool> DeletarPedidoAsync(long id, CancellationToken cancellationToken);
}