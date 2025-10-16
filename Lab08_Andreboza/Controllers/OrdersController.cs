using Lab08_Andreboza.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization; // <-- Añade este using

namespace Lab08_Andreboza.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")] // <-- AÑADE ESTA LÍNEA
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    // GET /api/orders/1/details
    [HttpGet("{orderId}/details")]
    public async Task<IActionResult> GetOrderDetails(int orderId)
    {
        var details = await _orderService.GetOrderDetailsByIdAsync(orderId);
        if (!details.Any())
        {
            return NotFound($"No se encontraron detalles para la orden con ID {orderId}.");
        }
        return Ok(details);
    }

    // GET /api/orders/1/total-quantity
    [HttpGet("{orderId}/total-quantity")]
    public async Task<IActionResult> GetTotalQuantityInOrder(int orderId)
    {
        var total = await _orderService.GetTotalProductQuantityByOrderIdAsync(orderId);
        return Ok(new { OrderId = orderId, TotalQuantity = total });
    }

    // Ejercicio 6: GET /api/orders?afterDate=2025-05-01
    [HttpGet]
    public async Task<IActionResult> GetOrdersAfterDate([FromQuery] DateTime afterDate)
    {
        var orders = await _orderService.GetOrdersAfterDateAsync(afterDate);
        return Ok(orders);
    }
    // Ejercicio 10: Se accede a través de GET /api/orders/with-details
    [HttpGet("with-details")]
    public async Task<IActionResult> GetAllOrdersWithDetails()
    {
        var orders = await _orderService.GetAllOrdersWithDetailsAsync();
        return Ok(orders);
    }
    
    // GET /api/orders/{orderId}/details-with-products
    [HttpGet("{orderId}/details-with-products")]
    public async Task<IActionResult> GetOrderWithDetails(int orderId)
    {
        var orderDetails = await _orderService.GetOrderWithDetailsAsync(orderId);

        if (orderDetails == null)
        {
            return NotFound($"No se encontró la orden con ID {orderId}.");
        }

        return Ok(orderDetails);
    }
}