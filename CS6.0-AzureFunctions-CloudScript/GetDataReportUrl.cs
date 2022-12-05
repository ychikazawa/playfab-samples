using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PlayFab;
using System.Xml.Linq;

namespace CS6._0_AzureFunctions_CloudScript
{
    public static class GetDataReportUrl
    {
        [FunctionName("GetDataReportUrl")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Start GetDataReportUrl.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);

            log.LogInformation(requestBody);

            string reportName = data.FunctionArgument.ReportName;
            int year = data.FunctionArgument.Year;
            int month = data.FunctionArgument.Month;
            int day = data.FunctionArgument.Day;

            log.LogInformation($"reportName: {reportName}, year: {year}, month: {month}, day: {day}");

            // This API needs Developer Secret Key.
            PlayFabSettings.staticSettings.TitleId = Environment.GetEnvironmentVariable("PLAYFAB_TITLE_ID", EnvironmentVariableTarget.Process);
            PlayFabSettings.staticSettings.DeveloperSecretKey = Environment.GetEnvironmentVariable("PLAYFAB_DEV_SECRET_KEY", EnvironmentVariableTarget.Process);

            var result = PlayFabAdminAPI.GetDataReportAsync(new PlayFab.AdminModels.GetDataReportRequest()
            {
                ReportName = reportName,
                Year = year,
                Month = month,
                Day = day
            }).Result.Result;

            return new OkObjectResult(result.DownloadUrl);
        }
    }
}
