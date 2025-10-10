using Lab08_Andreboza.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lab08_Andreboza.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;
    // 1. Declarar el campo para el servicio de clientes
    private readonly IClientService _clientService; 

    // 2. Añadir IClientService al constructor para que sea inyectado
    public ProductsController(IProductService productService, IClientService clientService)
    {
        _productService = productService;
        _clientService = clientService; // 3. Asignar el servicio inyectado al campo
    }

    // GET /api/products?priceGreaterThan=20
    [HttpGet]
    public async Task<IActionResult> GetProductsByPrice([FromQuery] decimal priceGreaterThan)
    {
        var products = await _productService.GetProductsPriceGreaterThanAsync(priceGreaterThan);
        return Ok(products);
    }

    // GET /api/products/most-expensive
    [HttpGet("most-expensive")]
    public async Task<IActionResult> GetMostExpensive()
    {
        var product = await _productService.GetMostExpensiveProductAsync();
        if (product == null)
        {
            return NotFound("No se encontraron productos.");
        }
        return Ok(product);
    }

    // GET /api/products/average-price
    [HttpGet("average-price")]
    public async Task<IActionResult> GetAveragePrice()
    {
        var average = await _productService.GetAverageProductPriceAsync();
        return Ok(new { AveragePrice = average });
    }



    // GET /api/products/without-description
    [HttpGet("without-description")]
    public async Task<IActionResult> GetWithoutDescription()
    {
        var products = await _productService.GetProductsWithoutDescriptionAsync();
        return Ok(products);
    }

    // Ejercicio 12: GET /api/products/{productId}/clients
    [HttpGet("{productId}/clients")]
    public async Task<IActionResult> GetClientsWhoBoughtProduct(int productId)
    {
        var clients = await _clientService.GetClientsWhoBoughtProductAsync(productId);
        return Ok(clients);
    }
}