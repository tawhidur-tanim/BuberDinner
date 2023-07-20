using BuberDinner.Domain.Common.Entities;

namespace BuberDinner.Application.Authentication.Common;


public record AuthenticationResult(
    User user,
    string Token

);