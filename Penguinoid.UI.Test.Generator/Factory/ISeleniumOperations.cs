using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Firefox;

namespace Penguinoid.UI.Test.Generator.Factory
{
    public interface ISeleniumOperations
    {
        void DoOperations(FirefoxDriver driver, Dictionary<string,string> options);
    }
}
