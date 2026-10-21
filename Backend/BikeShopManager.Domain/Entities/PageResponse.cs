namespace BikeShopManager.Domain.Entities;

public class PageResponse<T> : Audit
{
    public IEnumerable<T> Data { get; init; } = default!;
    public int PageNumber { get; init; }
    public int PageSize { get; init; }
    public int TotalPages { get; init; }
    public int TotalRecords { get; init; }
    public bool HasNextPage => PageNumber < TotalPages;
    public bool HasPreviousPage => PageNumber > 1;
}
