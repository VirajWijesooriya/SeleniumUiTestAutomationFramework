using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAutomationFramework.ApiHelpers.ApiHelpers.Models
{
    public class LoanSubmitResult
    {
        public bool Success { get; set; }
        public List<string> Messages { get; set; }

    }
}
