using Flunt.Notifications;
using store.Store.Domain.StoreContext.Repositories;
using store.Store.Share.Commands;
using Store.Domain.StoreContext.Commands.CustomerCommands.Inputs;
using Store.Domain.StoreContext.Entities;
using Store.Store.Shared.ValueObjects;
using Store.Store.Domain.Services;

namespace store.Store.Domain.StoreContext.Handlers;

public class CustomerHandler :
    Notifiable,
    ICommandHandler<CreateCustomerCommand>,
    ICommandHandler<AddAddressCommand>
{

    private readonly ICustomerRepository _customerRepository;
    private readonly IEmailService _emailService;

    public CustomerHandler(
        ICustomerRepository customerRepository,
        IEmailService emailService)
    {
        _emailService = emailService;
        _customerRepository = customerRepository;
    }

    public ICommandResult Handle(CreateCustomerCommand command)
    {

        // Create VOs
        var name = new Name(command.FirstName, command.LastName);
        var document = new Document(command.Document, command.DocumentType);
        var email = new Email(command.EmailAdress);

        // Create Customer
        var customer = new Customer(name, document, email, command.Phone);

        // Validade VOs and Customer
        AddNotifications(name, document, email, customer);

        // Email exists
        if (email.Valid)
            if (_customerRepository.ExistsEmail(command.EmailAdress))
                AddNotification("Email", "Email em uso");

        // Document exists
        if (document.Valid)
            if (_customerRepository.ExistsDocument(command.Document, command.DocumentType))
                AddNotification("Document", "Documento em uso");

        // Vefify everything is Valid
        if (Invalid)
            return null;

        // Persist Customer
        _customerRepository.Save(customer);

        // Send Welcome Email
        _emailService.Send(command.EmailAdress, "eu@gmail.com", "Seja bem vindo", "Bem vindo aos cursos!");

        // Return Result
        return new CreateCustomerCommandResult(
            customer.Id,
            customer.Name.ToString(),
            customer.Email.Adress,
            customer.Phone != null ? customer.Phone : ""
        );
    }

    public ICommandResult Handle(AddAddressCommand command)
    {
        var result = new AddAddressCommandResult();
        return result;
    }
}