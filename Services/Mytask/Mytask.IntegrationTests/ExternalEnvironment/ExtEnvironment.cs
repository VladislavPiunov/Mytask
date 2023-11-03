using Mytask.IntegrationTests.ExternalEnvironment.Containers;

namespace Mytask.IntegrationTests.ExternalEnvironment;

/// <summary>
/// Класс для работы с внешними зависимостями
/// </summary>
public static class ExtEnvironment
{
    /// <summary>
    /// Config server контейнер
    /// </summary>
    public static ConfigServerContainer ConfigServerContainer { get; set; }

    /// <summary>
    /// MongoDb контейнер
    /// </summary>
    public static MongoDbContainer MongoDbContainer { get; set; }    
    
    /// <summary>
    /// MountebankContainer контейнер
    /// </summary>
    public static MountebankContainer MountebankContainer { get; set; }

    /// <summary>
    /// Тестовый сервер приложения
    /// </summary>
    public static TestServer TestServer { get; set;}
    
    /// <summary>
    /// Клиент для маунтбанка
    /// </summary>
    public static MountebankClient MountebankClient { get; private set; } = new(new Uri("http://localhost:8484"));

    /// <summary>
    /// ctor
    /// </summary>
    public static async Task Start()
    {
        var dockerClient = new DockerClientConfiguration(new Uri(DockerApiUri())).CreateClient();
        
        // Создаем зависимости
        ConfigServerContainer = new ConfigServerContainer(dockerClient);
        MongoDbContainer = new MongoDbContainer(dockerClient);
        MountebankContainer = new MountebankContainer(dockerClient);
        
        // Готовим окружение - удаляем лишнее и запускаем нужные контейнеры
        await RemoveAllContainers(dockerClient);
        await Task.WhenAll(ConfigServerContainer.StartContainer(), MongoDbContainer.StartContainer(), MountebankContainer.StartContainer());

        // Стартуем сервер с приложением
        TestServer = CreateServer();
    }

    /// <summary>
    /// Старт сервера с приложением
    /// </summary>
    private static TestServer CreateServer()
    {
        // Конфигурируем наше тестовое прилоежние через переменные окружения
        Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Development");
        Environment.SetEnvironmentVariable("ASPNETCORE_URLS", "https://+:443;http://+:80");
        Environment.SetEnvironmentVariable("ASPNETCORE_Kestrel__Certificates__Default__Password", "Pass@*****");
        Environment.SetEnvironmentVariable("ASPNETCORE_Kestrel__Certificates__Default__Path", "/https/mytask.pfx");

        return new WebApplicationFactory<Program>().Server;
    }

    /// <summary>
    /// Получение Url API Docker
    /// </summary>
    /// <returns>Url</returns>
    /// <exception cref="Exception">Ошибка если не удалось определить ОС</exception>
    private static string DockerApiUri()
    {
        var isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
        if (isWindows)
        {
            return "npipe://./pipe/docker_engine";
        }

        var isLinux = RuntimeInformation.IsOSPlatform(OSPlatform.Linux);
        if (isLinux)
        {
            return "tcp://127.0.0.1:2375";
        }
            
        var isOsx = RuntimeInformation.IsOSPlatform(OSPlatform.OSX);
        if (isOsx)
        {
            return "unix:///var/run/docker.sock";
        }

        throw new Exception("Was unable to determine what OS this is running on");
    }
    
    /// <summary>
    /// Остановить и удалить все контейнеры
    /// </summary>
    private static async Task RemoveAllContainers(DockerClient dockerClient)
    {
        IList<ContainerListResponse> containers = await dockerClient.Containers.ListContainersAsync(new ContainersListParameters());
        foreach (var container in containers)
        {
            await dockerClient.Containers.KillContainerAsync(container.ID, new ContainerKillParameters());
            await dockerClient.Containers.RemoveContainerAsync(container.ID, new ContainerRemoveParameters());
        }
    }
}