using Microsoft.EntityFrameworkCore;

using Pedidos.Application.Pedidos.Dtos;
using Pedidos.Application.Pedidos.Services;
using Pedidos.Domain.Repositories;
using Pedidos.Infrastructure.Context;
using Pedidos.Infrastructure.Repositories;


var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("PostgresConnection");



builder.Services.AddDbContext<AppDbContext>(opt => 
    opt.UseNpgsql(connectionString)
        .UseSnakeCaseNamingConvention());

builder.Services.AddScoped<IPedidoService, PedidoService>();

builder.Services.AddScoped<IPedidosRepository, PedidoRepository>();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();



if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    // ATIVA O SWAGGER CLÁSSICO
    app.UseSwaggerUI(options =>
    {
        // Aponta para o JSON gerado nativamente pelo .NET 10
        options.SwaggerEndpoint("/openapi/v1.json", "Pedidos API v1");
        
        // Define o Swagger na raiz da rota (opcional, ex: localhost:PORTA/swagger)
        options.RoutePrefix = "swagger"; 
    });
}

app.UseHttpsRedirection();

app.MapPost("/api/pedidos", async (CriarPedidoDto dto, IPedidoService pedidoService, CancellationToken cancellationToken) =>
{
    long pedidoId = await pedidoService.CriarPedidoAsync(dto,cancellationToken);

    return Results.Created($"/api/pedido/{pedidoId}",new {Id = pedidoId} );
});

app.MapGet("/api/pedidos/{id:long}",async (long id, IPedidoService pedidoService, CancellationToken cancellationToken) =>
{
    var pedido = await pedidoService.ObterPedidoPorIdAsync(id,cancellationToken);

    if (pedido is null)
        return Results.NotFound(new { Mensagem = $"Pedido com ID {id} não foi encontrado." });

    return Results.Ok(pedido);
});

app.MapGet("/api/produtos", async (IPedidoService pedidoService, CancellationToken cancellationToken) =>
{
    var pedidos = await pedidoService.ObterTodosOsPedidosAsync(cancellationToken);
    return Results.Ok(pedidos);
});

app.MapPut("/api/pedidos/{id:long}", async (long id, AtualizarPedidoDto dto, IPedidoService pedidoService, CancellationToken cancellationToken) =>
{
    var pedidoAtualizado = await pedidoService.AtualizarPedidoAsync(id,dto,cancellationToken);

    if (pedidoAtualizado is null)
        return Results.NotFound(new { Mensagem = $"Pedido com ID {id} não encontrado para atualizar." });

    return Results.Ok(pedidoAtualizado);
});

app.MapDelete("/api/pedidos/{id:long}", async (long id, IPedidoService pedidoService, CancellationToken cancellationToken) =>
{
    var pedidoDeletado = await pedidoService.DeletarPedidoAsync(id,cancellationToken);

    if (!pedidoDeletado)
        return Results.NotFound(new { Mensagem = $"Pedido com ID {id} não encontrado para exclusão." });

    return Results.NoContent();
});

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<AppDbContext>();
        // Executa as Migrations pendentes e cria o banco se ele não existir
        await context.Database.MigrateAsync();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Ocorreu um erro ao aplicar as Migrations no PostgreSQL.");
    }
}

app.Run();
