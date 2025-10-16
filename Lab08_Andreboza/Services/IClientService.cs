using Lab08_Andreboza.DTOs;

namespace Lab08_Andreboza.Services;

public interface IClientService
{
    Task<IEnumerable<ClientDto>> GetClientsByNameAsync(string name);

    // Ejercicio 9
    Task<ClientWithOrderCountDto?> GetClientWithMostOrdersAsync();
    // Ejercicio 12
    Task<IEnumerable<ClientDto>> GetClientsWhoBoughtProductAsync(int productId);
    
    Task<IEnumerable<SalesByClientDto>> GetTotalSalesByClientAsync();
}