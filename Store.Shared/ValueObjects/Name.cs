using Flunt.Validations;

namespace Store.Store.Shared.ValueObjects;

public class Name : ValueObject
{
    public Name(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
        Validate();
    }

    public string FirstName { get; private set; }
    public string LastName { get; private set; }

    public override string ToString()
    {
        return $"{FirstName} {LastName}";
    }

    public override void Validate()
    {
        int nameMinLen = 03;
        int nameMaxLen = 80;
        AddNotifications(new Contract()
           .Requires()
           .IsTrue(FirstName.Length >= nameMinLen, "Name.FirstName", $"Nome precisa de no mínimo {nameMinLen} caracteres")
           .IsTrue(FirstName.Length <= nameMaxLen, "Name.FirstName", $"Nome precisa ter no máximo {nameMaxLen} caracteres")
           .IsTrue(LastName.Length >= nameMinLen, "Name.LastName", $"Sobrenome precisa de no mínimo {nameMinLen} caracteres")
           .IsTrue(LastName.Length <= nameMaxLen, "Name.LastName", $"Sobrenome precisa ter no máximo {nameMaxLen} caracteres")
       );
    }
}
