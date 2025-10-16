namespace Lab08_Andreboza.DTOs;

public class OrderWithDetailsDto
{
    public int OrderId { get; set; }
    public DateTime OrderDate { get; set; }
    public List<ProductDetailDto> Products { get; set; }
}