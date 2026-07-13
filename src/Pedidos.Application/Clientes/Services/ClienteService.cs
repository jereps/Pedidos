using Pedidos.Application.Clientes.Dtos;
using Pedidos.Domain.Entities;
using Pedidos.Domain.Repositories;

namespace Pedidos.Application.Clientes.Services;

public class ClienteService : IClienteService
{

    private readonly IClienteRepository _repository;

    public ClienteService(IClienteRepository repository)
    {
        _repository = repository;
    }
    public async Task<ClienteDetalheDto?> AtualizarCliente(long id, AtualizarClienteDto atualizar, CancellationToken cancellationToken)
    {
        var cliente = await _repository.ObterPorId(id,cancellationToken);
        if (cliente is null) return null;

        cliente.AtualizarCliente(atualizar.Nome,atualizar.Sobrenome);
        _repository.AtualizarCliente(cliente);

        return new ClienteDetalheDto(cliente.Id,cliente.Nome,cliente.Sobrenome);
    }

    public async Task<long> CriarCliente(CriarClienteDto dto, CancellationToken cancellationToken)
    {
        var cliente = new Cliente(dto.Nome,dto.Sobrenome);
        await _repository.AdicionarCliente(cliente,cancellationToken);
        return cliente.Id;
    }

    public async Task<bool> DeletarCliente(long id, CancellationToken cancellationToken)
    {
        var cliente = await _repository.ObterPorId(id,cancellationToken);
        if (cliente is null) return false;

        _repository.DeletarCliente(cliente);
        return true;
    }

    public async Task<ClienteDetalheDto?> ObterClientePorId(long id, CancellationToken cancellationToken)
    {
        var cliente = await _repository.ObterPorId(id,cancellationToken);
        if (cliente is null) return null;

        return new ClienteDetalheDto(cliente.Id,cliente.Nome,cliente.Sobrenome);
    }

    public async Task<IEnumerable<ClienteDetalheDto>> ObterTodosClientes(CancellationToken cancellationToken)
    {
        var cliente = await _repository.ObterTodosClientes(cancellationToken);
        return cliente.Select(p => new ClienteDetalheDto(p.Id,p.Nome,p.Sobrenome));
    }
}