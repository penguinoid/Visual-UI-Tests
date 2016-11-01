using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Firefox;
using Penguinoid.UI.Test.Generator.Factory;

namespace Penguinoid.UI.Test.Generator.SeleniumOperations
{
    public class Default : ISeleniumOperations
    {
        public void DoOperations(FirefoxDriver driver, Dictionary<string, string> options)
        {
            //do nothing
        }
    }
}
