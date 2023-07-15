using BuberDinner.Application.Common.Errors;
using BuberDinner.Application.Common.Interfaces;
using BuberDinner.Application.Common.Persistence;
using BuberDinner.Domain.Entities;
using BuberDinner.Domain.Errors;
using ErrorOr;

namespace BuberDinner.Application.Service.Authentication;


public class AuthenticationService : IAuthenticationService
{

    private readonly IJWTTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;

    public AuthenticationService(IJWTTokenGenerator jWTTokenGenerator, IUserRepository userRepository)
    {
        _jwtTokenGenerator = jWTTokenGenerator;
        _userRepository = userRepository;
    }


    public ErrorOr<AuthenticationResult> Register(string firstName, string lastName, string email, string password)
    {

        // 1. Check if user exist
        User? user = _userRepository.GetUserByEmail(email);
        if (user is not null)
        {
            return Errors.User.DuplicateEmailError;
        }

        // 2. create a new user and save it to the db

        User newUser = new User
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            Password = password
        };

        _userRepository.Add(newUser);


        // 3. Generate Jwt token
        string token = _jwtTokenGenerator.GenerateJWTToken(newUser.Id
        , newUser.FirstName
        , newUser.LastName);

        return new AuthenticationResult(newUser.Id
        , newUser.FirstName
        , newUser.LastName, newUser.Email, token);
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