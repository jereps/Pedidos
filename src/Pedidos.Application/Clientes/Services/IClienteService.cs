using Pedidos.Application.Clientes.Dtos;

namespace  Pedidos.Application.Clientes.Services;

public interface IClienteService
{
    Task<long> CriarCliente(CriarClienteDto dto, CancellationToken cancellationToken);
    Task<ClienteDetalheDto?> ObterClientePorId(long id, CancellationToken cancellationToken);
    Task<IEnumerable<ClienteDetalheDto>> ObterTodosClientes(CancellationToken cancellationToken);
    Task<ClienteDetalheDto?> AtualizarCliente(long id, AtualizarClienteDto atualizarClienteDto, CancellationToken cancellationToken);
    Task<bool> DeletarCliente(long id, CancellationToken cancellationToken);
}