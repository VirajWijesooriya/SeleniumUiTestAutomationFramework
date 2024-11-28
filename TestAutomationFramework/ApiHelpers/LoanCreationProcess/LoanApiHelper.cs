using RestSharp;
using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestAutomationFramework.Util.FileHelpers;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using TestAutomationFramework.ApiHelpers.ApiHelpers.Models;

namespace TestAutomationFramework.ApiHelpers
{
    public static class LoanApiHelper
    {
        public static LoanSubmitResult CreateLoan(RestClient client, string loanId, string token)
        {
            var request = new RestRequest($"api/AutomationTest/CopyExistingLoan/{loanId}", Method.Get);

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", $"Bearer {token}");

            var result = client.ExecuteAsync(request).GetAwaiter().GetResult();

            if (result.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception($"Create loan API endpoint failed with the status code {result.StatusCode}");
            }
            var loanSubmitResult = ApiHelper.DeserializeContent<LoanSubmitResult>(result.Content);

            return loanSubmitResult;
        }

        public static string CopyExistingLoan(RestClient client, string loanId, string token)
        {
            LoanSubmitResult loanSubmitResult = null;
            string loanID;

            try
            {
                loanSubmitResult = CreateLoan(client, loanId, token);
            }
            catch (Exception)
            {
                throw new Exception($"Loan submission failed with the error message {loanSubmitResult.Messages[0]}");
            }

            if (loanSubmitResult.Success == true)
            {
                loanID = loanSubmitResult.Messages[0];
            }
            else
            {
                throw new Exception($"Loan submission failed. Error message {loanSubmitResult.Messages[0]}");
            }

            return loanID;
        }
    }
}
