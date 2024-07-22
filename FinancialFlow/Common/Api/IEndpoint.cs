namespace FinancialFlow.Common.Api;

public interface IEndpoint
{
    public static abstract void Map(IEndpointRouteBuilder route);
}