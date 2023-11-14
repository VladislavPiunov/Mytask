using MongoDB.Bson;
using MongoDB.Driver.Core.Operations;

namespace Mytask.IntegrationTests.ExternalEnvironment.Containers;

/// <summary>
/// MongoDB container
/// </summary>
internal class MongoDbContainer : BaseContainer
{
    /// <summary>
    /// Конструктор
    /// </summary>
    public MongoDbContainer(DockerClient dockerClient) : base("mongodb/mongodb-community-server", "latest", dockerClient)
    {
    }

    /// <inheritdoc />
    public override async Task StartContainer()
    {
        await PullImage(Image,Tag);
        
        var exposedPorts = new Dictionary<string, EmptyStruct>
        {
            {
                "27017", default(EmptyStruct)
            }
        };

        var portBindings = new Dictionary<string, IList<PortBinding>>
        {
            {"27017", new List<PortBinding> {new() {HostPort = "27017" } }}
        };
        
        var env = new List<string>
        {
            "MONGODB_INITDB_ROOT_USERNAME=user",
            "MONGODB_INITDB_ROOT_PASSWORD=pass",
            "MONGODB_INITDB_DATABASE=mytask"
        };
            
        var container = await DockerClient.Containers.CreateContainerAsync(new CreateContainerParameters
        {
            Image = ImageFull,
            Env = env,
            ExposedPorts = exposedPorts,
            HostConfig = new HostConfig
            {
                PortBindings = portBindings,
                PublishAllPorts = true
            }
        });
        
        ContainerId = container.ID;
        await DockerClient.Containers.StartContainerAsync(ContainerId, null);
        await WaitContainer();
    }

    /// <inheritdoc />
    protected override async Task WaitContainer()
    {
        for (var i = 0; i < 30; i++)
        {
            try
            {
                var client = new MongoClient("mongodb://user:pass@localhost:27017/");
                var database = client.GetDatabase("mytask");
                bool isMongoLive = database.RunCommandAsync((Command<BsonDocument>)"{ping:1}").Wait(1000);
                if (isMongoLive)
                {
                    return;
                }
            }
            catch
            {
                //ignore
            }
            
            await Task.Delay(TimeSpan.FromSeconds(1));
        }
    }

    /// <summary>
    /// Удалить данные из БД
    /// </summary>
    public async Task DeleteAllData()
    {
        var client = new MongoClient("mongodb://user:pass@localhost:27017/");
        var database = client.GetDatabase("mytask");
        await database.DropCollectionAsync("boards");
        await database.DropCollectionAsync("stages");
        await database.DropCollectionAsync("tasks");
    }
}