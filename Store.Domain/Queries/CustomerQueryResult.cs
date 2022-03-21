using System.Reflection.Metadata;
namespace Store.Store.Domain.Queries;

public class CustomerQueryResult
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Document { get; set; }
    public string Email { get; set; }
}