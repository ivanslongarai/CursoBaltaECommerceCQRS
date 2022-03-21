using Microsoft.AspNetCore.Mvc;

namespace Store.Store.Api.Controllers;

[ApiController]
public class HomeController : Controller
{
    [HttpGet("")]
    public IActionResult Get()
    {
        return Ok(new
        {
            api_version = 1.1
        });
    }
}