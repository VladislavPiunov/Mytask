using Faq.Dtos;

namespace Mytask.IntegrationTests.Scenarios;

/// <summary>
/// Класс содержит данные, которые должны передаваться между шагами в сценарии
/// </summary>
public static class Common
{
    /// <summary>
    /// http ответ
    /// </summary>
    public static HttpResponseMessage? HttpResponseMessage { get; set; }

    /// <summary>
    /// Созданная категория
    /// </summary>
    public static CategoryDto? Category { get; set; }

    /// <summary>
    /// Созданный вопрос
    /// </summary>
    public static QuestionDto? Question { get; set; }

    /// <summary>
    /// токен авторизации
    /// </summary>
    public static string? AuthToken { get; set; }

    /// <summary>
    /// Почистить состояние между тесткейсами
    /// </summary>
    public static void ClearState()
    {
        HttpResponseMessage = null;
        Category = null;
        Question = null;
        AuthToken = null;
    }
}