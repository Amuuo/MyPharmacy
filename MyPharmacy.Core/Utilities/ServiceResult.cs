using System.Net;
using Microsoft.AspNetCore.Mvc;
using MyPharmacy.Core.Utilities.Interfaces;

namespace MyPharmacy.Core.Utilities;

public class ServiceResult<T> : IServiceResult<T>
{
    public T? Result { get; set; }
    public bool IsSuccess { get; set; }
    public string? ErrorMessage { get; set; }
    public HttpStatusCode StatusCode { get; set; }

    public Task ExecuteResultAsync(ActionContext context)
    {
        throw new NotImplementedException();
    }
}