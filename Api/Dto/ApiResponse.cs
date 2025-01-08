using System.Text.Json;
using System.Text.Json.Serialization;

namespace WebApplication1.Dto;

public class ApiResponse<T>
{
    public ApiResponse()
    {
        Success = true;
    }

    public ApiResponse(string error)
    {
        Error = error;
        Success = false;
    }

    public ApiResponse(T data)
    {
        Data = data;
        Success = true;
    }
    public ApiResponse(T data, string error)
    {
        Data = data;
        Success = true;
        Error = error;
    }
    
    [JsonPropertyName("success")]
    public bool Success { get; set; }
    
    [JsonPropertyName("data")]
    public T Data { get; set; }
    
    [JsonPropertyName("error")]
    public string Error { get; set; }
    
    // [JsonPropertyName("code")]
    // public int Code { get; set; }
}
// public class ApiResponse : ApiResponse<object>
// {
//
// }