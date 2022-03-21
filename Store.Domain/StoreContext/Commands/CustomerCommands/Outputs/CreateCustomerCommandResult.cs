using store.Store.Share.Commands;
using Store.Store.Share.Enums;

namespace Store.Domain.StoreContext.Commands.CustomerCommands.Inputs;

public class CreateCustomerCommandResult : ICommandResult
{
    public CreateCustomerCommandResult(Guid id, string name, string emailAdress, string phone)
    {
        Id = id;
        Name = name;
        EmailAdress = emailAdress;
        Phone = phone;
    }

    public Guid Id { get; set; }
    public string Name { get; set; }
    public string EmailAdress { get; set; }
    public string Phone { get; set; }

}
