using ErrorOr;

namespace BuberDinner.Application.Service.Authentication;

public interface IAuthenticationService
{
    ErrorOr<AuthenticationResult> Register(
        string FirstName,
        string LastName,
        string Email,
        string Password);

    ErrorOr<AuthenticationResult> Login(
        string Email,
        string Password);

}