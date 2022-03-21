using System;
using System.Collections.Generic;
using store.Store.Domain.StoreContext.Repositories;
using Store.Domain.StoreContext.Entities;
using Store.Store.Domain.Queries;
using Store.Store.Share.Enums;

namespace Store.Tests.Fakes;

public class FakeCustomerRepository : ICustomerRepository
{
    public CustomerOrdersCountResult CustomerOrdersCount(string document)
    {
        return new CustomerOrdersCountResult
        {
            Id = Guid.NewGuid(),
            Name = "Ivan Longarai",
            Orders = 10
        };
    }

    public bool ExistsDocument(string document, EDocumentType type)
    {
        return false;
    }

    public bool ExistsEmail(string email)
    {
        return false;
    }

    public IEnumerable<ListCustomerQueryResult> GetAll()
    {
        return null;
    }

    public CustomerQueryResult GetById(Guid id)
    {
        return null;
    }

    public IEnumerable<ListCustomerOrdersQueryResult> GetOrders(Guid id)
    {
        return null;
    }

    public bool Save(Customer customer)
    {
        return true;
    }
}