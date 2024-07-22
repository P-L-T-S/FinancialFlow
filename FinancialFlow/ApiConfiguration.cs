namespace FinancialFlow;

public static class ApiConfiguration
{
    public static readonly Guid UserId = new Guid("f504628f-2e2c-471e-bd6d-f74d71440135");

    public static string? ConnectionString { get; set; } = string.Empty;
    public static string CorsPolicyName = "wasm";
}