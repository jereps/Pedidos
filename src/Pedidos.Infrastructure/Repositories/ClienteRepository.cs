using Microsoft.EntityFrameworkCore;
using Pedidos.Domain.Entities;
using Pedidos.Domain.Repositories;
using Pedidos.Infrastructure.Context;

namespace Pedidos.Infrastructure.Repositories;

public class ClienteRepository : IClienteRepository
{
    private readonly AppDbContext _context;

    public ClienteRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AdicionarCliente(Cliente cliente, CancellationToken cancellationToken)
    {
        await _context.Clientes.AddAsync(cliente, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<Cliente?> ObterPorId(long id, CancellationToken cancellationToken)
    {
        return await _context.Clientes.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public void AtualizarCliente(Cliente cliente)
    {
        _context.Clientes.Update(cliente);
        _context.SaveChanges();
    }

    public void DeletarCliente(Cliente cliente)
    {
        _context.Clientes.Remove(cliente);
        _context.SaveChanges();
    }

    public async Task<IEnumerable<Cliente>> ObterTodosClientes(CancellationToken cancellationToken)
    {
        return await _context.Clientes.ToListAsync(cancellationToken);
    }
}


