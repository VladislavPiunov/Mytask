namespace Mytask.UnitTests.Application;

public class TaskControllerTest
{
    private readonly Mock<ITaskRepository> _taskRepositoryMock;

    public TaskControllerTest()
    {
        _taskRepositoryMock = new Mock<ITaskRepository>();
    }

    public static IEnumerable<object[]> testValues()
    {
        yield return new object[]
        {
            "1",
            new Board("1")
            {
                Id = ObjectId.GenerateNewId().ToString(),
                Stages =
                {
                    ObjectId.GenerateNewId().ToString(),
                    ObjectId.GenerateNewId().ToString(),
                    ObjectId.GenerateNewId().ToString()
                }
            }
        };

        yield return new object[]
        {
            "2",
            new Board("2")
            {
                Id = ObjectId.GenerateNewId().ToString(),
                Stages =
                {
                    ObjectId.GenerateNewId().ToString(),
                    ObjectId.GenerateNewId().ToString(),
                    ObjectId.GenerateNewId().ToString()
                }
            }
        };
    }

    [TestCaseSource(nameof(testValues))]
    public async Task Get_tasks_async_success(string fakeUserId, Board fakeBoard)
    {
        var fakeTaskList = new List<Model.Task>
        {
            new Model.Task("test1", fakeBoard.Id, fakeBoard.Stages[0]) { Description = "test", Executor = fakeUserId },
            new Model.Task("test3", fakeBoard.Id, fakeBoard.Stages[1]) { Description = "test" },
            new Model.Task("test2", fakeBoard.Id, fakeBoard.Stages[2]) { Description = "test" }
        };

        _taskRepositoryMock.Setup(x => x.GetTasksAsync(It.IsAny<string>()))
            .Returns(Task.FromResult(fakeTaskList));

        var taskController = new TaskController(_taskRepositoryMock.Object);

        var actionResult = await taskController.GetTasksAsync(fakeBoard.Id);

        Assert.AreEqual((actionResult.Result as OkObjectResult).StatusCode, (int)System.Net.HttpStatusCode.OK);
        Assert.AreEqual((((ObjectResult)actionResult.Result).Value as List<Model.Task>), fakeTaskList);
        Assert.AreEqual((((ObjectResult)actionResult.Result).Value as List<Model.Task>).First().Executor, fakeUserId);
    }

    public static IEnumerable<Model.Task> testTasks()
    {
        yield return new Model.Task("test", ObjectId.GenerateNewId().ToString(), ObjectId.GenerateNewId().ToString())
        {
            Description = "test description",
            Deadline = DateTime.Now,
            Executor = "test user"
        };

        yield return new Model.Task("test2", ObjectId.GenerateNewId().ToString(), ObjectId.GenerateNewId().ToString())
        {
            Description = "test description2",
            Deadline = DateTime.Now,
            Executor = "test user2"
        };
    }

    [TestCaseSource(nameof(testTasks))]
    public async Task Post_task_async_success(Model.Task fakeTask)
    {
        _taskRepositoryMock.Setup(x => x.CreateTaskAsync(It.IsAny<Model.Task>())).Returns(Task.FromResult(fakeTask));

        var taskController = new TaskController(_taskRepositoryMock.Object);

        var actionResult = await taskController.CreateTaskAsync(fakeTask);

        Assert.AreEqual((actionResult.Result as OkObjectResult).StatusCode, (int)System.Net.HttpStatusCode.OK);
        Assert.AreEqual((((ObjectResult)actionResult.Result).Value as Model.Task), fakeTask);
    }

    [TestCaseSource(nameof(testTasks))]
    public async Task Put_task_async_success(Model.Task fakeTask)
    {
        _taskRepositoryMock.Setup(x => x.UpdateTaskAsync(It.IsAny<Model.Task>())).Returns(Task.FromResult(fakeTask));

        var taskController = new TaskController(_taskRepositoryMock.Object);

        var actionResult = await taskController.UpdateTaskAsync(fakeTask);

        Assert.AreEqual((actionResult.Result as OkObjectResult).StatusCode, (int)System.Net.HttpStatusCode.OK);
        Assert.AreEqual((((ObjectResult)actionResult.Result).Value as Model.Task), fakeTask);
    }

    public static IEnumerable<string> testTasksIds()
        => new List<string>{"1", "2"};

    [TestCaseSource(nameof(testTasksIds))]
    public async Task Delete_task_async_success(string fakeTaskId)
    {
        _taskRepositoryMock.Setup(x => x.DeleteTaskAsync(It.IsAny<string>())).Returns(Task.FromResult(true));

        var taskController = new TaskController(_taskRepositoryMock.Object);

        var actionResult = await taskController.DeleteTaskAsync(fakeTaskId);

        Assert.AreEqual((actionResult.Result as OkObjectResult).StatusCode, (int)System.Net.HttpStatusCode.OK);
        Assert.AreEqual(((ObjectResult)actionResult.Result).Value as bool?, true);
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