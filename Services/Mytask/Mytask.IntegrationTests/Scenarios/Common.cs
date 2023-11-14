namespace Mytask.IntegrationTests.Scenarios;

/// <summary>
/// Класс содержит данные, которые должны передаваться между шагами в сценарии
/// </summary>
internal static class Common
{
    /// <summary>
    /// http ответ
    /// </summary>
    public static HttpResponseMessage? HttpResponseMessage { get; set; }

    /// <summary>
    /// Созданная доска
    /// </summary>
    public static Board? Board { get; set; }

    /// <summary>
    /// Созданный статус
    /// </summary>
    public static Stage? Stage { get; set; }

    /// <summary>
    /// Созданная задача
    /// </summary>
    public static API.Model.Task? Task { get; set; }

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
        Board = null;
        Stage = null;
        Task = null;
        AuthToken = null;
    }
}