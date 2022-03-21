using Flunt.Validations;
using Store.Store.Shared.Entities;
using Store.Store.Share.Enums;

namespace Store.Domain.StoreContext.Entities;

public class Delivery : Entity
{
    public Delivery(DateTime estimatedDeliveryDate)
    {
        CreatedAt = DateTime.Now;
        EstimatedDeliveryDate = estimatedDeliveryDate;
        DeliveredDate = null;
        Status = EDeliveryStatus.Pending;
    }

    public DateTime CreatedAt { get; }
    public DateTime EstimatedDeliveryDate { get; private set; }
    public DateTime? DeliveredDate { get; private set; }
    public EDeliveryStatus Status { get; private set; }

    public bool SetDone()
    {
        if (Status == EDeliveryStatus.Pending)
        {
            DeliveredDate = DateTime.Now;
            Status = EDeliveryStatus.Done;
            return true;
        }
        return false;
    }

    public override void Validate()
    {
        AddNotifications(new Contract()
            .Requires()
            .IsTrue(EstimatedDeliveryDate > DateTime.Now, "Delivery.EstimatedDeliveryDate",
                "A data de entrega tem que ser maior que a data atual")
        );
    }
}
