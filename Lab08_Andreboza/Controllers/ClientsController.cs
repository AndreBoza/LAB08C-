using Lab08_Andreboza.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization; // <-- Añade este using

namespace Lab08_Andreboza.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")] // <-- AÑADE ESTA LÍNEA
public class ClientsController : ControllerBase
{
    private readonly IClientService _clientService;
    // 1. Declarar el campo para el servicio de productos
    private readonly IProductService _productService; 

    // 2. Añadir IProductService al constructor para que sea inyectado
    public ClientsController(IClientService clientService, IProductService productService)
    {
        _clientService = clientService;
        _productService = productService; // 3. Asignar el servicio inyectado al campo
    }

    // GET /api/clients?name=Juan
    [HttpGet]
    public async Task<IActionResult> GetClientsByName([FromQuery] string name)
    {
        var clients = await _clientService.GetClientsByNameAsync(name);
        return Ok(clients);
    }

    // Ejercicio 9: GET /api/clients/with-most-orders
    [HttpGet("with-most-orders")]
    public async Task<IActionResult> GetClientWithMostOrders()
    {
        var client = await _clientService.GetClientWithMostOrdersAsync();
        if (client == null)
        {
            return NotFound("No se encontró un cliente con órdenes.");
        }
        return Ok(client);
    }
    
    // Ejercicio 11: Se accede a través de GET /api/clients/{clientId}/products
    [HttpGet("{clientId}/products")]
    public async Task<IActionResult> GetProductsSoldToClient(int clientId)
    {
        var products = await _productService.GetProductsSoldToClientAsync(clientId);
        return Ok(products);
    }
    
    
    // GET /api/clients/total-sales
    [HttpGet("total-sales")]
    public async Task<IActionResult> GetTotalSalesByClient()
    {
        var sales = await _clientService.GetTotalSalesByClientAsync();
        return Ok(sales);
    }
}