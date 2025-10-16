namespace Lab08_Andreboza.DTOs;

public class ClientOrderDto
{
    public string ClientName { get; set; }
    public List<OrderSummaryDto> Orders { get; set; } 
}