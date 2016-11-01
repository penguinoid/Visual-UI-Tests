using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Penguinoid.UI.Test.Generator.Structure;

namespace Penguinoid.UI.Test.Generator
{
    public interface IConfig
    {
        string BaselineScreenShotFolderPath { get; set; }
        string ComparisonScreenShotFolderPath { get; set; }
        string DiffImageFolderPath { get; set; }
        string ReportName { get; set; }
        string SiteUrl { get; set; }
        List<NamedScreenshot> NamedScreenshots { get; set; }
        List<ScreenSize> ScreenSizes { get; set; }
    }
}
