using BuberDinner.Application.Common.Errors;
using BuberDinner.Application.Common.Interfaces;
using BuberDinner.Application.Common.Persistence;
using BuberDinner.Application.Service.Common;
using BuberDinner.Domain.Entities;
using BuberDinner.Domain.Errors;
using ErrorOr;

namespace BuberDinner.Application.Service.Authentication.Queries;


public class AuthenticationQueryService : IAuthenticationQueryService
{

    private readonly IJWTTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;

    public AuthenticationQueryService(IJWTTokenGenerator jWTTokenGenerator, IUserRepository userRepository)
    {
        _jwtTokenGenerator = jWTTokenGenerator;
        _userRepository = userRepository;
    }

    public ErrorOr<AuthenticationResult> Login(string email, string password)
    {

        // 1. Check if user exists
        if (_userRepository.GetUserByEmail(email) is not User user)
        {
            return Errors.Authentication.InvalidCredentials;
        }

        // 2. Match password


        if (user.Password != password)
        {
            return Errors.Authentication.InvalidCredentials;
        }

        // 3. Generate and return new token
        string token = _jwtTokenGenerator.GenerateJWTToken(user.Id
             , user.FirstName
             , user.LastName);


        return new AuthenticationResult(user.Id, user.FirstName, user.LastName, user.Email, token);
    }


}