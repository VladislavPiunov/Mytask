namespace Mytask.IntegrationTests.Scenarios.StepDefinitions
{
    internal class BoardStepDefinitions
    {
        private const string BaseAddress = "http://localhost/";
        public WebApplicationFactory<Program> Factory { get; }
        public IBoardRepository Repository { get; }
        public HttpClient Client { get; set; } = null!;
        private HttpResponseMessage Response { get; set; } = null!;
        public JsonFilesRepository JsonFilesRepo { get; }
        private Board? Entity { get; set; }

        private JsonSerializerOptions JsonSerializerOptions { get; } = new JsonSerializerOptions
        {
            AllowTrailingCommas = true,
            PropertyNameCaseInsensitive = true
        };

        public BoardStepDefinitions(
            WebApplicationFactory<Program> factory,
            IBoardRepository repository,
            JsonFilesRepository jsonFilesRepo)
        {
            Factory = factory;
            Repository = repository;
            JsonFilesRepo = jsonFilesRepo;
        }

        [Given(@"I am a client")]
        public void GivenIAmAClient()
        {
            Client = Factory.CreateDefaultClient(new Uri(BaseAddress));
        }

        [Given(@"the repository has board data")]
        public async Task GivenTheRepositoryHasBoardData()
        {
            var boardsJson = JsonFilesRepo.Files["boards.json"];
            var boards = JsonSerializer.Deserialize<IList<Board>>(boardsJson, JsonSerializerOptions);
            if (boards != null)
                foreach (var board in boards)
                    await Repository.CreateBoardAsync(board);
        }
    }
}
