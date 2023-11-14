using System.Net;
using System.Net.Http.Headers;
using System.Text;
using Mytask.IntegrationTests.ExternalEnvironment;

namespace Mytask.IntegrationTests.Scenarios.Boards.Steps
{
    [Binding]
    internal class WhenBoardStepDefinitions
    {
        [When(@"пользователь создает канбан-доску с названием ""([^""]*)""")]
        public async Task ClientCreateBoardWithName(string name)
        {
            // Создаем доску 
            var board = new Board("111");
            board.Name = name;

            // Сериализуем в json
            var body = JsonSerializer.Serialize(board);

            // Создаем объект HTTP запрос
            var request = new HttpRequestMessage(HttpMethod.Post, "api/board")
            {
                // Контент
                Content = new StringContent(body, Encoding.UTF8, "application/json")
            };

            // Токен авторизации
            request.Headers.Authorization = AuthenticationHeaderValue.Parse("Bearer " + Common.AuthToken);

            // Отправляем запрос и сохраняем результат 
            Common.HttpResponseMessage = await ExtEnvironment.TestServer.CreateClient().SendAsync(request);
        }

        [When(@"пользователь обновляет название доски на ""([^""]*)""")]
        public async Task ClientUpdateBoardNameTo(string name)
        {
            // Создаём объект - DTO доски
            var board = new Board("111");
            board.Id = Common.Board.Id;
            board.Name = name;

            // Сериализуем в json
            var body = JsonSerializer.Serialize(board);

            // Создаем объект HTTP запрос
            var request = new HttpRequestMessage(HttpMethod.Put, "api/board")
            {
                // Контент
                Content = new StringContent(body, Encoding.UTF8, "application/json")
            };

            // Токен авторизации 
            request.Headers.Authorization = AuthenticationHeaderValue.Parse("Bearer " + Common.AuthToken);

            // Отправляем запрос и сохраняем результат
            Common.HttpResponseMessage = await ExtEnvironment.TestServer.CreateClient().SendAsync(request);
        }

        [When(@"пользователь запрашивает список всех досок")]
        public async Task ClientRequestAllBoards()
        {
            // Создаем объект HTTP запрос
            var request = new HttpRequestMessage(HttpMethod.Get, "api/board");

            // Токен авторизации
            request.Headers.Authorization = AuthenticationHeaderValue.Parse("Bearer " + Common.AuthToken);

            // Отправляем запрос и сохраняем результат
            Common.HttpResponseMessage = await ExtEnvironment.TestServer.CreateClient().SendAsync(request);
        }

        [When(@"пользователь удаляет доску")]
        public async Task ClientDeleteBoard()
        {
            // Создаем объект HTTP запрос
            var request = new HttpRequestMessage(HttpMethod.Delete, $"api/board/{Common.Board!.Id}");

            // Токен авторизации
            request.Headers.Authorization = AuthenticationHeaderValue.Parse("Bearer " + Common.AuthToken);

            // Отправляем запрос и сохраняем результат
            Common.HttpResponseMessage = await ExtEnvironment.TestServer.CreateClient().SendAsync(request);
        }
    }
}
