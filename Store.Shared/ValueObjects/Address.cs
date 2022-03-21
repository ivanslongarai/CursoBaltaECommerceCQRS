using Flunt.Validations;

namespace Store.Shared.ValueObjects;

public class Address : ValueObject
{
    public Address(string street, string number, string neighborhood, string city, string state, string country, string zipCode, string complement)
    {
        Street = street;
        Number = number;
        Neighborhood = neighborhood;
        City = city;
        State = state;
        Country = country;
        ZipCode = zipCode;
        Complement = complement;
        Validate();
    }

    public string Street { get; private set; }
    public string Number { get; private set; }
    public string Neighborhood { get; private set; }
    public string City { get; private set; }
    public string State { get; private set; }
    public string Country { get; private set; }
    public string ZipCode { get; private set; }
    public string Complement { get; private set; }

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
