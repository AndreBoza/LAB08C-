using Lab08_Andreboza.DTOs;

namespace Lab08_Andreboza.Services;

public interface IOrderService
{
    // Ejercicio 3
    Task<IEnumerable<OrderDetailDto>> GetOrderDetailsByIdAsync(int orderId);

    // Ejercicio 4
    Task<int> GetTotalProductQuantityByOrderIdAsync(int orderId);
    
    // Ejercicio 6
    Task<IEnumerable<OrderDto>> GetOrdersAfterDateAsync(DateTime date);
    
    // Ejercicio 10 (AÑADIDO)
    Task<IEnumerable<OrderDto>> GetAllOrdersWithDetailsAsync();
}