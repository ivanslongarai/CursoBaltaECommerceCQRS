using Flunt.Validations;

namespace Store.Store.Shared.ValueObjects;

public class Address : ValueObject
{
    public Address(string street, string number, string district, string city, string state, string country, string zipCode, string complement)
    {

        Id = Guid.NewGuid();
        Street = street;
        Number = number;
        District = district;
        City = city;
        State = state;
        Country = country;
        ZipCode = zipCode;
        Complement = complement;
        Type = 1; // Just for example in Case we had types of address
        Validate();
    }

    public Guid Id { get; private set; }
    public string Street { get; private set; }
    public string Number { get; private set; }
    public string District { get; private set; }
    public string City { get; private set; }
    public string State { get; private set; }
    public string Country { get; private set; }
    public string ZipCode { get; private set; }
    public string Complement { get; private set; }
    public int Type { get; private set; }

    public override void Validate()
    {
        int streetMinLen = 03;
        int streetMaxLen = 80;
        int zipCodeLen = 08;

        AddNotifications(new Contract()
            .Requires()
            .HasMinLen(Street, streetMinLen, "Address.Street", $"Rua deve conter no mínimo {streetMinLen} caracteres")
            .HasMaxLen(Street, streetMaxLen, "Address.Street", $"Rua deve conter no máximo {streetMaxLen} caracteres")
            .HasMaxLen(ZipCode, zipCodeLen, "Address.ZipCode", $"Cep inválido")
        );
    }

}
