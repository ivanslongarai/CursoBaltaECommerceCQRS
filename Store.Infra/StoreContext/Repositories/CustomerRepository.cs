using System.Runtime.InteropServices;
using System.Data;
using Dapper;
using store.Store.Domain.StoreContext.Repositories;
using Store.Domain.StoreContext.Entities;
using Store.Store.Infra.StoreContext.DataContexts;
using Store.Store.Share.Enums;
using Store.Store.Domain.Queries;

namespace Store.Store.Infra.StoreContext.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly StoreDataContext _context;

    public CustomerRepository(StoreDataContext context)
    {
        _context = context;
    }

    public CustomerOrdersCountResult CustomerOrdersCount(string document)
    {
        return _context
            .Connection
            .Query<CustomerOrdersCountResult>(
                "spGetCustomOrdersCount",
                new { Document = document },
                commandType: CommandType.StoredProcedure
            ).First();
    }

    public bool ExistsDocument(string document, EDocumentType type)
    {
        return _context
            .Connection
            .Query<bool>(
                "spCheckDocument",
                new { Document = document },
                commandType: CommandType.StoredProcedure
            ).FirstOrDefault();
    }

    public bool ExistsEmail(string email)
    {
        return _context
            .Connection
            .Query<bool>(
                "spCheckEmail",
                new { Email = email },
                commandType: CommandType.StoredProcedure
            ).FirstOrDefault();
    }

    public IEnumerable<ListCustomerQueryResult> GetAll()
    {
        return
            _context
            .Connection
            .Query<ListCustomerQueryResult>(
                @"SELECT 
                [Id], 
                CONCAT([FirstName], ' ', [LastName]) AS [Name],
                [Document]
                FROM [Customer]", new { });
    }

    public CustomerQueryResult? GetById(Guid id)
    {
        return
            _context
            .Connection
            .Query<CustomerQueryResult>(
                @"SELECT 
                [Id], 
                CONCAT([FirstName], ' ', [LastName]) AS [Name],
                [Document]
                FROM [Customer]
                WHERE [Id] = @id", new { Id = id }).FirstOrDefault();
    }

    public IEnumerable<ListCustomerOrdersQueryResult> GetOrders(Guid id)
    {
        return
            _context
            .Connection
            .Query<ListCustomerOrdersQueryResult>(
                @"SELECT C.[Id],
                        CONCAT(C.[FirstName], ' ', C.[LastName]) AS [Name],
                        C.[Document],
                        C.[Email],
                        O.[Id] AS [OrderId],
                        SUM(OI.Quantity * OI.Price) AS Total
                    FROM [Customer] C
                    JOIN [Order] O ON O.[CustomerId] = C.[Id]
                    JOIN [OrderItem] OI ON OI.[OrderId] = O.[Id]
                    WHERE C.[Id] = @id
                    GROUP BY C.[Id],
                            C.[FirstName],
                            C.[LastName],
                            C.[Document],
                            C.[Email],
                            O.[Id]", new { Id = id });
    }

    public bool Save(Customer customer)
    {

        try
        {
            _context.Connection.Execute("spCreateCustomer",
            new
            {
                Id = customer.Id,
                FirstName = customer.Name.FirstName,
                LastName = customer.Name.LastName,
                Document = customer.Document.Number,
                DocumentType = customer.Document.Type,
                Email = customer.Email.Adress,
                EmailType = customer.Email.Type,
                Phone = customer.Phone
            }, commandType: CommandType.StoredProcedure);

            foreach (var address in customer.Addresses)
            {
                _context.Connection.Execute("spCreateAdress",
                new
                {
                    Id = address.Id,
                    CustomerId = customer.Id,
                    Number = address.Number,
                    Complement = address.Complement,
                    District = address.District,
                    City = address.City,
                    State = address.State,
                    ZipCode = address.ZipCode,
                    Country = address.Country,
                    Type = address.Type,
                }, commandType: CommandType.StoredProcedure);
            }

            return true;
        }
        catch
        {
            return false;
        }
    }
}