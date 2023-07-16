using BuberDinner.Application.Service.Authentication;
using BuberDinner.Contracts.Authentication;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using BuberDinner.Domain.Errors;
using BuberDinner.Application.Service.Common;
using BuberDinner.Application.Service.Authentication.Commands;
using BuberDinner.Application.Service.Authentication.Queries;

namespace BuberDinner.Api.Controllers;

[Route("Auth")]
[ApiController]
public class AuthenticationController : ApiController
{

    IAuthenticationCommandService _authenticationCommandService;
    IAuthenticationQueryService _authenticationQueryService;

    public AuthenticationController(IAuthenticationCommandService authenticationCommandService, IAuthenticationQueryService authenticationQueryService)
    {
        _authenticationCommandService = authenticationCommandService;
        _authenticationQueryService = authenticationQueryService;
    }


    [HttpPost("register")]
    public IActionResult Register(RegisterRequest request)
    {

        ErrorOr<AuthenticationResult> authenticationResult = _authenticationCommandService.Register(
            request.FirstName,
            request.LastName,
            request.Email,
            request.Password);



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
        ErrorOr<AuthenticationResult> authenticationResult = _authenticationQueryService.Login(
        request.Email, request.Password);


        if (authenticationResult.IsError
            && authenticationResult.FirstError == Errors.Authentication.InvalidCredentials)
        {
            return Problem(
                statusCode: StatusCodes.Status401Unauthorized,
                title: authenticationResult.FirstError.Description);
        }


        return authenticationResult.Match(authResult => Ok(MapAuthResult(authResult)), errors => Problem(errors));


    }
}