using System;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using store.Store.Domain.StoreContext.Handlers;
using Store.Domain.StoreContext.Commands.CustomerCommands.Inputs;
using Store.Store.Share.Enums;
using Store.Tests.Fakes;

namespace Store.Tests.Handlers;

[TestClass]
public class CustomerHandlerTests
{
    [TestMethod]
    public void GivenAValidCommandShouldRegisterACustomer()
    {
        var command = new CreateCustomerCommand(
            "Ivan",
            "Longarai",
            "12345678912",
            EDocumentType.CPF,
            "eu@gmail.com",
            ""
        );

        var handler = new CustomerHandler(new FakeCustomerRepository(), new FakeEmailService());
        var result = handler.Handle(command);

        Assert.AreNotEqual(result, null);
        Assert.AreEqual(0, handler.Notifications.Count);
    }
}