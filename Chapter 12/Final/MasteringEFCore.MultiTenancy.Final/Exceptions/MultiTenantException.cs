using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasteringEFCore.MultiTenancy.Final.Exceptions
{
    public class MultiTenantException : Exception
    {
        public string ErrorMessage { get; private set; }

        public MultiTenantException(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }
    }
}
