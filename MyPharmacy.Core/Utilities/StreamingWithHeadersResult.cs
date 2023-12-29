using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using MyPharmacy.Core.Utilities.Interfaces;
using Newtonsoft.Json;

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
            var json = JsonConvert.SerializeObject(item);
            await writer.WriteAsync(json);
            await writer.FlushAsync();
        }
    }
}