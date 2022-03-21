using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Mvc;
using store.Store.Domain.StoreContext.Handlers;
using store.Store.Domain.StoreContext.Repositories;
using Store.Domain.StoreContext.Commands.CustomerCommands.Inputs;
using Store.Domain.StoreContext.Entities;
using Store.Store.Domain.Queries;
using Store.Store.Domain.Services;

namespace Store.Store.Api.Controllers;

[ApiController]
public class CustomerController : Controller
{
    private readonly ICustomerRepository _ctx;
    private readonly CustomerHandler _handler;

    public CustomerController(ICustomerRepository ctx, CustomerHandler handler)
    {
        _ctx = ctx;
        _handler = handler;
    }

    [HttpGet("v1/customers")]
    //[ResponseCache(Location = ResponseCacheLocation.Client, Duration = 10)]
    //[ResponseCache(Duration = 10)]
    //Just an example //Cache-Control: public, max-age=10
    public ActionResult<IEnumerable<ListCustomerQueryResult>> Get()
    {
        return Ok(_ctx.GetAll());
    }

    [HttpGet("v1/customers/{id}")]
    public ActionResult<CustomerQueryResult> GetById([FromRoute] Guid id)
    {
        return Ok(_ctx.GetById(id));
    }

    [HttpGet("v1/customers/{id}/orders")]
    public ActionResult<List<ListCustomerOrdersQueryResult>> GetOrders([FromRoute] Guid id)
    {
        return Ok(_ctx.GetOrders(id));
    }

    [HttpPost("v1/customers")]
    public ActionResult<CreateCustomerCommandResult> Post([FromBody] CreateCustomerCommand command)
    {
        var result = (CreateCustomerCommandResult)_handler.Handle(command);
        if (_handler.Invalid)
            return BadRequest(_handler.Notifications);
        return Ok((CreateCustomerCommandResult)result);
    }

    [HttpPut("v1/customers/{id}")]
    public ActionResult<Customer> Put([FromBody] Customer customer, [FromRoute] Guid id)
    {
        return Ok("TODO Implement");
    }

    [HttpDelete("v1/customers/{id}")]
    public ActionResult<bool> Delete()
    {
        return Ok("TODO Implement");
    }
}