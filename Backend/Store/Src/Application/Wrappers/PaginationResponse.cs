namespace Application.Wrappers;

public class PaginationResponse<T>where T : class
{
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public int Count { get; set; }
    public IReadOnlyList<T> Result { get; set; }
}