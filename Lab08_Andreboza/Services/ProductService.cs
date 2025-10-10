using Lab08_Andreboza.Data;
using Lab08_Andreboza.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Lab08_Andreboza.Services;

public class ProductService : IProductService
{
    private readonly LINQExample01DbContext _context;

    public ProductService(LINQExample01DbContext context)
    {
        _context = context;
    }

    // Productos con precio mayor a un valor
    public async Task<IEnumerable<ProductDto>> GetProductsPriceGreaterThanAsync(decimal price)
    {
        return await _context.Products
            .Where(p => p.Price > price)
            .Select(p => new ProductDto
            {
                ProductId = p.ProductId,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price
            })
            .ToListAsync();
    }

    // Ejercicio 5: Producto más caro
    public async Task<ProductDto?> GetMostExpensiveProductAsync()
    {
        return await _context.Products
            .OrderByDescending(p => p.Price)
            .Select(p => new ProductDto
            {
                ProductId = p.ProductId,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price
            })
            .FirstOrDefaultAsync();
    }

    // Ejercicio 7: Promedio de precio de los productos
    public async Task<decimal> GetAverageProductPriceAsync()
    {
        return await _context.Products.AverageAsync(p => p.Price);
    }

    // Ejercicio 8: Productos que no tienen descripción
    public async Task<IEnumerable<ProductDto>> GetProductsWithoutDescriptionAsync()
    {
        return await _context.Products
            .Where(p => string.IsNullOrEmpty(p.Description))
            .Select(p => new ProductDto
            {
                ProductId = p.ProductId,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price
            })
            .ToListAsync();
    }
    // Ejercicio 11: Implementación de la consulta LINQ
    public async Task<IEnumerable<ProductDto>> GetProductsSoldToClientAsync(int clientId)
    {
        return await _context.Orderdetails
            .Where(od => od.Order.ClientId == clientId) 
            .Select(od => od.Product) 
            .Distinct() 
            .Select(p => new ProductDto 
            {
                ProductId = p.ProductId,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price
            })
            .ToListAsync();
    }
}