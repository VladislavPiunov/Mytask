namespace Mytask.UnitTests.Application;

public class BoardControllerTest
{
    private readonly Mock<IBoardRepository> _boardRepositoryMock;
    private readonly Mock<IIdentityService> _identityServiceMock;

    public BoardControllerTest()
    {
        _boardRepositoryMock = new Mock<IBoardRepository>();
        _identityServiceMock = new Mock<IIdentityService>();
    }

    [Datapoint]
    public string firstUserId = "1";
    
    [Datapoint]
    public string secondUserId = "2";

    [Theory]
    public async Task Get_boards_async_success(string fakeUserId)
    {
        var fakeBoardList = new List<Board> { GetBoardFake(fakeUserId) };

        _identityServiceMock.Setup(x => x.GetUserIdentity()).Returns(fakeUserId);
        _boardRepositoryMock.Setup(x => x.GetBoardsAsync(It.IsAny<string>())).Returns(Task.FromResult(fakeBoardList));

        var boardController = new BoardController(
            _boardRepositoryMock.Object,
            _identityServiceMock.Object
        );

        var actionResult = await boardController.GetBoardsAsync();
        
        Assert.AreEqual((actionResult.Result as OkObjectResult).StatusCode, (int)System.Net.HttpStatusCode.OK);
        Assert.AreEqual((((ObjectResult)actionResult.Result).Value as List<Board>).First().OwnerId, fakeUserId);
        Assert.AreEqual((((ObjectResult)actionResult.Result).Value as List<Board>), fakeBoardList);
    }

    [Theory]
    public async Task Post_board_async_success(string fakeUserId)
    {
        var fakeBoard = GetBoardFake(fakeUserId);

        _identityServiceMock.Setup(x => x.GetUserIdentity()).Returns(fakeUserId);
        _boardRepositoryMock.Setup(x => x.CreateBoardAsync(It.IsAny<Board>())).Returns(Task.FromResult(fakeBoard));

        var boardController = new BoardController(
            _boardRepositoryMock.Object,
            _identityServiceMock.Object
        );

        var actionResult = await boardController.CreateBoardAsync(fakeBoard);
        
        Assert.AreEqual((actionResult.Result as OkObjectResult).StatusCode, (int)System.Net.HttpStatusCode.OK);
        Assert.AreEqual((((ObjectResult)actionResult.Result).Value as Board).OwnerId, fakeUserId);
    }

    [Theory]
    public async Task Put_board_async_success(string fakeUserId)
    {
        var fakeBoard = GetBoardFake(fakeUserId);

        _identityServiceMock.Setup(x => x.GetUserIdentity()).Returns(fakeUserId);
        _boardRepositoryMock.Setup(x => x.UpdateBoardAsync(It.IsAny<Board>())).Returns(Task.FromResult(fakeBoard));

        var boardController = new BoardController(
            _boardRepositoryMock.Object,
            _identityServiceMock.Object
        );

        var actionResult = await boardController.UpdateBoardAsync(fakeBoard);

        Assert.AreEqual((actionResult.Result as OkObjectResult).StatusCode, (int)System.Net.HttpStatusCode.OK);
        Assert.AreEqual((((ObjectResult)actionResult.Result).Value as Board).OwnerId, fakeUserId);
    }

    [Theory]
    public async Task Delete_board_async_success(string fakeUserId)
    {
        var fakeBoardId = "2";

        _identityServiceMock.Setup(x => x.GetUserIdentity()).Returns(fakeUserId);
        _boardRepositoryMock.Setup(x => x.DeleteBoardAsync(It.IsAny<string>())).Returns(Task.FromResult(true));

        var boardController = new BoardController(
            _boardRepositoryMock.Object,
            _identityServiceMock.Object
        );

        var actionResult = await boardController.DeleteBoardAsync(fakeBoardId);

        Assert.AreEqual((actionResult.Result as OkObjectResult).StatusCode, (int)System.Net.HttpStatusCode.OK);
        Assert.AreEqual((((ObjectResult)actionResult.Result).Value as bool?), true);
    }

    private Board GetBoardFake(string fakeUserId)
    {
        return new Board(fakeUserId)
        {
            Id = new ObjectId().ToString()
        };
    }
}