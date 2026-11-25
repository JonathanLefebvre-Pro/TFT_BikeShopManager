using BikeShopManager.Domain.Entities.Shared;

namespace BikeShopManager.Domain.Entities;

public class PageResponse<T> : IAudit
{
    public IEnumerable<T> Data { get; init; } = default!;
    public int PageNumber { get; init; }
    public int PageSize { get; init; }
    public int TotalPages { get; init; }
    public int TotalRecords { get; init; }
    public bool HasNextPage => PageNumber < TotalPages;
    public bool HasPreviousPage => PageNumber > 1;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
