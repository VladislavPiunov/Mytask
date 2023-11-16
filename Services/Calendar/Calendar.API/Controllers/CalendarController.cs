using Calendar.API.Dtos;
using Calendar.API.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Calendar.API.Controllers
{
    [ApiController]
    [Route("api/calendar")]
    public class CalendarController : ControllerBase
    {
        public CalendarController() { }

        [HttpPost]
        public async Task<ActionResult<TaskDTO>> Test()
        {
            string endPoint = "http://localhost:8484/auth/realms/my_realm/protocol/openid-connect/token";
            var keycloakClient = new HttpClient();
            var data = new[]
            {
                new KeyValuePair<string, string>("client_id", "mytask-client"),
                new KeyValuePair<string, string>("client_secret", "JUImQpTmItjl6gtL0kimr6TbDrYHt0DW"),
                new KeyValuePair<string, string>("grant_type", "password"),
                new KeyValuePair<string, string>("scope", "openid"),
                new KeyValuePair<string, string>("username", "admin"),
                new KeyValuePair<string, string>("password", "admin")
            };

            var keycloakResponse = await keycloakClient.PostAsync(endPoint, new FormUrlEncodedContent(data));

            var accessToken = keycloakResponse.Content.ReadAs<Token>()!.AccessToken;

            var httpClient = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:8081/")
            };

            var firstRequest = new HttpRequestMessage(HttpMethod.Get, "api/board");
            firstRequest.Headers.Authorization = AuthenticationHeaderValue.Parse("Bearer " + accessToken);

            var firstResponse = await httpClient.SendAsync(firstRequest);

            var board = firstResponse.Content.ReadAs<List<BoardDTO>>().First(x => x.Name == "Myboard");

            var task = new TaskDTO
            {
                Id = "",
                Name = "Test Task",
                BoardId = board.Id,
                StageId = board.Stages.First(),
                Description = "test test",
                Executor = ""
            };

            var body = JsonSerializer.Serialize(task);

            var secondRequest = new HttpRequestMessage(HttpMethod.Post, $"api/task")
            {
                Content = new StringContent(body, Encoding.UTF8, "application/json"),
                Headers =
                {
                    Authorization = AuthenticationHeaderValue.Parse("Bearer " + accessToken)
                }
            };

            var secondResponse = await httpClient.SendAsync(secondRequest);

            task = secondResponse.Content.ReadAs<TaskDTO>();

            task.Deadline = DateTime.Now.AddDays(1);

            body = JsonSerializer.Serialize(task);

            var thirdRequest = new HttpRequestMessage(HttpMethod.Put, "api/task")
            {
                Content = new StringContent(body, Encoding.UTF8, "application/json"),
                Headers =
                {
                    Authorization = AuthenticationHeaderValue.Parse("Bearer " + accessToken)
                }
            };

            var thirdResponse = httpClient.SendAsync(thirdRequest).GetAwaiter().GetResult().Content.ReadAs<TaskDTO>();

            return Ok(thirdResponse);
        }
    }
}
