using Lab08_Andreboza.DTOs;

namespace Lab08_Andreboza.Services;

public interface IProductService
{
    Task<IEnumerable<ProductDto>> GetProductsPriceGreaterThanAsync(decimal price);
    
    // Ejercicio 5
    Task<ProductDto?> GetMostExpensiveProductAsync();
    
    // Ejercicio 7
    Task<decimal> GetAverageProductPriceAsync();
    
    // Ejercicio 8
    Task<IEnumerable<ProductDto>> GetProductsWithoutDescriptionAsync();
    // Ejercicio 11
    Task<IEnumerable<ProductDto>> GetProductsSoldToClientAsync(int clientId);
    
}