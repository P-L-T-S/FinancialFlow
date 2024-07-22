using System.Text.Json.Serialization;
using FinancialCore.Enums;

namespace FinancialCore.Responses;

public class Response<TData>
{
    [JsonConstructor]
    public Response() => StatusCode = EHttpStatusCode.Success;
 
    public Response(
        TData? data,
        EHttpStatusCode code = EHttpStatusCode.Success,
        string? message = null
    )
    {
        Data = data;
        StatusCode = code;
        Message = message;
        // SetIsSuccess(code);
    }

    public EHttpStatusCode StatusCode { get; set; }
    //
    // private void SetIsSuccess(EHttpStatusCode code)
    // {
    //     IsSuccess = code is >= (EHttpStatusCode)200 and <= (EHttpStatusCode)299;
    // }
    // public bool IsSuccess { get; set; } = true;

    public bool IsSuccess => StatusCode is >= (EHttpStatusCode)200 and <= (EHttpStatusCode)299;

    // public bool IsSuccess => StatusCode is >= (EHttpStatusCode)200 and <= (EHttpStatusCode)299;
    
    public string? Message { get; set; }

    public TData? Data { get; set; }
}