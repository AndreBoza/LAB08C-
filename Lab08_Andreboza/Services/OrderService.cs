using Lab08_Andreboza.Data;
using Lab08_Andreboza.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Lab08_Andreboza.Services;

public class OrderService : IOrderService
{
    private readonly LINQExample01DbContext _context;

    public OrderService(LINQExample01DbContext context)
    {
        _context = context;
    }

    // Ejercicio 3: Detalle de productos en una orden
    public async Task<IEnumerable<OrderDetailDto>> GetOrderDetailsByIdAsync(int orderId)
    {
        return await _context.Orderdetails
            .Where(od => od.OrderId == orderId)
            .Select(od => new OrderDetailDto
            {
                ProductName = od.Product.Name,
                Quantity = od.Quantity
            })
            .ToListAsync();
    }

    // Ejercicio 4: Cantidad total de productos por orden
    public async Task<int> GetTotalProductQuantityByOrderIdAsync(int orderId)
    {
        return await _context.Orderdetails
            .Where(od => od.OrderId == orderId)
            .SumAsync(od => od.Quantity);
    }

    // Ejercicio 6: Pedidos realizados después de una fecha
    public async Task<IEnumerable<OrderDto>> GetOrdersAfterDateAsync(DateTime date)
    {
        return await _context.Orders
            .Where(o => o.OrderDate > date)
            .Select(o => new OrderDto
            {
                OrderId = o.OrderId,
                OrderDate = o.OrderDate,
                ClientName = o.Client.Name,
                Details = o.Orderdetails.Select(od => new OrderDetailDto
                {
                    ProductName = od.Product.Name,
                    Quantity = od.Quantity
                }).ToList()
            })
            .ToListAsync();
    }
    
    // Ejercicio 10: Obtener todos los pedidos y sus detalles (AÑADIDO)
    public async Task<IEnumerable<OrderDto>> GetAllOrdersWithDetailsAsync()
    {
        return await _context.Orders
            .Include(o => o.Client) // Incluimos el cliente para obtener el nombre
            .Include(o => o.Orderdetails) // Incluimos los detalles...
                .ThenInclude(od => od.Product) // ...y el producto de cada detalle
            .Select(o => new OrderDto
            {
                OrderId = o.OrderId,
                OrderDate = o.OrderDate,
                ClientName = o.Client.Name,
                Details = o.Orderdetails.Select(od => new OrderDetailDto
                {
                    ProductName = od.Product.Name,
                    Quantity = od.Quantity
                }).ToList()
            })
            .ToListAsync();
    }
}