using Flunt.Notifications;

namespace Store.Store.Shared.ValueObjects;

public abstract class ValueObject : Notifiable
{
    public abstract void Validate();
}
