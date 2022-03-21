using Microsoft.VisualStudio.TestTools.UnitTesting;
using Store.Shared.ValueObjects;

namespace Store.Tests.ValueObjects;

[TestClass]
public class NameTests
{

    // Red, Green, Refactory
    // Just examples

    [TestMethod]
    public void Given_ValidName_ShoudReturn_ValidName()
    {
        var name = new Name("Ivan", "Longarai");
        Assert.IsTrue(name.Valid);
    }

    [TestMethod]
    public void Given_InvalidName_ShoudReturn_InvalidName()
    {
        var name = new Name("Iv", "Longarai");
        Assert.IsTrue(!name.Valid);
    }

    [TestMethod]
    public void Given_ValidName_ShoudReturn_ValueFullName()
    {
        var name = new Name("Ivan", "Longarai");
        Assert.AreEqual(name.ToString(), "Ivan Longarai");
    }

}