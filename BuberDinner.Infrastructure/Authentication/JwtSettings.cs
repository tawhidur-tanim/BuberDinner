namespace BuberDinner.Infrastructure.Authentication;


public class JwtSettings
{
    public string Secret { get; set; } = null!;
    public string Issuer { get; set; } = null!;
    public string Audience { get; set; } = null!;
    public int ExpirationInMinutes { get; set; }
    public static string SectionName { get; set; } = "JwtSettings";
}
