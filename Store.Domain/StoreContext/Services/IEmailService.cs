namespace Store.Store.Domain.Services;

public interface IEmailService
{
    bool Send(string to, string from, string subject, string body);
}