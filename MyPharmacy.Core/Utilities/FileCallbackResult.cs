using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;

namespace MyPharmacy.Core.Utilities;

public class FileCallbackResult(
    MediaTypeHeaderValue contentType,
    Func<Stream, ActionContext, Task> callback
)
    : IActionResult
{
    private readonly MediaTypeHeaderValue _contentType = contentType ?? throw new ArgumentNullException(nameof(contentType));
    private readonly Func<Stream, ActionContext, Task> _callback = callback ?? throw new ArgumentNullException(nameof(callback));

    public async Task ExecuteResultAsync(ActionContext context)
    {
        var response = context.HttpContext.Response;
        response.ContentType = _contentType.ToString();

        await _callback(context.HttpContext.Response.Body, context);
    }
}