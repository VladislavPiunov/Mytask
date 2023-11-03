using Mytask.IntegrationTests.Helpers;
using Mytask.IntegrationTests.Helpers.JwtAuthorisation.Models;

namespace Mytask.IntegrationTests.Scenarios.CommonSteps
{
    [Binding]
    internal class WhenCommonStepDefinitions
    {
        [When("пользователь является администратором приложения")]
        public async Task ClientIsAdmin()
        {
            // создаём http клиент для отпавки запроса на получение токена
            using var httpClient = new HttpClient();

            // задаём базовый адрес кейклоака
            httpClient.BaseAddress = new Uri($"http://localhost:8484");

            // создаём запрос на получение токена
            var request = new HttpRequestMessage(HttpMethod.Post, $"/auth/realms/my_realm/protocol/openid-connect/token");

            // отправляем запрос
            var response = await httpClient.SendAsync(request);

            // десериализуем полученный ответ и прихраниваем AccessToken для дальнейшего использования в запросах к сервису
            Common.AuthToken = response.Content.ReadAs<Token>()!.AccessToken;
        }
    }

}
