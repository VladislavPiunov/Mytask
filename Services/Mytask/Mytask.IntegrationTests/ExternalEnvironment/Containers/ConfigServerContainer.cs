namespace Mytask.IntegrationTests.ExternalEnvironment.Containers
{
    /// <summary>
    /// Config server container
    /// </summary>
    public class ConfigServerContainer : BaseContainer
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public ConfigServerContainer(DockerClient dockerClient) : base("steeltoeoss/config-server", "latest", dockerClient)
        { }

        /// <inheritdoc />
        public override async Task StartContainer()
        {
            await PullImage(Image, Tag);

            var exposedPorts = new Dictionary<string, EmptyStruct>
            {
                {
                    "8888", default(EmptyStruct)
                }
            };

            var portBindings = new Dictionary<string, IList<PortBinding>>
            {
                {"8888", new List<PortBinding> {new() {HostPort = "8888"}}}
            };

            var cmd = new List<string>
            {
                "--spring.cloud.config.server.git.uri=https://github.com/VladislavPiunov/my-config.git", 
                "--spring.cloud.config.server.git.default-label=main"
            } ;

            var container = await DockerClient.Containers.CreateContainerAsync(new CreateContainerParameters
            {
                Image = ImageFull,
                ExposedPorts = exposedPorts,
                Cmd = cmd,
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
                    var response = await client.GetAsync("http://localhost:8888");
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
}
