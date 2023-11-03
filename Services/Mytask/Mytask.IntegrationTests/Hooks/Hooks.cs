using Mytask.IntegrationTests.ExternalEnvironment;

namespace Mytask.IntegrationTests.Hooks;

[assembly: CollectionBehavior(DisableTestParallelization = true)]

[Binding]
public static class ScenarioBeforeAndAfter
{
    /// <summary>
    /// Перед запуском тестов поднимем все необходимое окружение в докере
    /// </summary>
    [BeforeTestRun]
    public static async Task BeforeTestRun()
    {
        await ExtEnvironment.Start();
    }

    /// <summary>
    /// Перед каждым тестом почистим состояние
    /// </summary>
    [BeforeScenario]
    public static async Task BeforeScenario()
    {
        await ExtEnvironment.MongoDbContainer.DeleteAllData();
        await ExtEnvironment.MountebankClient.DeleteImposterAsync(4501);

        Common.ClearState();
    }
}

