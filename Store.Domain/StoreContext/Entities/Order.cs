using Flunt.Validations;
using Store.Shared.Entities;
using Store.SharedDomain.Enums;

namespace Store.StoreDomain.StoreContext.Entities;

public class Order : Entity
{

    private readonly IList<OrderItem> _items = new List<OrderItem>();
    private readonly IList<Delivery> _deliveries = new List<Delivery>();

    public Order(Customer customer)
    {
        Customer = customer;
        Number = Id.ToString().Replace("-", "").Substring(0, 8).ToUpper();
        CreatedAt = DateTime.Now;
        Status = EOrderStatus.Created;
        Validate();
        AddNotifications(customer.Notifications);
    }

    public Customer Customer { get; private set; }
    public string Number { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public EOrderStatus Status { get; private set; }
    public IReadOnlyCollection<OrderItem> Items { get => _items.ToArray(); }
    public IReadOnlyCollection<Delivery> Deliveries { get => _deliveries.ToArray(); }

    public void AddItem(Product product, decimal quantity, decimal price)
    {
        var item = new OrderItem(product, quantity, price);
        item.Validate();

        if (item.Invalid)
        {
            AddNotifications(item.Notifications);
            return;
        }
        _items.Add(item);
        item.Product.BookOut(item.Quantity);
    }

    public void AddItem(OrderItem orderItem)
    {
        orderItem.Validate();

        if (orderItem.Invalid)
        {
            AddNotifications(orderItem.Notifications);
            return;
        }
        _items.Add(orderItem);
        orderItem.Product.BookOut(orderItem.Quantity);
    }

    public bool Place()
    {
        Validate();
        if (Valid && _items.Count > 0)
        {
            Status = EOrderStatus.Validated;
            return true;
        }
        return false;
    }

    public bool Pay(decimal value)
    {
        // There will be no payments in this studying        
        if (Status == EOrderStatus.Validated)
        {
            Status = EOrderStatus.Paid;
            return true;
        }
        return false;
    }

    public bool Cancel()
    {
        if ((short)Status < (short)EOrderStatus.Paid)
        {
            Status = EOrderStatus.Canceled;
            return true;
        }
        return false;
    }

    public bool AddDelivery(Delivery delivery)
    {
        if (Status == EOrderStatus.Paid && delivery.Valid)
        {
            _deliveries.Add(delivery);
            return true;
        }
        return false;
    }

    public bool SetDelivered(Guid deliveryId)
    {
        if (Status == EOrderStatus.WaitingForDelievery)
        {

            var delivery = _deliveries.Where(x => x.Id == deliveryId).FirstOrDefault();
            if (delivery != null)
                delivery.SetDone();

            var delivering = _deliveries.Where(x => x.Status == EDeliveryStatus.Pending).FirstOrDefault();
            if (delivering == null)
                Status = EOrderStatus.Delievered;

            return true;
        }
        return false;
    }

    public bool Send()
    {
        if (Status == EOrderStatus.Paid && _deliveries.Count > 0)
        {
            Status = EOrderStatus.WaitingForDelievery;
            return true;
        }
        return false;
    }

    public bool SetAllDelivered()
    {
        if (Status == EOrderStatus.WaitingForDelievery)
        {
            var deliveries = _deliveries.Where(x => x.Status == EDeliveryStatus.Pending).ToList();
            foreach (var delivery in deliveries)
                delivery.SetDone();
            Status = EOrderStatus.Delievered;
            return true;
        }
        return false;
    }

    public bool SetConcluded()
    {
        if (Status == EOrderStatus.Delievered)
        {
            Status = EOrderStatus.Concluded;
            return true;
        }
        return false;
    }

    public decimal Total()
    {
        decimal total = 0;
        foreach (var item in _items)
            total += item.Quantity * item.Price;
        return total;
    }

    public override void Validate()
    {
        AddNotifications(new Contract()
            .Requires()
            .IsTrue(Customer is not null, "Order.Customer", "Cliente inválido")
        // .IsTrue(Items.Count > 0, "Order.Items", "O pedido não possui itens")
        );
    }


}
