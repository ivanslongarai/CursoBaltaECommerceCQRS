using Flunt.Notifications;
using Flunt.Validations;
using store.Store.Share.Commands;
using Store.Store.Share.Enums;

namespace Store.Domain.StoreContext.Commands.CustomerCommands.Inputs;

public class CreateCustomerCommand : Notifiable, ICommand
{
    public CreateCustomerCommand(string firstName, string lastName, string document, EDocumentType documentType, string emailAdress, string phone)
    {
        FirstName = firstName;
        LastName = lastName;
        Document = document;
        DocumentType = documentType;
        EmailAdress = emailAdress;
        Phone = phone;
    }

    // Fail Fast Validations

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Document { get; set; }
    public EDocumentType DocumentType { get; set; }
    public string EmailAdress { get; set; }
    public string Phone { get; set; }

    public bool Validate()
    {
        int nameMinLen = 03;
        int nameMaxLen = 80;
        AddNotifications(new Contract()
           .Requires()
           .IsTrue(FirstName?.Length >= nameMinLen, "FirstName", $"Nome precisa de no mínimo {nameMinLen} caracteres")
           .IsTrue(FirstName?.Length <= nameMaxLen, "FirstName", $"Nome precisa ter no máximo {nameMaxLen} caracteres")
           .IsTrue(LastName?.Length >= nameMinLen, "LastName", $"Sobrenome precisa de no mínimo {nameMinLen} caracteres")
           .IsTrue(LastName?.Length <= nameMaxLen, "LastName", $"Sobrenome precisa ter no máximo {nameMaxLen} caracteres")
           .IsEmail(EmailAdress, "Email.Address", $"E-mail inválido")
           .IsTrue((Document?.Length == 11 && DocumentType == EDocumentType.CPF) || (Document?.Length == 14 &&
                DocumentType == EDocumentType.CNPJ), "Document.Number", $"Documento inválido")
        );
        return Valid;
    }
}
