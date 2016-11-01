using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium.Firefox;
using Penguinoid.UI.Test.Generator.Factory;

namespace Penguinoid.UI.Test.Generator.SeleniumOperations
{
    public class WithDelay : ISeleniumOperations
    {
        public void DoOperations(FirefoxDriver driver, Dictionary<string, string> options)
        {
            var delay = int.Parse(options["delay"]);
            Thread.Sleep(delay); //masonry js can take a little while to rearrange elements. this delay should avoid false positives from taking a screenshot before it's settled.
        }
    }
}
