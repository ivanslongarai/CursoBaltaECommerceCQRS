using Flunt.Notifications;

namespace Store.Store.Shared.Entities;

public abstract class Entity : Notifiable
{
    public Entity()
    {
        Id = Guid.NewGuid();
    }
    public Guid Id { get; private set; }

    public abstract void Validate();
}
