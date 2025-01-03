﻿namespace Application.PaginationWrappers;

public class PaginatedResult<T>
{
    public List<T> Data { get; }
    public List<string> Messages { get; set; } = new();
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public int TotalCount { get; set; }
    public bool Succedded { get; set; }
    public bool HasNextPage => CurrentPage < TotalPages;
    public bool HasPreviousPage => CurrentPage > 1;

    public PaginatedResult(List<T> data)
    {
        Data = data;
    }

    internal PaginatedResult(bool succeeded, List<T> data = default, List<string> messages = null, 
        int count = 0, int pageNumber = 1, int pageSize = 10)
    {
        Succedded = succeeded;
        Data = data;
        Messages = messages; 
        CurrentPage = pageNumber;
        PageSize = pageSize;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        TotalCount = count;
    }

    public static PaginatedResult<T> Success(List<T> data, int count, int pageNumber, int pageSize)
    {
        return new PaginatedResult<T>(true, data, null, count, pageNumber, pageSize);
    }
}