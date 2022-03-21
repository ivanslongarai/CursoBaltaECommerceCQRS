
using Flunt.Validations;
using Store.SharedDomain.Enums;

namespace Store.Shared.ValueObjects;

public class Document : ValueObject
{
    public Document(string number, EDocumentType type = EDocumentType.CPF)
    {
        Number = number;
        Type = type;
        Validate();
    }

    public string Number { get; private set; }
    public EDocumentType Type { get; private set; }

    public override void Validate()
    {
        AddNotifications(new Contract()
           .Requires()
           .IsTrue((Number.Length == 11 && Type == EDocumentType.CPF) || (Number.Length == 14 && Type == EDocumentType.CNPJ),
               "Document.Number", $"Documento inválido")
       );
    }

}
