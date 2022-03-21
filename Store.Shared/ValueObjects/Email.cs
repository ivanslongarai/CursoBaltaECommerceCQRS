using Flunt.Validations;
using Store.SharedDomain.Enums;

namespace Store.Shared.ValueObjects;

public class Email : ValueObject
{
    public Email(string adress, EEmailType type = EEmailType.Individual)
    {
        Adress = adress;
        Type = type;
        Validate();
    }

    public string Adress { get; private set; }
    public EEmailType Type { get; private set; }

    public override void Validate()
    {
        AddNotifications(new Contract()
           .Requires()
           .IsEmail(Adress, "Email.Address", $"E-mail inválido")
       );
    }

}
