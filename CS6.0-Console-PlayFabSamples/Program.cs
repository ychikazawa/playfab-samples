using System;
using System.Threading;
using System.Threading.Tasks;
using PlayFab;
using PlayFab.ClientModels;
using PlayFab.CloudScriptModels;
using EntityKey = PlayFab.CloudScriptModels.EntityKey;
using PlayFab.ServerModels;
using System.Text.Json;
using PlayFab.AdminModels;
using Microsoft.Extensions.Configuration;

namespace CS6._0_Console_PlayFabSamples
{
    public static class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json");
            var configuration = builder.Build();

            PlayFabSettings.staticSettings.TitleId = configuration["TitleID"]; // Please change this value to your own titleId from PlayFab Game Manager
            PlayFabSettings.staticSettings.DeveloperSecretKey = configuration["DevSecretKey"];

            Console.WriteLine($"TitleId: {PlayFabSettings.staticSettings.TitleId}");
            Console.WriteLine($"DeveloperSecretKey: {PlayFabSettings.staticSettings.DeveloperSecretKey}");

            FunctionsTest functionsTest = new FunctionsTest("test user");

            functionsTest.CallHelloWorld();
            functionsTest.CallGetDataReportUrl();

            Console.ReadKey();
        }
    }
}