namespace Store.SharedDomain.Enums;

public enum EOrderStatus
{
    Created = 1,
    Validated = 2,
    WaitingForPayment = 3,
    Paid = 4,
    WaitingForDelievery = 5,
    Delievered = 6,
    Concluded = 7,
    Canceled = 8
}
