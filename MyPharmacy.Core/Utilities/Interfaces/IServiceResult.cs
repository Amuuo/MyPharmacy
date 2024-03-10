using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace MyPharmacy.Core.Utilities.Interfaces;

public interface IServiceResult<T> : IActionResult
{
    T? Result { get; set; }
    bool IsSuccess { get; set; }
    string? ErrorMessage { get; set; }
    HttpStatusCode StatusCode { get; set; }
}