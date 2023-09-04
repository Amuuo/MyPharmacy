using Microsoft.AspNetCore.Mvc;
using PharmacyApi.Utilities.Interfaces;

namespace PharmacyApi.Utilities
{
    public static class ControllerHelper
    {
        public static IActionResult HandleResponse<T>(IServiceResult<T> serviceResponse)
        {
            return serviceResponse.IsSuccess
                ? new ObjectResult(serviceResponse.Result)
                {
                    StatusCode = (int)serviceResponse.StatusCode
                }
                : new ContentResult
                {
                    Content = serviceResponse.ErrorMessage,
                    StatusCode = (int)serviceResponse.StatusCode
                };
        }
    }
}