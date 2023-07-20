
using BuberDinner.Application.Authentication.Common;
using BuberDinner.Application.Common.Interfaces;
using BuberDinner.Application.Common.Persistence;
using BuberDinner.Domain.Common.Entities;

using ErrorOr;
using MediatR;

namespace BuberDinner.Application.Authentication.Commands.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
{

    private readonly IJWTTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;

    public RegisterCommandHandler(
        IJWTTokenGenerator jWTTokenGenerator,
        IUserRepository userRepository)
    {
        _jwtTokenGenerator = jWTTokenGenerator;
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {

        await Task.CompletedTask;

        User? user = _userRepository.GetUserByEmail(request.Email);
        if (user is not null)
        {
            return Domain.Common.Errors.Errors.User.DuplicateEmailError;
        }

        // 2. create a new user and save it to the db

        User newUser = new User
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            Password = request.Password
        };

        _userRepository.Add(newUser);


        // 3. Generate Jwt token
        string token = _jwtTokenGenerator.GenerateJWTToken(newUser.Id
        , newUser.FirstName
        , newUser.LastName);

        return new AuthenticationResult(newUser, token);
    }
}