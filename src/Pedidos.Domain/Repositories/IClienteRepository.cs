using Pedidos.Domain.Entities;

namespace Pedidos.Domain.Repositories;

public interface IClienteRepository
{
    Task AdicionarCliente(Cliente cliente, CancellationToken cancellationToken);

    Task<Cliente?> ObterPorId(long id, CancellationToken cancellationToken);

    Task<IEnumerable<Cliente>> ObterTodosClientes(CancellationToken cancellationToken);

    void AtualizarCliente(Cliente cliente);
    void DeletarCliente(Cliente cliente);
}