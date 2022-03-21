using store.Store.Domain.StoreContext.Repositories;
using Store.Domain.StoreContext.Entities;
using Store.Store.Share.Enums;

namespace Store.Tests.Fakes;

public class FakeCustomerRepository : ICustomerRepository
{
    public bool ExistsDocument(string document, EDocumentType type)
    {
        return false;
    }

    public bool ExistsEmail(string email)
    {
        return false;
    }

    public bool Save(Customer customer)
    {
        return true;
    }
}