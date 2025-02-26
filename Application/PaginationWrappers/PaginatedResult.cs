namespace Application.PaginationWrappers;

public record PaginatedResult<T>
{
    public List<T> Data { get; }
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public int TotalCount { get; set; }
    public bool HasNextPage => CurrentPage < TotalPages;
    public bool HasPreviousPage => CurrentPage > 1;

    public PaginatedResult(List<T> data = default, int pageNumber = 1, int pageSize = 15, int count = 1)
    {
        Data = data;
        CurrentPage = pageNumber;
        PageSize = pageSize;
        TotalCount = count;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);
    }
}