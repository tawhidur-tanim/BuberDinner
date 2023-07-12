namespace BuberDinner.Application.Common.Interfaces;

public interface IJWTTokenGenerator
{
    string GenerateJWTToken(Guid userId, string firstName, string lastName);
}