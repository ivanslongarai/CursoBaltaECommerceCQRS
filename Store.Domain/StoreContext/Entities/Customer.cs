using Flunt.Validations;
using Store.Shared.Entities;
using Store.Shared.ValueObjects;

namespace Store.StoreDomain.StoreContext.Entities;
// Single-responsibility principle
// Open–closed principle
// L
// I
// D

public class Customer : Entity
{
    private readonly IList<Address> _addresses = new List<Address>();
    public Customer(Name name, Document document, Email email, string? phone = null)
    {
        Name = name;
        Document = document;
        Email = email;
        Phone = phone;
    }

    public Name Name { get; private set; }
    public Document Document { get; private set; }
    public Email Email { get; private set; }
    public string? Phone { get; private set; }
    public IReadOnlyCollection<Address> Addresses { get => _addresses.ToArray(); }

    public override void Validate()
    {
        AddNotifications(new Contract()
            .Requires()
            .IsTrue(Name is not null, "Customer.Name", "Nome inválido")
            .IsTrue(Document is not null, "Customer.Document", "Documento inválido")
            .IsTrue(Email is not null, "Customer.Email", "Email inválido")
        );
    }

    public void AddAddress(Address address)
    {
        if (address.Invalid)
        {
            AddNotification("Customer.Address", "Endereço inválido");
            return;
        }
        _addresses.Add(address);
    }
}