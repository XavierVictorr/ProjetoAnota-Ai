using System.Net;
using Domain.DTOS;
using Domain.Interfaces.Services.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace application.Controllers;

[Route("Api/[controller]")]
[ApiController]

public class LoginController : ControllerBase
{
    
    [AllowAnonymous]
    [HttpPost]
    public async Task<object> Login([FromBody] LoginDto loginDto, [FromServices] IloginService service)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (loginDto == null)
        {
            return BadRequest();
        }

        try
        {
            var result = await service.FindByLogin(loginDto);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }
        catch (ArgumentException e)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, e.Message); 
        }
    }
}