namespace Mytask.IntegrationTests.Scenarios.Boards
{
    internal class GivenBoardStepDefinitions
    {
        public JsonFilesRepository JsonFilesRepository { get; }

        private JsonSerializerOptions JsonSerializerOptions { get; } = new JsonSerializerOptions { 
            AllowTrailingCommas = true,
            PropertyNameCaseInsensitive = true
        };

        public GivenBoardStepDefinitions (JsonFilesRepository jsonFilesRepository)
        {
            JsonFilesRepository = jsonFilesRepository;
        }

        [Given("база данных имеет данные о канбан-досках")]
        public async Task GivenTheRepositoryHasBoardData()
        {
            var boardsJson = JsonFilesRepository.Files["boards.json"];
            var boards = JsonSerializer.Deserialize<IList<Board>>(boardsJson, JsonSerializerOptions);

            await
        }
    }
}
