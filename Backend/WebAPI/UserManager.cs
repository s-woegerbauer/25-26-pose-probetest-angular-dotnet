namespace WebAPI;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using Microsoft.IdentityModel.Tokens;

using WebAPI.Model;

/// <summary>
/// UserManager
/// </summary>
public class UserManager
{
    private IConfiguration _configuration;

    /// <summary>
    /// Constructor for LoginController.
    /// </summary>
    /// <param name="configuration"></param>
    public UserManager(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    /// <summary>
    /// Get the JWT.
    /// </summary>
    /// <param name="userInfo"></param>
    /// <returns></returns>
    public string GenerateJwt(UserInfo userInfo)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>()
        {
            new Claim(JwtRegisteredClaimNames.Name,  userInfo.Username),
            new Claim(JwtRegisteredClaimNames.Sub,   userInfo.Username),
            new Claim(JwtRegisteredClaimNames.Email, userInfo.EmailAddress),
            new Claim(JwtRegisteredClaimNames.Jti,   Guid.NewGuid().ToString())
        };

        if (!string.IsNullOrEmpty(userInfo.Roles))
        {
            claims.Add(new Claim(ClaimTypes.Role, userInfo.Roles));
        }

        var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
            _configuration["Jwt:Issuer"],
            claims,
            expires: DateTime.Now.AddMinutes(120),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    /// <summary>
    /// User authentication.
    /// </summary>
    /// <param name="login"></param>
    /// <returns></returns>
    public UserInfo? AuthenticateUser(User login)
    {
        var userinfo = _adminUsers.FirstOrDefault(user => user.Username == login.Username);
        if (userinfo is not null && login.Password == "Employee")
        {
            return userinfo;
        }

        if (login.Username.Length >= 3 && login.Password == "Guest1234")
        {
            return new UserInfo { Username = login.Username, EmailAddress = "guest@htl.at", Roles = "Guest" };
        }

        return null;
    }

    private static UserInfo[] _adminUsers = new[]
    {
        new UserInfo { Username = "Maxi", EmailAddress   = "admin@htl.at", Roles = "Employee" },
        new UserInfo { Username = "Franzi", EmailAddress = "admin@htl.at", Roles = "Employee" },
        new UserInfo { Username = "Seppi", EmailAddress  = "admin@htl.at", Roles = "Employee" },
    };
}