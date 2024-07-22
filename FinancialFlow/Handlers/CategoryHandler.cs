using FinancialCore.Enums;
using FinancialCore.Handlers;
using FinancialCore.Models;
using FinancialCore.Requests.Categories;
using FinancialCore.Responses;
using FinancialFlow.Data;
using Microsoft.EntityFrameworkCore;

namespace FinancialFlow.Handlers;

public class CategoryHandler(AppDataContext context) : ICategoryHandler
{
    public async Task<Response<Category?>> CreateAsync(CreateCategoryRequest request)
    {
        var res = new Response<Category?>();

        var category = new Category
        {
            Description = request.Description,
            Title = request.Title,
            UserId = request.UserId
        };

        try
        {
            await context.Categories.AddAsync(category);
            await context.SaveChangesAsync();

            return new Response<Category?>(category, EHttpStatusCode.Created);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            res.Message = "Ouve um erro e não foi possível criar a categoria";
            res.StatusCode = EHttpStatusCode.Error;
            return res;
        }
    }

    public async Task<Response<Category?>> UpdateAsync(UpdateCategoryRequest request)
    {
        var res = new Response<Category?>();

        try
        {
            var category = await context
                .Categories
                .FirstOrDefaultAsync(category =>
                    category.Id == request.Id && category.UserId == request.UserId
                );

            if (category == null)
            {
                res.Message = "Categoria não foi encontrada";
                res.StatusCode = EHttpStatusCode.NotFound;
                return res;
            }

            category.Title = request.Title;
            category.Description = request.Description;

            context.Categories.Update(category);
            await context.SaveChangesAsync();

            res.Message = "Categoria atualizada com sucesso";
            res.StatusCode = EHttpStatusCode.Success;
            res.Data = category;

            return res;
        }
        catch (Exception e)
        {
            res.Message = "Não foi possível atualizar a categoria";
            res.StatusCode = EHttpStatusCode.Error;
            return res;
        }
    }

    public async Task<Response<Category?>> DeleteAsync(DeleteCategoryRequest request)
    {
        var res = new Response<Category?>();
        try
        {
            var category = await context.Categories.FirstOrDefaultAsync(category =>
                category.Id == request.Id && category.UserId == request.UserId
            );

            if (category == null)
            {
                res.Message = "Categoria não encontrada";
                res.StatusCode = EHttpStatusCode.NotFound;

                return res;
            }

            context.Categories.Remove(category);
            await context.SaveChangesAsync();

            res.Message = "Categoria excluída com sucesso";
            res.StatusCode = EHttpStatusCode.Success;

            return res;
        }
        catch (Exception e)
        {
            res.Message = "Erro ao deletar usuário";
            res.StatusCode = EHttpStatusCode.Error;
            return res;
        }
    }

    public async Task<Response<Category?>> GetByIdAsync(GetCategoryByIdRequest request)
    {
        try
        {
            var category = await context.Categories
                .AsNoTracking()
                .FirstOrDefaultAsync(category =>
                    category.Id == request.Id && category.UserId == request.UserId
                );

            return category == null
                ? new Response<Category?>(null, EHttpStatusCode.NotFound, "Nenhuma categoria encontrada")
                : new Response<Category?>(category, EHttpStatusCode.Success);
        }
        catch (Exception e)
        {
            return new Response<Category?>(null, EHttpStatusCode.Error, "Erro ao buscar categoria");
        }
    }

    public async Task<PagedResponse<List<Category>>> GetAllAsync(GetAllCategoryRequest request)
    {
        try
        {
            var query = context
                .Categories
                .AsNoTracking()
                .Where(x => x.UserId == request.UserId)
                .OrderBy(x => x.Title)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize);

            var categories = await query
                .ToListAsync();

            var count = await query.CountAsync();

            return new PagedResponse<List<Category>>(categories, count, request.PageNumber, request.PageSize);
        }
        catch (Exception e)
        {
            return new PagedResponse<List<Category>>(null, 0, request.PageNumber, request.PageSize);
        }
    }
}