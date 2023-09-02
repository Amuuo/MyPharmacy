using System.Net;

namespace PharmacyApi.Utilities.Interfaces
{
    public interface IServiceResult<T>
    {
        T? Result { get; set; }
        bool IsSuccess { get; set; }
        string? ErrorMessage { get; set; }
        HttpStatusCode StatusCode { get; set; }
    }
}
