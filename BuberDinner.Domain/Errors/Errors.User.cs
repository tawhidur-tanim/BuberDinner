using ErrorOr;

namespace BuberDinner.Domain.Errors;

public static partial class Errors
{
    public static class User
    {
        public static Error DuplicateEmailError => Error.Conflict(
            code: "user.EmailError",
            description: "Duplicate Email");
    }
}