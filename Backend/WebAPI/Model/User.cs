namespace WebAPI.Model;

/// <summary>
/// Record for the User.
/// </summary>
public class User
{
    /// <summary>
    /// Uername.
    /// </summary>
    public required string Username { get; set; }

    /// <summary>
    /// Password (only valid while login)
    /// </summary>
    public required string Password { get; set; }
}