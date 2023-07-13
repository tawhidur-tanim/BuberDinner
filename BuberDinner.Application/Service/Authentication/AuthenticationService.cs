using BuberDinner.Application.Common.Interfaces;

namespace BuberDinner.Application.Service.Authentication;


public class AuthenticationService : IAuthenticationService
{

    private readonly IJWTTokenGenerator _jwtTokenGenerator;

    public AuthenticationService(IJWTTokenGenerator jWTTokenGenerator)
    {
        _jwtTokenGenerator = jWTTokenGenerator;
    }


    public AuthenticationResult Register(string firstName, string lastName, string email, string password)
    {
        // create jwt 



        Guid userId = Guid.NewGuid();
        string token = _jwtTokenGenerator.GenerateJWTToken(userId, firstName, lastName);

        return new AuthenticationResult(Guid.NewGuid(), firstName, lastName, email, token);
    }

    public AuthenticationResult Login(string email, string password)
    {

        return new AuthenticationResult(Guid.NewGuid(), "FirstName", "LastName", email, "token");
    }


}