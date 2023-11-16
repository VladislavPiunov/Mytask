using System.Text.Json;

namespace Calendar.API.Helpers;

/// <summary>
/// Методы расширения для десериализации данных
/// </summary>
internal static class DeserializationExtensions
{
    /// <summary>
    /// Десериализовать из HttpContent
    /// </summary>
    public static T? ReadAs<T>(this HttpContent content)
    {
        var stringContent = content.ReadAsStringAsync().Result;

        JsonSerializerOptions options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
             
        return JsonSerializer.Deserialize<T>(stringContent, options);
    }    
    
    /// <summary>
    /// Десериализовать из строки
    /// </summary>
    public static T? ReadAs<T>(this string stringContent)
    {
        JsonSerializerOptions options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
             
        return JsonSerializer.Deserialize<T>(stringContent, options);
    }
}