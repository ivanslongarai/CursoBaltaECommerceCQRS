using Microsoft.VisualStudio.TestTools.UnitTesting;
using Store.Store.Shared.ValueObjects;
using Store.Store.Share.Enums;
using Store.Domain.StoreContext.Entities;

namespace Store.Tests.Entities;

[TestClass]
public class OrderTests
{

    // Red, Green, Refactory
    // Just examples

    [TestMethod]
    public void WhenCreate_AnOrder_ShouldReturn_CreatedStatus()
    {
        var customer = GetCustomer();
        var order = new Order(customer);
        Assert.AreEqual(order.Status, EOrderStatus.Created);
    }

    [TestMethod]
    public void WhenPaid_AnOrder_ShouldReturn_PaidStatus()
    {
        var customer = GetCustomer();
        var order = new Order(customer);

        var product1 = new Product("Product 01", "Description 01", 10, 100);
        var item1 = new OrderItem(product1, 10, 100);
        order.AddItem(item1);

        order.Place();
        order.Pay(order.Total());
        Assert.AreEqual(order.Status, EOrderStatus.Paid);
    }

    [TestMethod]
    public void WhenPaid_AnOrderWithNotEnoughItems_ShouldNotReturn_PaidStatus()
    {
        var customer = GetCustomer();
        var order = new Order(customer);

        var product1 = new Product("Product 01", "Description 01", 15, 100);
        var item1 = new OrderItem(product1, 10, 100);
        order.AddItem(item1);
        order.AddItem(product1, 10, 200);

        order.Place();
        order.Pay(order.Total());
        Assert.AreNotEqual(order.Status, EOrderStatus.Paid);
    }

    [TestMethod]
    public void WhenCancel_AnOrder_ShouldReturn_CanceledStatus()
    {
        var customer = GetCustomer();
        var order = new Order(customer);
        order.Cancel();
        Assert.AreEqual(order.Status, EOrderStatus.Canceled);
    }

    [TestMethod]
    public void WhenCancel_AnOrder_ShouldReturn_FailCanceledStatus()
    {
        var customer = GetCustomer();
        var order = new Order(customer);

        var product1 = new Product("Product 01", "Description 01", 10, 100);
        var item1 = new OrderItem(product1, 10, 100);
        order.AddItem(item1);

        order.Place();
        order.Pay(order.Total());

        order.Cancel();
        Assert.AreNotEqual(order.Status, EOrderStatus.Canceled);
    }

    private Customer GetCustomer()
    {
        var name = new Name("Ivan", "Longarai");
        var document = new Document("12345678912");
        var email = new Email("eu@gmail.com");

        var address = new Address(
            "Andradas",
            "1000",
            "Centro",
            "Porto Alegre",
            "RS",
            "Brasil",
            "92015741",
            ""
        );

        var customer = new Customer(name, document, email, "48999999999");
        customer.AddAddress(address);
        return customer;
    }

}