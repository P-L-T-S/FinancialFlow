﻿namespace FinancialCore;

public static class Configuration
{
    public const int DefaultPageSize = 25;
    public const int DefaultPageNumber = 1;

    public static string BackendUrl { get; set; } = "http://localhost:5026";
    public static string FrontendUrl { get; set; } = "http://localhost:5228";
}