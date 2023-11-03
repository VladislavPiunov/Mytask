namespace Mytask.IntegrationTests.ExternalEnvironment.Containers;

/// <summary>
///  Базовый контейнер для работы с Docker Api
/// </summary>
internal abstract class BaseContainer
{
    protected readonly DockerClient DockerClient;
    protected string? ContainerId;
        
    protected readonly string Image;
    protected readonly string Tag;

    protected string ImageFull => $"{Image}:{Tag}";
        
    protected BaseContainer(string image, string tag, DockerClient dockerClient)
    {
        Image = image;
        Tag = tag;
        DockerClient = dockerClient;
    }

    /// <summary>
    /// Скачать образ
    /// </summary>
    /// <param name="image">Image</param>
    /// <param name="tag">tag</param>
    protected async Task PullImage(string image,string tag)
    {
        await DockerClient.Images
            .CreateImageAsync(new ImagesCreateParameters
                {
                    FromImage = image,
                    Tag = tag
                },
                new AuthConfig(),
                new Progress<JSONMessage>());
    }

    /// <summary>
    /// Запустить контейнер
    /// </summary>
    public abstract Task StartContainer();
        
    /// <summary>
    /// Ожидание готовности контейнера
    /// </summary>
    protected abstract Task WaitContainer();
}