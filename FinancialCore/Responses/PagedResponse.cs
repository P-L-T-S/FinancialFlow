using System.Text.Json.Serialization;
using FinancialCore.Enums;

namespace FinancialCore.Responses;

public class PagedResponse<TData> : Response<TData>
{
    [JsonConstructor]
    public PagedResponse(TData? data,
        int totalCount,
        int currentPage = 1,
        int pageSize = Configuration.DefaultPageSize
    ) : base(data)
    {
        CurrentPage = currentPage;
        PageSize = pageSize;
        TotalCount = totalCount;
    }

    public PagedResponse(
        TData? data,
        EHttpStatusCode code = EHttpStatusCode.Success,
        string? message = null
    ) : base(
        data, code, message)
    {
    }

    public int CurrentPage { get; set; }
    public int TotalPage => (int)Math.Ceiling(TotalCount / (double)PageSize);
    public int PageSize { get; set; } = Configuration.DefaultPageSize;
    public int TotalCount { get; set; }
}