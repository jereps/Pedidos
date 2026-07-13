using Microsoft.EntityFrameworkCore;
using Pedidos.Domain.Entities;
using Pedidos.Domain.Repositories;
using Pedidos.Infrastructure.Context;

namespace Pedidos.Infrastructure.Repositories;

public class PedidoRepository : IPedidosRepository
{
    private readonly AppDbContext _context;

    public PedidoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AdicionarAsync(Pedido pedido, CancellationToken cancellationToken)
    {
        await _context.Pedidos.AddAsync(pedido,cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<Pedido>> ObterTodosPedidosAsync(CancellationToken cancellationToken)
    {
        return await _context.Pedidos.ToListAsync(cancellationToken);
    }

    public async Task<Pedido?> ObterPorIdAsync(long id, CancellationToken cancellationToken)
    {
        return await _context.Pedidos.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public void AtualizarPedidoAsync(Pedido pedido)
    {
        _context.Pedidos.Update(pedido);
        _context.SaveChanges();
    }

    public void DeletarPedidoAsync(Pedido pedido)
    {
        _context.Pedidos.Remove(pedido);
        _context.SaveChanges();
    }

}