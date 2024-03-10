using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
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

        response.Headers.Add("X-Current-Page", pagedResult.PagingInfo?.Page.ToString());
        response.Headers.Add("X-Total-Pages",  pagedResult.Pages.ToString());
        response.Headers.Add("X-Page-Size",    pagedResult.PagingInfo?.Take.ToString());
        response.Headers.Add("X-Total-Count",  pagedResult.Total.ToString());

        await using var writer = new StreamWriter(response.Body);
        //await foreach (var item in pagedResult.Data)
        //{
        //    var json = JsonSerializer.Serialize(item);
        //    await Task.Delay(200);
        //    await writer.WriteAsync(json + "\n");
        //    await writer.FlushAsync();
        //}
    }
}