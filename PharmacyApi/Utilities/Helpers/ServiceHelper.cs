using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PharmacyApi.Models;
using PharmacyApi.Utilities.Interfaces;

namespace PharmacyApi.Utilities.Helpers;

public static class ServiceHelper
{
    /// <summary>
    /// Builds a service result with no content.
    /// </summary>
    /// <typeparam name="T">The type of the result.</typeparam>
    /// <param name="message">The error message.</param>
    /// <returns>The service result with no content.</returns>
    public static IServiceResult<T> BuildNoContentResult<T>(string message)
    {
        return new ServiceResult<T>
        {
            IsSuccess    = false,
            ErrorMessage = message,
            StatusCode   = HttpStatusCode.OK
        };
    }

    /// <summary>
    /// Builds an error service result.
    /// </summary>
    /// <typeparam name="T">The type of the result.</typeparam>
    /// <param name="ex">The exception that occurred.</param>
    /// <param name="operation">The operation being performed.</param>
    /// <returns>The error service result.</returns>
    public static IServiceResult<T> BuildErrorServiceResult<T>(Exception ex, string operation)
    {
        return new ServiceResult<T>
        {
            IsSuccess    = false,
            ErrorMessage = $"An error occurred while {operation}, ex: {ex}",
            StatusCode   = HttpStatusCode.InternalServerError
        };
    }

    /// <summary>
    /// Builds a success service result.
    /// </summary>
    /// <typeparam name="T">The type of the result.</typeparam>
    /// <param name="result">The result.</param>
    /// <returns>The success service result.</returns>
    public static IServiceResult<T> BuildSuccessServiceResult<T>(T result)
    {
        return new ServiceResult<T>
        {
            IsSuccess  = true,
            Result     = result,
            StatusCode = HttpStatusCode.OK
        };
    }

    /// <summary>
    /// Handles the service response and returns an appropriate action result.
    /// </summary>
    /// <typeparam name="T">The type of the result.</typeparam>
    /// <param name="serviceResponse">The service response.</param>
    /// <returns>The action result.</returns>
    public static IActionResult HandleResponse<T>(this IServiceResult<T> serviceResponse)
    {
        return serviceResponse.IsSuccess
            ? new ObjectResult(serviceResponse.Result)
            {
                StatusCode = (int)serviceResponse.StatusCode
            }
            : new ContentResult
            {
                Content    = serviceResponse.ErrorMessage,
                StatusCode = (int)serviceResponse.StatusCode
            };
    }

    /// <summary>
    /// Retrieves a paged result asynchronously.
    /// </summary>
    /// <typeparam name="T">The type of the result.</typeparam>
    /// <param name="logger">The logger.</param>
    /// <param name="query">The queryable data.</param>
    /// <param name="pageNumber">The page number.</param>
    /// <param name="pageSize">The page size.</param>
    /// <returns>The paged result.</returns>
    public static async Task<IServiceResult<IPagedResult<T>>> GetPagedResultAsync<T>(
        ILogger logger,
        IQueryable<T> query, 
        int pageNumber, 
        int pageSize) where T : class
    {
        try
        {
            var startRow = pageNumber * pageSize;

            var entities = query
                .Skip(startRow)
                .Take(pageSize)
                .ToAsyncEnumerable();

            if (await entities.AnyAsync())
            {
                return await BuildPagedServiceResultAsync(
                    entities,
                    pageNumber, 
                    pageSize,
                    await query.CountAsync());
            }

            return BuildNoContentResult<IPagedResult<T>>("No entities found.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while retrieving paged results.");
            return BuildErrorServiceResult<IPagedResult<T>>(ex, 
                "retrieving entities.");
        }
    }

    /// <summary>
    /// Builds a paged service result asynchronously.
    /// </summary>
    /// <typeparam name="T">The type of the result.</typeparam>
    /// <param name="data">The paged data.</param>
    /// <param name="currentPage">The current page number.</param>
    /// <param name="pageSize">The page size.</param>
    /// <param name="totalCount">The total count of items.</param>
    /// <returns>The paged service result.</returns>
    public static async Task<IServiceResult<IPagedResult<T>>> BuildPagedServiceResultAsync<T>(
        IAsyncEnumerable<T> data, 
        int currentPage, 
        int pageSize, 
        int totalCount) where T : class
    {
        var pagedResult = new PagedResult<T>
        {
            CurrentPage = currentPage,
            PageSize    = pageSize,
            TotalCount  = totalCount,
            TotalPages  = (int)Math.Ceiling(totalCount / (double)pageSize),
            Data        = data
        };

        return BuildSuccessServiceResult<IPagedResult<T>>(pagedResult);
    }
}

