namespace Pedidos.Domain.Entities;

public class Pedido
{
    public long Id {get; private set; }
    public string Nome {get; private set; }
    public string Item {get; private set; }
    public decimal Total {get; private set; }
    
    public Pedido(string nome, string item, decimal total)
    {
        Nome = nome;
        Item = item;
        Total = total;
    }

    public void AtualizarPedido(string nome, string item, decimal total)
    {
        Nome = nome;
        Item = item;
        Total = total;
    }
}