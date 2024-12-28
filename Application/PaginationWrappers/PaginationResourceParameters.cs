namespace Application.PaginationWrappers;

public class PaginationResourceParameters
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public string Search { get; set; }
    public string SortBy { get; set; }
}
