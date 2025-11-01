using EcommerceSimples.Data;
using EcommerceSimples.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Banco SQLite
builder.Services.AddDbContext<AppDb>(o =>
    o.UseSqlite("Data Source=ecommerce.db"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

// --------------------- ENDPOINTS ---------------------

// Criar produto
app.MapPost("/products", async (Product p, AppDb db) =>
{
    db.Products.Add(p);
    await db.SaveChangesAsync();
    return Results.Ok(p);
});

// Listar produtos
app.MapGet("/products", async (AppDb db) =>
{
    return Results.Ok(await db.Products.ToListAsync());
});

// Obter produto
app.MapGet("/products/{id}", async (Guid id, AppDb db) =>
{
    var p = await db.Products.FindAsync(id);
    return p is null ? Results.NotFound() : Results.Ok(p);
});

// Criar pedido (verifica estoque)
app.MapPost("/orders", async (Order o, AppDb db) =>
{
    var product = await db.Products.FindAsync(o.ProductId);

    if (product is null)
        return Results.BadRequest("Produto não encontrado.");

    if (product.Quantity < o.Quantity)
        return Results.BadRequest("Estoque insuficiente.");

    product.Quantity -= o.Quantity;

    db.Orders.Add(o);
    await db.SaveChangesAsync();

    return Results.Ok(o);
});

// Listar pedidos
app.MapGet("/orders", async (AppDb db) =>
{
    return Results.Ok(await db.Orders.ToListAsync());
});

// ---------------------

app.Run();
