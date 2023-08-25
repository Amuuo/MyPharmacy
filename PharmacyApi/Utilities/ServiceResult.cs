namespace PharmacyApi.Utilities
{
    public class ServiceResult<T>
    {
        public T? Result { get; set; }
        public bool Success { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
