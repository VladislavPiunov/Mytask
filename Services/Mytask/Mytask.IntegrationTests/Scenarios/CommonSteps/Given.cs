using MbDotNet.Models.Imposters;
using MbDotNet.Models.Stubs;
using MbDotNet.Models;
using Mytask.IntegrationTests.ExternalEnvironment;
using Mytask.IntegrationTests.Helpers.JwtAuthorisation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Mytask.IntegrationTests.Scenarios.CommonSteps
{
    internal class GivenCommonStepsDefinitions
    {
        [Given("кейклоак работает и настроен")]
        public async Task KeyCloakIsWorking()
        {
            // Удалим импостер, если он уже есть
            await ExtEnvironment.MountebankClient.DeleteImposterAsync(8484);

            // Создадим импостер кейклоака
            await ExtEnvironment.MountebankClient.CreateHttpImposterAsync(new HttpImposter(8484, "keyCloak",
                new HttpImposterOptions()
                {
                    // Разрешим cors, чтобы можно было удобно получить токен из UI сваггера
                    AllowCORS = true
                }));

            // Мокаем запрос на получение настроек для проверки токена
            await ExtEnvironment.MountebankClient.AddHttpImposterStubAsync(8484,
                new HttpStub()
                    .OnPathAndMethodEqual($"/auth/realms/my_realm/.well-known/openid-configuration", Method.Get)
                    .ReturnsJson(HttpStatusCode.OK, KeyCloakResponseGenerator.GetOpenidConfiguration("my_realm")), 0);

            // Мокаем запрос на получение сертификата для проверки токена
            await ExtEnvironment.MountebankClient.AddHttpImposterStubAsync(8484,
                new HttpStub()
                    .OnPathAndMethodEqual($"/auth/realms/my_realm/protocol/openid-connect/certs", Method.Get)
                    .ReturnsJson(HttpStatusCode.OK, KeyCloakResponseGenerator.GetCertificates()), 1);

            // Мокаем запрос на получение токена 
            await ExtEnvironment.MountebankClient.AddHttpImposterStubAsync(8484,
                new HttpStub()
                    .OnPathAndMethodEqual($"/auth/realms/my_realm/protocol/openid-connect/token", Method.Post)
                    .ReturnsJson(HttpStatusCode.OK, KeyCloakResponseGenerator.GetToken("myrealm", new Dictionary<string, string>())));
        }

    }
}
