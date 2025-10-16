using Lab08_Andreboza.Data;
using Lab08_Andreboza.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Lab08_Andreboza.Services;

public class ClientService : IClientService
{
    private readonly LINQExample01DbContext _context;

    public ClientService(LINQExample01DbContext context)
    {
        _context = context;
    }

    // Obtener clientes por nombre
    public async Task<IEnumerable<ClientDto>> GetClientsByNameAsync(string name)
    {
        return await _context.Clients
            .Where(c => c.Name.Contains(name))
            .Select(c => new ClientDto
            {
                ClientId = c.ClientId,
                Name = c.Name,
                Email = c.Email
            })
            .ToListAsync();
    }

    // Ejercicio 9: Obtener el cliente con más pedidos
    public async Task<ClientWithOrderCountDto?> GetClientWithMostOrdersAsync()
    {
        return await _context.Orders
            .GroupBy(o => o.Client)
            .Select(g => new ClientWithOrderCountDto
            {
                ClientId = g.Key.ClientId,
                Name = g.Key.Name,
                OrderCount = g.Count()
            })
            .OrderByDescending(x => x.OrderCount)
            .FirstOrDefaultAsync();
    }
    // Ejercicio 12: Implementación de la consulta LINQ
    public async Task<IEnumerable<ClientDto>> GetClientsWhoBoughtProductAsync(int productId)
    {
        return await _context.Orderdetails
            .Where(od => od.ProductId == productId) 
            .Select(od => od.Order.Client) 
            .Distinct() 
            .Select(c => new ClientDto 
            {
                ClientId = c.ClientId,
                Name = c.Name,
                Email = c.Email
            })
            .ToListAsync();
    }
    public async Task<List<ClientOrderDto>> GetClientsWithOrdersAsNoTrackingAsync()
    {
        var clientOrders = await _context.Clients
            .AsNoTracking()
            .Select(client => new ClientOrderDto // <-- Usamos el DTO principal
            {
                ClientName = client.Name,
                Orders = client.Orders
                    .Select(order => new OrderSummaryDto // <-- Usamos el DTO de resumen
                    {
                        OrderId = order.OrderId,
                        OrderDate = order.OrderDate
                    }).ToList()
            }).ToListAsync();

        return clientOrders;
    }
    public async Task<IEnumerable<SalesByClientDto>> GetTotalSalesByClientAsync()
    {
        return await _context.Orders
            .AsNoTracking()
            .Include(o => o.Client) 
            .Include(o => o.Orderdetails) 
            .ThenInclude(od => od.Product) 
            .GroupBy(o => o.Client) 
            .Select(group => new SalesByClientDto
            {
                // La "Key" del grupo es el Cliente, así que podemos acceder a su nombre
                ClientName = group.Key.Name,
                // Sumamos el total de los detalles de todas las órdenes en el grupo
                TotalSales = group.Sum(o => 
                    o.Orderdetails.Sum(od => od.Quantity * od.Product.Price))
            })
            .OrderByDescending(s => s.TotalSales)
            .ToListAsync();
    }
}