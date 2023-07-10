using BuberDinner.Application.Service.Authentication;
using BuberDinner.Contracts.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.Api.Controllers;

[Route("Auth")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    IAuthenticationService _authenticationService;

    public AuthenticationController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }


    [HttpPost("register")]
    public IActionResult Register(RegisterRequest request)
    {

        AuthenticationResult authenticationResult = _authenticationService.Register(request.FirstName, request.LastName,
        request.Email, request.Password);

        AuthenticationResponse response = new AuthenticationResponse(
        authenticationResult.Id,
        authenticationResult.FirstName,
        authenticationResult.LastName,
        authenticationResult.Email,
        authenticationResult.Token);


        return Ok(response);
    }

    [HttpPost("login")]
    public IActionResult Login(LoginRequest request)
    {


        AuthenticationResult authenticationResult = _authenticationService.Login(
        request.Email, request.Password);

        AuthenticationResponse response = new AuthenticationResponse(
        authenticationResult.Id,
        authenticationResult.FirstName,
        authenticationResult.LastName,
        authenticationResult.Email,
        authenticationResult.Token);


        return Ok(response);
    }
}