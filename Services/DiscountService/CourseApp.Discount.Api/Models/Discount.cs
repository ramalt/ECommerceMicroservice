namespace CourseApp.Discount.Api.Models;

[Dapper.Contrib.Extensions.Table("Discounts")]
public class Discount
{
    public int Id { get; set; }    
    public string Code { get; set; } = null!;
    public string UserId { get; set; } = null!;
    public int Rate { get; set; }
    public DateTime CreatedDate { get; set; }
}
