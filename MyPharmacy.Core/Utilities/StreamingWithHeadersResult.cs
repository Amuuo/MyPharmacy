using System.Net.Http.Headers;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using MyPharmacy.Core.Utilities;
using MyPharmacy.Core.Utilities.Interfaces;

public class StreamingWithHeadersResult<T>(
    IPagedResult<T> pagedResult,
    MediaTypeHeaderValue? contentType
)
    : IActionResult
{
    public async Task ExecuteResultAsync(ActionContext context)
    {
        var response = context.HttpContext.Response;
        response.ContentType = contentType?.ToString();

        response.Headers.Add("X-Current-Page", pagedResult.CurrentPage.ToString());
        response.Headers.Add("X-Total-Pages",  pagedResult.TotalPages.ToString());
        response.Headers.Add("X-Page-Size",    pagedResult.PageSize.ToString());
        response.Headers.Add("X-Total-Count",  pagedResult.TotalCount.ToString());

        await using var writer = new StreamWriter(response.Body);
        await foreach (var item in pagedResult.Data)
        {
            var json = JsonSerializer.Serialize(item);
            await Task.Delay(200);
            await writer.WriteAsync(json + "\n");
            await writer.FlushAsync();
        }
    }
}