using Microsoft.AspNetCore.Mvc;

namespace PharmacyApi.Utilities
{
    public static class ControllerHelper
    {
        public static IActionResult HandleResponse<T>(ServiceResult<T> serviceResponse)
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