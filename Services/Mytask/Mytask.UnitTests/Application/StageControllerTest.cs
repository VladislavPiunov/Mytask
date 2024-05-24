namespace Mytask.UnitTests.Application;

public class StageControllerTest
{
    private readonly Mock<IBoardRepository> _boardRepositoryMock;
    private readonly Mock<IStageRepository> _stageRepositoryMock;
    
    public StageControllerTest()
    {
        _boardRepositoryMock = new Mock<IBoardRepository>();
        _stageRepositoryMock = new Mock<IStageRepository>();
    }

    [Datapoint]
    public string firstUserId = "1";

    [Datapoint]
    public string secondUserId = "2";

    [Theory]
    public async Task Get_stages_async_success(string fakeUserId)
    {
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

    [Datapoint]
    public Stage firstStage = new Stage("test1", "test1")
    {
        Id = ObjectId.GenerateNewId().ToString()
    };

    [Datapoint]
    public Stage secondStage = new Stage("test2", "test2")
    {
        Id = ObjectId.GenerateNewId().ToString()
    };

    [Theory]
    public async Task Post_stage_async_success(Stage fakeStage)
    {
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

    [Theory]
    public async Task Put_stage_async_success(Stage fakeStage)
    {
        _stageRepositoryMock.Setup(x => x.UpdateStageAsync(It.IsAny<Stage>()))
            .Returns(Task.FromResult(fakeStage));

        var stageController = new StageController(
            _boardRepositoryMock.Object,
            _stageRepositoryMock.Object
        );

        var actionResult = await stageController.UpdateStageAsync(fakeStage);

        Assert.AreEqual((actionResult.Result as OkObjectResult).StatusCode, (int)System.Net.HttpStatusCode.OK);
        Assert.AreEqual((((ObjectResult)actionResult.Result).Value as Stage).Id, fakeStage.Id);
    }

    [Datapoint]
    public string firstStageId = "1";

    [Datapoint]
    public string secondStageId = "2";

    [Theory]
    public async Task Delete_stage_async_success(string fakeStageId)
    {
        _stageRepositoryMock.Setup(x => x.DeleteStageAsync(It.IsAny<string>()))
            .Returns(Task.FromResult(true));

        var stageController = new StageController(
            _boardRepositoryMock.Object,
            _stageRepositoryMock.Object
        );

        var actionResult = await stageController.DeleteStageAsync(fakeStageId);

        Assert.AreEqual((actionResult.Result as OkObjectResult).StatusCode, (int)System.Net.HttpStatusCode.OK);
        Assert.AreEqual(((ObjectResult)actionResult.Result).Value as bool?, true);
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