namespace Lab08_Andreboza.DTOs;

public class OrderDto
{
    public int OrderId { get; set; }
    public DateTime OrderDate { get; set; }
    public string ClientName { get; set; }
    public List<OrderDetailDto> Details { get; set; }
}