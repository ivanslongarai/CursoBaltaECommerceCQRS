
using Flunt.Notifications;
using Flunt.Validations;
using store.Store.Share.Commands;

namespace Store.Domain.StoreContext.Commands.OrderCommands.Inputs;

public class PlaceOrderCommand : Notifiable, ICommand
{
    public PlaceOrderCommand()
    {
        OrderItems = new List<OrderItemCommand>();
    }

    public Guid Customer { get; set; }
    public IList<OrderItemCommand> OrderItems { get; set; }

    public bool Validate()
    {
        AddNotifications(new Contract()
            .Requires()
            .IsTrue(!string.IsNullOrEmpty(Customer.ToString()), "Customer", $"Cliente inválido")
            .IsTrue(OrderItems.Count > 0, "OrderItems", $"Sem itens no pedido"));
        return Valid;
    }
}

public class OrderItemCommand
{
    public Guid Product { get; set; }
    public decimal Quantity { get; set; }
}
