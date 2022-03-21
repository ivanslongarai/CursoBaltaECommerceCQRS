using Store.Store.Domain.Services;

namespace Store.Store.Infra.StoreContext.Services;

public class EmailService : IEmailService
{
    public bool Send(string to, string from, string subject, string body)
    {
        // TODO Implement
        return true;
    }
}