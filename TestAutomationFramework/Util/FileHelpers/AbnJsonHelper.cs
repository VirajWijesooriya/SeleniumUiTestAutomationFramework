using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TestAutomationFramework.Util.Enums.Enums;

namespace TestAutomationFramework.Util.FileHelpers
{
    class AbnJsonHelper
    {
        public static string GetAbnForClientCompany(CompanyType companyType, string companyId)
        {
            string abn;
            JObject allAbnDetails;

            // Read the Abn  file and find the required company catergory in file data
            try
            {
                allAbnDetails = JsonFileHelper.GetFileData("ClientAbns.json");
            }
            catch (Exception)
            {
                throw new ApplicationException($"Couldn't find the file AbnJsonHelper.json in the output folder.");
            }

            var companyTypeString = GetCompanyType(companyType);
            JArray companies = (JArray)allAbnDetails[companyTypeString];
            if (companies == null)
            {
                throw new ApplicationException($"Couldn't find the given company type {companyTypeString} in the ClientABns.json file");
            }

            var companyIdType = GetCompanyIdType(companyType);
            JObject company = JsonFileHelper.GetEntityFromFileData(companies, companyIdType, companyId);

            if (company != null)
            {
                abn = (string)company["Abn"];
            }
            else
            {
                throw new ApplicationException($"Couldn't find the given company id {companyId} under the company type {companyTypeString} in the ClientABns.json file");
            }

            return abn;
        }


        private static string GetCompanyIdType(CompanyType t)
        {
            string companyIdType;
            switch (t)
            {
                case CompanyType.COM_TYPE_COMPANY:
                    companyIdType = "CompanyId";
                    break;
                case CompanyType.COM_TYPE_SOLE_TRADER:
                    companyIdType = "SoleTraderId";
                    break;
                case CompanyType.COM_TYPE_PARTNERSHIP:
                    companyIdType = "PartnershipId";
                    break;
                case CompanyType.COM_TYPE_TRUST:
                    companyIdType = "TrustId";
                    break;
                case CompanyType.COM_AYPE_ASSO:
                    companyIdType = "AssociationId";
                    break;
                default:
                    companyIdType = "CompanyId";
                    break;
            }
            return companyIdType;
        }

        private static string GetCompanyType(CompanyType t)
        {
            string companyIdType;
            switch (t)
            {
                case CompanyType.COM_TYPE_COMPANY:
                    companyIdType = "Company";
                    break;
                case CompanyType.COM_TYPE_SOLE_TRADER:
                    companyIdType = "SoleTrader";
                    break;
                case CompanyType.COM_TYPE_PARTNERSHIP:
                    companyIdType = "Partnership";
                    break;
                case CompanyType.COM_TYPE_TRUST:
                    companyIdType = "Trust";
                    break;
                case CompanyType.COM_AYPE_ASSO:
                    companyIdType = "Association";
                    break;
                default:
                    companyIdType = "Company";
                    break;
            }
            return companyIdType;
        }
    }
}
