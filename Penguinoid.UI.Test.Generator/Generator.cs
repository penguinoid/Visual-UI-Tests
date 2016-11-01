using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using ImageMagick;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using Penguinoid.UI.Test.Generator.Structure;
using Screenshot = Penguinoid.UI.Test.Generator.Configuration.Screenshot;

namespace Penguinoid.UI.Test.Generator
{
    public class Generator
    {
        public void GenerateBaselineScreenshots(IConfig config)
        {
            GenerateScreenshots(config, config.BaselineScreenShotFolderPath);
        }

        public void GenerateComparisonScreenshots(IConfig config)
        {
            GenerateScreenshots(config, config.ComparisonScreenShotFolderPath);
        }

        private void GenerateScreenshots(IConfig config, string screenshotFolderPath)
        {
            foreach (var namedScreenshot in config.NamedScreenshots)
            {
                Screenshot(config, namedScreenshot, screenshotFolderPath);
            }
        }

        private void Screenshot(IConfig config, NamedScreenshot namedScreenshot, string filepath)
        {
            foreach (var screenSize in config.ScreenSizes)
            {
                Viewport(namedScreenshot, filepath, screenSize, config.SiteUrl);
            }
        }

        public void Viewport(NamedScreenshot namedScreenshot, string filepath, ScreenSize screenSize, string siteUrl)
        {
            var driver = new FirefoxDriver();
            driver.Manage().Window.Size = new Size(screenSize.Width, screenSize.Height);
            driver.Navigate().GoToUrl(string.Concat(siteUrl, namedScreenshot.Url));
            var seleniumOperationsFactory = new Factory.SeleniumOperationsFactory();
            var seleniumOperations = seleniumOperationsFactory.SeleniumOperations(namedScreenshot.Type);
            seleniumOperations.DoOperations(driver, namedScreenshot.Options);
            var screenshot = ((ITakesScreenshot)driver).GetScreenshot();
            screenshot.SaveAsFile(string.Concat(filepath, GetFilename(namedScreenshot, screenSize)), System.Drawing.Imaging.ImageFormat.Png);
            driver.Dispose();
        }

        public string GetFilename(NamedScreenshot namedScreenshot, ScreenSize screenSize)
        {
            return string.Format("{0}-{1}.png", namedScreenshot.Name, screenSize.Name);
        }

        public IEnumerable<Viewport> GetViewPorts(IConfig config)
        {
            return from namedScreenshot in config.NamedScreenshots from screenSize in config.ScreenSizes select new Viewport
            {
                NamedScreenshot = namedScreenshot,
                ScreenSize = screenSize
            };
        }

        public double CompareImage(IConfig config, Viewport viewport)
        {
            var filename = GetFilename(viewport.NamedScreenshot, viewport.ScreenSize);
            var baseline = string.Concat(config.BaselineScreenShotFolderPath, filename);
            var comparison = string.Concat(config.ComparisonScreenShotFolderPath, filename);
            using (MagickImage baselineImage = new MagickImage(baseline))
            using (MagickImage comparisonImage = new MagickImage(comparison))
            using (MagickImage diffImage = new MagickImage())
            {
                var result = baselineImage.Compare(comparisonImage, ErrorMetric.Absolute, diffImage);
                diffImage.Write(string.Concat(config.DiffImageFolderPath, filename));
                return result;
            }
        }
    }
}
