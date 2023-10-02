namespace Mytask.UnitTests;

public class BoardControllerTest
{
    private readonly Mock<IBoardRepository> _boardRepositoryMock;
    private readonly Mock<IIdentityService> _identityServiceMock;

    public BoardControllerTest()
    {
        _boardRepositoryMock = new Mock<IBoardRepository>();
        _identityServiceMock = new Mock<IIdentityService>();
    }

    [Test]
    public async Task Get_boards_async_success()
    {
        var fakeUserId = "1";
        var fakeBoardList = new List<Board> { GetBoardFake(fakeUserId) };

        _identityServiceMock.Setup(x => x.GetUserIdentity()).Returns(fakeUserId);
        _boardRepositoryMock.Setup(x => x.GetBoardsAsync(It.IsAny<string>())).Returns(Task.FromResult(fakeBoardList));

        var boardController = new BoardController(
            _boardRepositoryMock.Object,
            _identityServiceMock.Object
        );

        var actionResult = await boardController.GetBoardsAsync();
        
        Assert.AreEqual((actionResult.Result as OkObjectResult).StatusCode, (int)System.Net.HttpStatusCode.OK);
        Assert.AreEqual((((ObjectResult)actionResult.Result).Value as List<Board>), fakeBoardList);
    }

    [Test]
    public async Task Post_board_async_success()
    {
        var fakeUserId = "1";
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
    
    private Board GetBoardFake(string fakeUserId)
    {
        return new Board(fakeUserId)
        {
            Id = new ObjectId().ToString()
        };
    }
}