namespace Pedidos.Domain.Entities;

public class Cliente
{
    public long Id {get; private set; }

        public string Nome {get; private set; }

        public string Sobrenome {get; private set;}

        public Cliente(string nome, string sobrenome)
    {
        Nome = nome;
        Sobrenome = sobrenome;
    }

    public void AtualizarCliente(string nome, string sobrenome)
    {
        Nome = nome;
        Sobrenome = sobrenome;
    }

}