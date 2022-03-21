using Store.Domain.StoreContext.Entities;
using Store.Store.Domain.Queries;
using Store.Store.Share.Enums;

namespace store.Store.Domain.StoreContext.Repositories;

public interface ICustomerRepository
{
    bool ExistsDocument(string document, EDocumentType type);
    bool ExistsEmail(string email);
    bool Save(Customer customer);
    CustomerOrdersCountResult CustomerOrdersCount(String document);
    IEnumerable<ListCustomerQueryResult> GetAll();
    CustomerQueryResult GetById(Guid id);
    IEnumerable<ListCustomerOrdersQueryResult> GetOrders(Guid id);
}