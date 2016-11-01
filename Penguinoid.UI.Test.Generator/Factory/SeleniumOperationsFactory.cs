using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Penguinoid.UI.Test.Generator.SeleniumOperations;

namespace Penguinoid.UI.Test.Generator.Factory
{
    public class SeleniumOperationsFactory
    {
        public ISeleniumOperations SeleniumOperations(string operationsTypeName)
        {
            switch (operationsTypeName)
            {
                case "WithDelay":
                    return new WithDelay();
                default:
                    return new Default();
            }
        }
    }
}
