using Mytask.IntegrationTests.Helpers;
using Mytask.IntegrationTests.Helpers.JwtAuthorisation.Models;

namespace Mytask.IntegrationTests.Scenarios.CommonSteps
{
    [Binding]
    internal class WhenCommonStepDefinitions
    {
        [When(@"пользователь зарегистрирован в приложении")]
        public async Task ClientIsAdmin()
        {
            // Создаём HTTP клиент для отпавки запроса на получение токена
            using var httpClient = new HttpClient();

            // Задаём базовый адрес кейклоака
            httpClient.BaseAddress = new Uri($"http://localhost:8484");

            // Создаём запрос на получение токена
            var request = new HttpRequestMessage(HttpMethod.Post, $"/auth/realms/my_realm/protocol/openid-connect/token");

            // Отправляем запрос
            var response = await httpClient.SendAsync(request);

            // Десериализуем полученный ответ и прираниваем AccessToken для дальнейшего использования в запросах к сервису
            Common.AuthToken = response.Content.ReadAs<Token>()!.AccessToken;
        }
    }

}
