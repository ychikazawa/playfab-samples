using PlayFab.CloudScriptModels;
using PlayFab;
using PlayFab.ClientModels;
using EntityKey = PlayFab.CloudScriptModels.EntityKey;

namespace CS6._0_Console_PlayFabSamples
{
    internal class FunctionsTest
    {
        LoginResult login;
        
        public FunctionsTest(string customID = "test user")
        {
            /*
             * LoginWithCustomID
             */
            login = PlayFabClientAPI.LoginWithCustomIDAsync(new LoginWithCustomIDRequest
            {
                TitleId = PlayFabSettings.staticSettings.TitleId,
                CreateAccount = false,
                CustomId = customID
            }).Result.Result;
        }

        
        public void CallHelloWorld(string name = "ychikazawa")
        {
            var param = new Dictionary<string, object>();

            param["name"] = name;
            var res = PlayFabCloudScriptAPI.ExecuteFunctionAsync(
                new ExecuteFunctionRequest()
                {
                    AuthenticationContext = login.AuthenticationContext,
                    Entity = new EntityKey
                    {
                        Id = login.EntityToken.Entity.Id,
                        Type = login.EntityToken.Entity.Type
                    },
                    FunctionName = "HelloWorld",
                    FunctionParameter = param
                }
            );
            
            try
            {
                Console.WriteLine($"Message: {res.Result.Result.FunctionResult}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Raise Error: {e}");
            }
        }

        public void CallGetDataReportUrl()
        {
            var param = new Dictionary<string, object>()
            {
                { "ReportName", "Rolling Thirty Day Overview Report" },
                { "Year", 2022 },
                { "Month", 11 },
                { "Day", 25 }
            };

            var res = PlayFabCloudScriptAPI.ExecuteFunctionAsync(
                new ExecuteFunctionRequest()
                {
                    AuthenticationContext = login.AuthenticationContext,
                    Entity = new EntityKey
                    {
                        Id = login.EntityToken.Entity.Id,
                        Type = login.EntityToken.Entity.Type
                    },
                    FunctionName = "GetDataReportUrl",
                    FunctionParameter = param
                }
            );

            Console.WriteLine(res.Result.Result.FunctionResult);
        }
    }
}
