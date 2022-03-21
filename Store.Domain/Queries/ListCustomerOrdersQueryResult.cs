using System.Reflection.Metadata;
namespace Store.Store.Domain.Queries;

public class ListCustomerOrdersQueryResult
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Document { get; set; }
    public string Email { get; set; }
    public Guid OrderId { get; set; }
    public decimal Total { get; set; }
}