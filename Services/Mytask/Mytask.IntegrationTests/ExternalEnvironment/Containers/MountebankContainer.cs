namespace Mytask.IntegrationTests.ExternalEnvironment.Containers;

/// <summary>
/// Mountebank container
/// </summary>
public class MountebankContainer : BaseContainer
{
    // Список портов для импостеров
    private readonly List<int> _imposterPosts = new() { 8484 };
        
    /// <summary>
    /// Конструктор
    /// </summary>
    public MountebankContainer(DockerClient dockerClient) : base("andyrbell/mountebank", "latest", dockerClient)
    { }

    /// <inheritdoc />
    public override async Task StartContainer()
    {
        await PullImage(Image, Tag);

        var exposedPorts = new Dictionary<string, EmptyStruct>
        {
            {
                "2525", default(EmptyStruct)
            }
        };

        var portBindings = new Dictionary<string, IList<PortBinding>>
        {
            {"2525", new List<PortBinding> {new() {HostPort = "2525"}}}
        };

        // выставим порты для импостеров
        foreach (int port in _imposterPosts)
        {
            exposedPorts.Add(port.ToString(), default);
            portBindings.Add(port.ToString(), new List<PortBinding> {new() {HostPort = port.ToString()}});
        }

        var container = await DockerClient.Containers.CreateContainerAsync(new CreateContainerParameters
        {
            Image = ImageFull,
            ExposedPorts = exposedPorts,
            HostConfig = new HostConfig
            {
                PortBindings = portBindings,
                PublishAllPorts = true
            },
        });
            
        ContainerId = container.ID;
        await DockerClient.Containers.StartContainerAsync(ContainerId, null);
        await WaitContainer();
    }

    /// <inheritdoc />
    protected override async Task WaitContainer()
    {
        for (int i = 0; i < 30; i++)
        {
            try
            {
                using var client = new HttpClient();
                var response = await client.GetAsync("http://localhost:2525");
                if (response.IsSuccessStatusCode)
                {
                    return;
                }
            }
            catch (Exception)
            {
                //ignore
            }
        }
            
        await Task.Delay(TimeSpan.FromSeconds(1));
    }
}