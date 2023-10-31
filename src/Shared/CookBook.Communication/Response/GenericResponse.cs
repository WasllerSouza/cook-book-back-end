namespace CookBook.Communication.Response;

public class GenericResponse<T>
{
    public T Data { get; set; }
    public T Errors { get; set; }
    public int StatusCode { get; set; }
    public bool Success { get; set; }
    public string Message { get; set; }

}
