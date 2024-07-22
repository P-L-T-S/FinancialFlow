using System.Net.Http.Json;
using FinancialCore.Enums;
using FinancialCore.Handlers;
using FinancialCore.Models;
using FinancialCore.Requests.Categories;
using FinancialCore.Responses;

namespace FinancialApp.Handlers;

public class CategoryHandler : ICategoryHandler
{
    private readonly ILogger<CategoryHandler> _logger;
    private readonly HttpClient _http;

    public CategoryHandler(ILogger<CategoryHandler> logger, IHttpClientFactory httpClientFactory)
    {
        _logger = logger;
        _http = httpClientFactory.CreateClient(WebConfiguration.HttpClientName);
    }
    
    public async Task<Response<Category?>> CreateAsync(CreateCategoryRequest request)
    {
        try
        {
            var response = await _http.PostAsJsonAsync("v1/categories", request);

            var result = await response.Content.ReadFromJsonAsync<Response<Category?>>();

            if (result == null)
            {
                return new Response<Category?>
                {
                    Data = null,
                    StatusCode = EHttpStatusCode.BadRequest,
                    Message = "Erro ao criar categoria"
                };
            }

            return result;
        }
        catch (Exception e)
        {
            return new Response<Category?>
            {
                Data = null,
                StatusCode = EHttpStatusCode.BadRequest,
                Message = e.Message
            };
        }
    }

    public async Task<Response<Category?>> UpdateAsync(UpdateCategoryRequest request)
    {
        try
        {
            var response = await _http.PutAsJsonAsync($"v1/categories/{request.Id}", request);
            var result = await response.Content.ReadFromJsonAsync<Response<Category?>>();

            if (result == null)
            {
                return new Response<Category?>
                {
                    Data = null,
                    StatusCode = EHttpStatusCode.BadRequest,
                    Message = "Erro ao editar categoria"
                };
            }

            return result;
        }
        catch (Exception e)
        {
            return new Response<Category?>
            {
                Data = null,
                StatusCode = EHttpStatusCode.BadRequest,
                Message = "Erro ao editar categoria"
            };
        }
    }

    public async Task<Response<Category?>> DeleteAsync(DeleteCategoryRequest request)
    {
        try
        {
            var response = await _http.DeleteAsync($"v1/categories/{request.Id}");
            var result = await response.Content.ReadFromJsonAsync<Response<Category?>>();

            if (result == null)
            {
                return new Response<Category?>
                {
                    Data = null,
                    StatusCode = EHttpStatusCode.BadRequest,
                    Message = "Erro ao deletar categoria"
                };
            }

            return result;
        }
        catch (Exception e)
        {
            return new Response<Category?>
            {
                Data = null,
                StatusCode = EHttpStatusCode.BadRequest,
                Message = "Erro ao deletar categoria"
            };
        }
    }

    public async Task<Response<Category?>> GetByIdAsync(GetCategoryByIdRequest request)
    {
        try
        {
            var response = await _http
                .GetAsync($"v1/categories/{request.Id}");

            var result = await response
                .Content
                .ReadFromJsonAsync<Response<Category?>>();

            if (result == null)
            {
                return new Response<Category?>
                {
                    Data = null,
                    StatusCode = EHttpStatusCode.BadRequest,
                    Message = "Erro ao buscar categorias"
                };
            }

            return result;
        }
        catch (Exception e)
        {
            return new Response<Category?>
            {
                Data = null,
                StatusCode = EHttpStatusCode.BadRequest,
                Message = "Erro ao buscar categorias"
            };
        }
    }

    public async Task<PagedResponse<List<Category>>> GetAllAsync(GetAllCategoryRequest request)
    {
        try
        {
            var result = await _http.GetFromJsonAsync<PagedResponse<List<Category>>>(
                "/v1/categories"
            );

            if (result == null)
            {
                return new PagedResponse<List<Category>>(
                    null,
                    EHttpStatusCode.BadRequest,
                    "Erro ao buscar categoria"
                );
            }

            return result;
        }
        catch (Exception e)
        {
            return new PagedResponse<List<Category>>(
                null,
                EHttpStatusCode.BadRequest,
                e.Message
            );
        }
    }
}