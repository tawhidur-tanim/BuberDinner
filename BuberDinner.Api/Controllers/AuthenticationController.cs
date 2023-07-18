using BuberDinner.Contracts.Authentication;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using BuberDinner.Domain.Errors;
using BuberDinner.Application.Authentication.Commands.Register;
using BuberDinner.Application.Authentication.Common;
using BuberDinner.Application.Authentication.Queries.Login;
using MapsterMapper;
using MediatR;

namespace BuberDinner.Api.Controllers;

[Route("Auth")]
[ApiController]
public class AuthenticationController : ApiController
{

    private readonly ISender _mediatr;
    private readonly IMapper _mapper;

    public AuthenticationController(ISender mediatr, IMapper mapper)
    {
        _mediatr = mediatr;
        _mapper = mapper;
    }


    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {

        RegisterCommand registerCommand = _mapper.Map<RegisterCommand>(request);

        ErrorOr<AuthenticationResult> authenticationResult = await _mediatr.Send(registerCommand);

        return authenticationResult
        .Match(
            authResult => Ok(_mapper.Map<AuthenticationResponse>(authResult))
            , errors => Problem(errors));
    }



    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {

        LoginQuery loginQuery = _mapper.Map<LoginQuery>(request); ;

        ErrorOr<AuthenticationResult> authenticationResult = await _mediatr.Send(loginQuery);

        if (authenticationResult.IsError
            && authenticationResult.FirstError == Errors.Authentication.InvalidCredentials)
        {
            return Problem(
                statusCode: StatusCodes.Status401Unauthorized,
                title: authenticationResult.FirstError.Description);
        }


        return authenticationResult.Match(authResult => Ok(_mapper.Map<AuthenticationResponse>(authResult))
        , errors => Problem(errors));


    }
}