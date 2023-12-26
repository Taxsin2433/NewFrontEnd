namespace Catalog.Host.Data;

public class PaginatedItems<T>
{
    public long Count { get; set; }
    public IEnumerable<T>? Data { get; set; }
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; internal set; }
}