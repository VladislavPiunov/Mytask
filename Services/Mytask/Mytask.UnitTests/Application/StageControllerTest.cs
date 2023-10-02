namespace Mytask.UnitTests;

public class StageControllerTest
{
    private readonly Mock<IBoardRepository> _boardRepositoryMock;
    private readonly Mock<IStageRepository> _stageRepositoryMock;
    
    public StageControllerTest()
    {
        _boardRepositoryMock = new Mock<IBoardRepository>();
        _stageRepositoryMock = new Mock<IStageRepository>();
    }

    [Test]
    public async Task Get_stages_async_success()
    {
        var fakeUserId = "1";
        var fakeStagesList = GetStagesFake();
        var fakeBoard = GetBoardFake(fakeUserId, fakeStagesList.Select(x => x.Id).ToList());
        
        
        _boardRepositoryMock.Setup(x => x.GetBoardByIdAsync(It.IsAny<string>()))
            .Returns(Task.FromResult(fakeBoard));
        _stageRepositoryMock.Setup(x => x.GetStagesAsync(It.IsAny<List<string>>()))
            .Returns(Task.FromResult(fakeStagesList));

        var stageController = new StageController(
            _boardRepositoryMock.Object,
            _stageRepositoryMock.Object
        );

        var actionResult = await stageController.GetStagesAsync(fakeBoard.Id);
        
        Assert.AreEqual((actionResult.Result as OkObjectResult).StatusCode, (int)System.Net.HttpStatusCode.OK);
        Assert.AreEqual((((ObjectResult)actionResult.Result).Value as List<Stage>), fakeStagesList);
    }

    [Test]
    public async Task Post_stage_async_success()
    {
        var fakeStage = new Stage("test", "test")
        {
            Id = ObjectId.GenerateNewId().ToString()
        };

        _stageRepositoryMock.Setup(x => x.CreateStageAsync(It.IsAny<Stage>()))
            .Returns(Task.FromResult(fakeStage));

        var stageController = new StageController(
            _boardRepositoryMock.Object,
            _stageRepositoryMock.Object
        );

        var actionResult = await stageController.CreateStageAsync(fakeStage);
        
        Assert.AreEqual((actionResult.Result as OkObjectResult).StatusCode, (int)System.Net.HttpStatusCode.OK);
        Assert.AreEqual((((ObjectResult)actionResult.Result).Value as Stage).Id, fakeStage.Id);
    }
    
    private Board GetBoardFake(string fakeUserId, List<string> fakeStagesList)
    {
        return new Board(fakeUserId)
        {
            Id = ObjectId.GenerateNewId().ToString(),
            Stages = fakeStagesList
        };
    }

    private List<Stage> GetStagesFake()
    {
        return new List<Stage>
        {
            new Stage("test1", "test1")
            {
                Id = ObjectId.GenerateNewId().ToString()
            },
            new Stage("test2", "test2")
            {
                Id = ObjectId.GenerateNewId().ToString()
            },
            new Stage("test3", "test3")
            {
                Id = ObjectId.GenerateNewId().ToString()
            }
        };
    }
}