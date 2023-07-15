using ErrorOr;

namespace BuberDinner.Domain.Errors;

public static partial class Errors
{
    public static class Authentication
    {
        public static Error InvalidCredentials => Error.Validation(
            code: "user.InvalidCredentials",
            description: "Credentials do not match");
    }
}