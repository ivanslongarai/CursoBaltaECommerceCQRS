using Flunt.Notifications;

namespace Store.Shared.ValueObjects;

public abstract class ValueObject : Notifiable
{
    public abstract void Validate();
}
