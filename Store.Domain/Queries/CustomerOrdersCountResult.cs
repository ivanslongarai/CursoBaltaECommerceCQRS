namespace Store.Store.Domain.Queries;

public class CustomerOrdersCountResult
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Orders { get; set; }
}