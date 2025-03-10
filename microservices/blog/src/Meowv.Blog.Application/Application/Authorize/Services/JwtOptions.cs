namespace Meowv.Blog.Application.Authorize.Services;

public class JwtOptions
{
    public double Expires { get; set; }
    public string SigningKey { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
}