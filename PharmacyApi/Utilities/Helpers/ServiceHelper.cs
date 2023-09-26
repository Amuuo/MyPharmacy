using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PharmacyApi.Models;
using PharmacyApi.Utilities.Interfaces;

namespace PharmacyApi.Utilities.Helpers
{
    public static class ServiceHelper
    {
        public static IServiceResult<T> BuildNoContentResult<T>(string message)
        {
            return new ServiceResult<T>
            {
                IsSuccess    = false,
                ErrorMessage = message,
                StatusCode   = HttpStatusCode.OK
            };
        }

        public static IServiceResult<T> BuildErrorServiceResult<T>(Exception ex, string operation)
        {
            return new ServiceResult<T>
            {
                IsSuccess    = false,
                ErrorMessage = $"An error occurred while {operation}, ex: {ex}",
                StatusCode   = HttpStatusCode.InternalServerError
            };
        }

        public static IServiceResult<T> BuildSuccessServiceResult<T>(T result)
        {
            return new ServiceResult<T>
            {
                IsSuccess  = true,
                Result     = result,
                StatusCode = HttpStatusCode.OK
            };
        }


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
}
