namespace BikeShopManager.Domain.Entities.Auth;

public class RefreshToken
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public string TokenHash { get; set; } = string.Empty;
    public DateTime ExpiresAt { get; set; }
    public DateTime? RevokedAt { get; set; }
    public DateTime CreatedAt { get; set; }

    // When this token is rotated, we record the token that replaced it.
    public string? ReplacedByTokenHash { get; set; }

    // A token is only usable if it has not been revoked and has not expired.
    public bool IsActive => RevokedAt is null && DateTime.UtcNow < ExpiresAt;
}
