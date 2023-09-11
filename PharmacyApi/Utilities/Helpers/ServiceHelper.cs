using System.Net;
using Microsoft.AspNetCore.Mvc;
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

        public static async Task<IServiceResult<IPagedResult<Pharmacy>>> 
            BuildPagedResultAsync(IAsyncEnumerable<Pharmacy> pharmacyList, 
                                  PagedRequest pagedRequest, int totalCount)
        {
            var pagedResult = await BuildPagedResultAsync(pharmacyList, 
                                                          pagedRequest.PageNumber, 
                                                          pagedRequest.PageSize, 
                                                          totalCount);

            return BuildSuccessServiceResult(pagedResult);
        }


        private static async Task<IPagedResult<T>> 
            BuildPagedResultAsync<T>(IAsyncEnumerable<T> data,
                                     int currentPage,
                                     int pageSize,
                                     int totalCount)
        {
            return new PagedResult<T>
            {
                CurrentPage = currentPage,
                PageSize    = pageSize,
                TotalCount  = totalCount,
                TotalPages  = (int)Math.Ceiling(totalCount / (double)pageSize),
                Data        = data
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
    }
}
