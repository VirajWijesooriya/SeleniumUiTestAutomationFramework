using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAutomationFramework.Framework.DataAccess.Models
{
    class BrokerCompanyEntity
    {
        public string Com_LegalName { get; set; }
        public string Com_TradingName { get; set; }
        public string Com_ABN { get; set; }
        public double Com_BrokerFirmFee { get; set; }
    }
}
