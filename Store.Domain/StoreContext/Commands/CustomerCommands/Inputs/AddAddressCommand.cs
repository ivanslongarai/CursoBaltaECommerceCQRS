
using Flunt.Notifications;
using Flunt.Validations;
using store.Store.Share.Commands;

namespace Store.Domain.StoreContext.Commands.CustomerCommands.Inputs;

public class AddAddressCommand : Notifiable, ICommand
{
    public AddAddressCommand(Guid id, string street, string number, string neighborhood, string city, string state, string country, string zipCode, string complement)
    {
        Id = id;
        Street = street;
        Number = number;
        Neighborhood = neighborhood;
        City = city;
        State = state;
        Country = country;
        ZipCode = zipCode;
        Complement = complement;
    }

    public Guid Id { get; set; }
    public string Street { get; set; }
    public string Number { get; set; }
    public string Neighborhood { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Country { get; set; }
    public string ZipCode { get; set; }
    public string Complement { get; set; }

    public bool Validate()
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

        return Valid;
    }
}
