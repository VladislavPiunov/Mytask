using Mytask.IntegrationTests.Helpers;
using System.Net;

namespace Mytask.IntegrationTests.Scenarios.Boards.Steps
{
    [Binding]
    internal class ThenBoardStepDefinitions
    {
        [Then(@"доска успешно создана")]
        public void BoardSuccessfullyCreated()
        {
            // Проверим, что ответ на запрос не пустой
            Common.HttpResponseMessage.Should().NotBeNull();

            // Проверим, что код ответа 200
            Common.HttpResponseMessage!.StatusCode.Should().Be(HttpStatusCode.OK);

            // Десериализуем и сохраним полученную в ответе категорию
            Common.Board = Common.HttpResponseMessage.Content.ReadAs<Board>();
        }

        [Then(@"название доски ""([^""]*)""")]
        public void BoardNameShouldBe(string name)
        {
            // Название категории должно быть {name}
            Common.Board!.Name.Should().Be(name);
        }

        [Then(@"доска успешно обновлена")]
        public void BoardSuccessfullyUpdated()
        {
            // Проверим, что ответ на запрос не пустой
            Common.HttpResponseMessage.Should().NotBeNull();

            // Проверим, что код ответа 200
            Common.HttpResponseMessage!.StatusCode.Should().Be(HttpStatusCode.OK);

            // Десериализуем и сохраним полученную в ответе категорию
            Common.Board = Common.HttpResponseMessage.Content.ReadAs<Board>();
        }

        [Then(@"доска успешно удалена")]
        public void CategorySuccessfullyDeleted()
        {
            // Проверим, что ответ на запрос не пустой
            Common.HttpResponseMessage.Should().NotBeNull();
            
            // Проверим, что код ответа 200
            Common.HttpResponseMessage!.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Then(@"список досок получен c количеством элементов (.*)")]
        public void ClientGotCategoryListWithCount(int count)
        {
            // Проверим, что ответ на запрос не пустой
            Common.HttpResponseMessage.Should().NotBeNull();

            // Проверим, что код ответа 200
            Common.HttpResponseMessage!.StatusCode.Should().Be(HttpStatusCode.OK);

            // Десериализуем полученный список категорий
            var result = Common.HttpResponseMessage.Content.ReadAs<List<Board>>();

            // Десериализуем и сохраним полученную в ответе категорию
            Common.Board = result[0];

            // Количество категорий в списке должно быть {count}
            result!.Count.Should().Be(count);
        }
    }
}
