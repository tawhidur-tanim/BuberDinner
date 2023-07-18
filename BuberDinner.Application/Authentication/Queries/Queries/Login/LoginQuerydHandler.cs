
using BuberDinner.Application.Authentication.Common;
using BuberDinner.Application.Common.Interfaces;
using BuberDinner.Application.Common.Persistence;
using BuberDinner.Domain.Entities;
using BuberDinner.Domain.Errors;
using ErrorOr;
using MediatR;

namespace BuberDinner.Application.Authentication.Queries.Login;

public class LoginQuerydHandler : IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
{

    private readonly IJWTTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;

    public LoginQuerydHandler(
        IJWTTokenGenerator jWTTokenGenerator,
        IUserRepository userRepository)
    {
        _jwtTokenGenerator = jWTTokenGenerator;
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        // 1. Check if user exists
        if (_userRepository.GetUserByEmail(request.Email) is not User user)
        {
            return Errors.Authentication.InvalidCredentials;
        }

        // 2. Match password


        if (user.Password != request.Password)
        {
            return Errors.Authentication.InvalidCredentials;
        }

        // 3. Generate and return new token
        string token = _jwtTokenGenerator.GenerateJWTToken(user.Id
             , user.FirstName
             , user.LastName);


        return new AuthenticationResult(user, token);
    }
}