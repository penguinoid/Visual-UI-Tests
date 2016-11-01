using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Penguinoid.UI.Test.Generator.Configuration
{
    public class UiTestsConfig : ConfigurationSection
    {
        [ConfigurationProperty("baselineScreenShotFolderPath")]
        public string BaselineScreenShotFolderPath
        {
            get { return base["baselineScreenShotFolderPath"] as string; }
            set { base["baselineScreenShotFolderPath"] = value; }
        }

        [ConfigurationProperty("comparisonScreenShotFolderPath")]
        public string ComparisonScreenShotFolderPath
        {
            get { return base["comparisonScreenShotFolderPath"] as string; }
            set { base["comparisonScreenShotFolderPath"] = value; }
        }

        [ConfigurationProperty("diffImageFolderPath")]
        public string DiffImageFolderPath
        {
            get { return base["diffImageFolderPath"] as string; }
            set { base["diffImageFolderPath"] = value; }
        }

        [ConfigurationProperty("reportName")]
        public string ReportName
        {
            get { return base["reportName"] as string; }
            set { base["reportName"] = value; }
        }

        [ConfigurationProperty("siteUrl")]
        public string SiteUrl
        {
            get { return base["siteUrl"] as string; }
            set { base["siteUrl"] = value; }
        }
    }
}
