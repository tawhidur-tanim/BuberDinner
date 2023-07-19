using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.Api.Controllers;


[Route("[Controller]")]
public class DinnersController : ApiController
{

    [HttpGet]
    public IActionResult ListOfDinners()
    {
        return Ok(Array.Empty<string>());
    }
}