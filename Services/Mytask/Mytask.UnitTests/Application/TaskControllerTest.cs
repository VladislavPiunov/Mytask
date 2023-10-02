namespace Mytask.UnitTests;

public class TaskControllerTest
{
    private readonly Mock<ITaskRepository> _taskRepositoryMock;

    public TaskControllerTest()
    {
        _taskRepositoryMock = new Mock<ITaskRepository>();
    }

    [Test]
    public async Task Get_tasks_async_success()
    {
        var fakeUserId = "1";
        var fakeBoard = GetBoardFake(fakeUserId);

        var fakeTaskList = new List<Model.Task>
        {
            new Model.Task("test1", fakeBoard.Id, fakeBoard.Stages[0]) { Description = "test" },
            new Model.Task("test3", fakeBoard.Id, fakeBoard.Stages[1]) { Description = "test" },
            new Model.Task("test2", fakeBoard.Id, fakeBoard.Stages[2]) { Description = "test" }
        };

        _taskRepositoryMock.Setup(x => x.GetTasksAsync(It.IsAny<string>()))
            .Returns(Task.FromResult(fakeTaskList));

        var taskController = new TaskController(_taskRepositoryMock.Object);

        var actionResult = await taskController.GetTasksAsync(fakeBoard.Id);

        Assert.AreEqual((actionResult.Result as OkObjectResult).StatusCode, (int)System.Net.HttpStatusCode.OK);
        Assert.AreEqual((((ObjectResult)actionResult.Result).Value as List<Model.Task>), fakeTaskList);
    }

    [Test]
    public async Task Post_board_async_success()
    {
        var fakeTask = new Model.Task("test", ObjectId.GenerateNewId().ToString(), ObjectId.GenerateNewId().ToString())
        {
            Description = "test description",
            Deadline = DateTime.Now,
            Executor = "test user"
        };

        _taskRepositoryMock.Setup(x => x.CreateTaskAsync(It.IsAny<Model.Task>())).Returns(Task.FromResult(fakeTask));

        var taskController = new TaskController(_taskRepositoryMock.Object);

        var actionResult = await taskController.CreateTaskAsync(fakeTask);

        Assert.AreEqual((actionResult.Result as OkObjectResult).StatusCode, (int)System.Net.HttpStatusCode.OK);
        Assert.AreEqual((((ObjectResult)actionResult.Result).Value as Model.Task), fakeTask);
    }


    private Board GetBoardFake(string fakeUserId)
    {
        return new Board(fakeUserId)
        {
            Id = ObjectId.GenerateNewId().ToString(),
            Stages =
            {
                ObjectId.GenerateNewId().ToString(),
                ObjectId.GenerateNewId().ToString(),
                ObjectId.GenerateNewId().ToString()
            }
        };
    }
}