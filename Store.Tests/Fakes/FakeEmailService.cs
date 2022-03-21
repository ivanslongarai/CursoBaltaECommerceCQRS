using store.Store.Domain.StoreContext.Repositories;
using Store.Domain.StoreContext.Entities;
using Store.Store.Domain.Services;
using Store.Store.Share.Enums;

namespace Store.Tests.Fakes;

public class FakeEmailService : IEmailService
{
    public bool Send(string to, string from, string subject, string body)
    {
        return true;
    }
}