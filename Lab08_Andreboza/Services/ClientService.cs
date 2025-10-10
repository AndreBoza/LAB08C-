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
}