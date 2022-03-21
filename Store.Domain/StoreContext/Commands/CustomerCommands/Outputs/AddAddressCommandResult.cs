using store.Store.Share.Commands;
using Store.Store.Share.Enums;

namespace Store.Domain.StoreContext.Commands.CustomerCommands.Inputs;

public class AddAddressCommandResult : ICommandResult
{
    public AddAddressCommandResult()
    {

    }

    public AddAddressCommandResult(Guid id, string street, string number, string neighborhood, string city, string state, string country, string zipCode, string complement)
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

}
