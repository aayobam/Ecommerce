namespace Application.PaginationWrappers;

public abstract class ResourceParameters
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public string? Search { get; set; }
    public string? SortBy { get; set; }
}
