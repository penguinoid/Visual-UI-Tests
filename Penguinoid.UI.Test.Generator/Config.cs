using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Penguinoid.UI.Test.Generator.Configuration;
using Penguinoid.UI.Test.Generator.Structure;
using ScreenSize = Penguinoid.UI.Test.Generator.Structure.ScreenSize;

namespace Penguinoid.UI.Test.Generator
{
    public class Config : IConfig
    {
        public string BaselineScreenShotFolderPath { get; set; }
        public string ComparisonScreenShotFolderPath { get; set; }
        public string DiffImageFolderPath { get; set; }
        public string ReportName { get; set; }
        public string SiteUrl { get; set; }
        public List<NamedScreenshot> NamedScreenshots { get; set; }
        public List<ScreenSize> ScreenSizes { get; set; }

        public Config()
        {
            var index = System.AppDomain.CurrentDomain.BaseDirectory.IndexOf("\\Penguinoid.UI.Test\\", StringComparison.InvariantCultureIgnoreCase);
            var basePath = System.AppDomain.CurrentDomain.BaseDirectory.Substring(0, index);
            string filePath = string.Concat(basePath, "\\Penguinoid.UI.Test\\Penguinoid.UI.Test.Generator\\App.Config");
            var configMap = new ExeConfigurationFileMap { ExeConfigFilename = filePath };
            var config = ConfigurationManager.OpenMappedExeConfiguration(configMap, ConfigurationUserLevel.None);
            var uiTests = config.GetSectionGroup("uiTests");
            var uiTestsConfig = uiTests.Sections.Get("uiTestsConfig") as UiTestsConfig;
            var uiTestsScreenshots = uiTests.Sections.Get("uiTestsScreenshots") as UiTestsScreenshots;
            var uiTestsScreenSizes = uiTests.Sections.Get("uiTestsScreenSizes") as UiTestsScreenSizes;

            BaselineScreenShotFolderPath = uiTestsConfig.BaselineScreenShotFolderPath;
            ComparisonScreenShotFolderPath = uiTestsConfig.ComparisonScreenShotFolderPath;
            DiffImageFolderPath = uiTestsConfig.DiffImageFolderPath;
            ReportName = uiTestsConfig.ReportName;
            SiteUrl = uiTestsConfig.SiteUrl;

            NamedScreenshots = new List<NamedScreenshot>();
            foreach (var screenshot in uiTestsScreenshots.Screenshots)
            {
                NamedScreenshots.Add(new NamedScreenshot
                {
                    Name = screenshot.Name,
                    Url = screenshot.Url,
                    Type = screenshot.Type,
                    Options = BuildOptions(screenshot.Options)
                });
            }

            ScreenSizes = new List<ScreenSize>();
            foreach (var screenSize in uiTestsScreenSizes.ScreenSizes)
            {
                ScreenSizes.Add(new ScreenSize
                {
                    Name = screenSize.Name,
                    Height = int.Parse(screenSize.Height),
                    Width = int.Parse(screenSize.Width)
                });
            }
        }

        private Dictionary<string, string> BuildOptions(string options)
        {
            if(string.IsNullOrWhiteSpace(options))
                return new Dictionary<string, string>();
            var optionPairs = options.Split('|');
            return optionPairs.Select(optionPair => optionPair.Split(';')).ToDictionary(splitPair => splitPair[0], splitPair => splitPair[1]);
        }
    }
}
