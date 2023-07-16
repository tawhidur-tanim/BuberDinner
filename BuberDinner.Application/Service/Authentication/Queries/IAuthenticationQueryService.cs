using BuberDinner.Application.Service.Common;
using ErrorOr;

namespace BuberDinner.Application.Service.Authentication.Queries;

public interface IAuthenticationQueryService
{

    ErrorOr<AuthenticationResult> Login(
        string Email,
        string Password);

}