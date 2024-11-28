using Dapper;
using TestAutomationFramework.Framework.DataAccess.Models;
using TestAutomationFramework.Framework.TestInit;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;

namespace TestAutomationFramework.Framework.DataAccess
{
    class DbInterface
    {
       public static string GetLoanIDForLoan(string loanNumber, TestBrowser browser)
        {
            string query = $"SELECT Loan_ID FROM Loan WHERE Loan_Number = @LoanNumber";

            var parameters = new DynamicParameters();
            parameters.Add("@LoanNumber", loanNumber);

            SqlConnection connection = new SqlConnection(browser.ConnectionString);

            var loanID = connection.Query<string>(query, parameters).FirstOrDefault();
            return loanID;
        }

        public static BrokerCompanyEntity GetBrokerCompanyDetails(string legalName, TestBrowser testBrowser)
        {
            string query = $"SELECT TOP 10 * from Company c " +
                $"JOIN Code_Types ct on c.Com_SubTypeID = ct.Ct_ID and Ct_Category = 'Company' and Ct_Code = 'Broker' " +
                $"WHERE c.Com_LegalName = @legalname";

            var parameters = new DynamicParameters();
            parameters.Add("@legalname", legalName);


            SqlConnection connection = new SqlConnection(testBrowser.ConnectionString);

            BrokerCompanyEntity values = connection.Query<BrokerCompanyEntity>(query, parameters).FirstOrDefault();
            return values;
        }
    }
}
