using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAutomationFramework.Util.FileHelpers
{
    class LoanDataHelper
    {
        protected EnvironmentLoans environmentLoans;

        public EnvironmentLoans GetLoanDataForEnvironment(string targetEnv)
        {
            environmentLoans = new EnvironmentLoans();

            // Read the environement details file and find the required environment in file data
            JObject envDetails = JsonFileHelper.GetFileData("LoanStaticData.json");
            JArray environments = (JArray)envDetails["environments"];
            JObject environmentLoanData = JsonFileHelper.GetEntityFromFileData(environments, "environmentName", targetEnv);

            if (environmentLoanData != null)
            {
                // PPSR Loans
                environmentLoans.ppsrCFALoan = (string)environmentLoanData["ppsrLoanCFA"];
            }
            else
            {
                throw new Exception($"Given Environement {targetEnv} could not be found in the LoanStaticData.json file.");
            }
            return environmentLoans;
        }
    }

    public class EnvironmentLoans
    {
        // PPSR Loans
        public string ppsrCFALoan { get; set; }
    }
}
