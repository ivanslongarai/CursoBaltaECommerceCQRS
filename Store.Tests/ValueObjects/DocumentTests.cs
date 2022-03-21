using Microsoft.VisualStudio.TestTools.UnitTesting;
using Store.Store.Shared.ValueObjects;
using Store.Store.Share.Enums;

namespace Store.Tests.ValueObjects;

[TestClass]
public class DocumentTests
{
    // Red, Green, Refactory
    // Just examples

    [TestMethod]
    public void Given_ValidCPFDocument_ShouldReturn_ValidDocument()
    {
        var document = new Document("11111111111", EDocumentType.CPF);
        Assert.IsTrue(document.Valid);
    }

    [TestMethod]
    public void Given_InvalidCPFDocument_ShouldReturn_InvalidDocument()
    {
        var document = new Document("991111111111111", EDocumentType.CPF);
        Assert.IsTrue(!document.Valid);
    }

    public void Given_ValidCNPJDocument_ShouldReturn_ValidDocument()
    {
        var document = new Document("11111111111111", EDocumentType.CNPJ);
        Assert.IsTrue(document.Valid);
    }

    [TestMethod]
    public void Given_InvalidCNPJDocument_ShouldReturn_InvalidDocument()
    {
        var document = new Document("12345", EDocumentType.CNPJ);
        Assert.IsTrue(!document.Valid);
    }

}