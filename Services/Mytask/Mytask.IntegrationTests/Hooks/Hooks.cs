using Mytask.IntegrationTests.ExternalEnvironment;
using Mytask.IntegrationTests.Scenarios;

[assembly: CollectionBehavior(DisableTestParallelization = true)]

namespace Mytask.IntegrationTests.Hooks;

[Binding]
internal static class ScenarioBeforeAndAfter
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
        await ExtEnvironment.MountebankClient.DeleteImposterAsync(8484);

        Common.ClearState();
    }
}

