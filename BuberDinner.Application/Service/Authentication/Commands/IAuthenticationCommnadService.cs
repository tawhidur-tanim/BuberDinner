using BuberDinner.Application.Service.Common;
using ErrorOr;

namespace BuberDinner.Application.Service.Authentication.Commands;

public interface IAuthenticationCommandService
{
    ErrorOr<AuthenticationResult> Register(
        string FirstName,
        string LastName,
        string Email,
        string Password);

}