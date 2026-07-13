using Pedidos.Domain.Entities;

namespace Pedidos.Domain.Repositories;

public interface IPedidosRepository
{
    Task AdicionarAsync(Pedido pedido, CancellationToken cancellationToken);

    Task<Pedido?> ObterPorIdAsync(long id, CancellationToken cancellationToken);
    Task<IEnumerable<Pedido>> ObterTodosPedidosAsync(CancellationToken cancellationToken);
    void AtualizarPedidoAsync(Pedido pedido);
    void DeletarPedidoAsync(Pedido pedido);
}