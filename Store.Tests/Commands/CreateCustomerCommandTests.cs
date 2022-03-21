using Microsoft.VisualStudio.TestTools.UnitTesting;
using Store.Store.Share.Enums;
using Store.Domain.StoreContext.Commands.CustomerCommands.Inputs;

namespace Store.Tests.ValueObjects;

[TestClass]
public class CreateCustomerCommandTests
{
    // Red, Green, Refactory
    // Just examples

    [TestMethod]
    public void Given_ValidCommand_ShouldReturn_Valid()
    {
        var command = new CreateCustomerCommand(
            "Ivan",
            "Longarai",
            "12345678912",
            EDocumentType.CPF,
            "eu@gmail.com",
            ""
        );
        Assert.IsTrue(command.Valid);
    }

}