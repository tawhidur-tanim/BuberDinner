using BuberDinner.Application.Service.Authentication;
using BuberDinner.Contracts.Authentication;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.Api.Controllers;

[Route("Auth")]
[ApiController]
public class AuthenticationController : ApiController
{
    IAuthenticationService _authenticationService;

    public AuthenticationController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }


    [HttpPost("register")]
    public IActionResult Register(RegisterRequest request)
    {

        ErrorOr<AuthenticationResult> authenticationResult = _authenticationService.Register(request.FirstName, request.LastName,
        request.Email, request.Password);



        return authenticationResult.Match(authResult => Ok(MapAuthResult(authResult)), errors => Problem(errors));
    }

    private static AuthenticationResponse MapAuthResult(AuthenticationResult authenticationResult)
    {
        return new AuthenticationResponse(
        authenticationResult.Id,
        authenticationResult.FirstName,
        authenticationResult.LastName,
        authenticationResult.Email,
        authenticationResult.Token);
    }

    [HttpPost("login")]
    public IActionResult Login(LoginRequest request)
    {
        ErrorOr<AuthenticationResult> authenticationResult = _authenticationService.Login(
        request.Email, request.Password);

        return authenticationResult.Match(authResult => Ok(MapAuthResult(authResult)), errors => Problem(errors));


    }
}