using System.Collections.Generic;
using System.IO;
using System.Text;
using NUnit.Framework;
using Penguinoid.UI.Test.Generator;
using Penguinoid.UI.Test.Generator.Structure;

namespace Penguinoid.UI.Test.Runner
{
    [TestFixture]
    public class Runner
    {
        private readonly Generator.Generator _generator = new Generator.Generator();
        private readonly IConfig _config = new Config();
        private readonly List<Viewport> _failedTests = new List<Viewport>();

        [TestFixtureSetUp]
        public void Generate()
        {
            _generator.GenerateComparisonScreenshots(_config);
        }

        [Test, TestCaseSource("GetViewPorts")]
        public void Compare(Viewport viewport)
        {
            var result = _generator.CompareImage(_config, viewport);
            if (!result.Equals(0))
                _failedTests.Add(viewport);
            Assert.AreEqual(0, result);
        }

        [TestFixtureTearDown]
        public void Report()
        {
            var results = new StringBuilder();
            results.Append(ReportSummary);
            foreach (var test in _failedTests)
            {
                var filename = _generator.GetFilename(test.NamedScreenshot, test.ScreenSize);
                var screenshotName = ScreenshotName(test.NamedScreenshot.Name, test.ScreenSize.Name);
                results.AppendFormat("<h2>{0}</h2>", screenshotName);
                results.AppendFormat("<a href=\"/regression/baseline/{0}\" target=\"_blank\">[baseline]</a><a href=\"/regression/comparison/{0}\" target=\"_blank\">[latest]</a>", filename);
                results.AppendFormat("<img src=\"{0}\" title=\"{1}\">", filename, screenshotName);
            }
            var report = ReportTemplate.Replace("_RESULTS_", results.ToString());
            File.WriteAllText(string.Concat(_config.DiffImageFolderPath, _config.ReportName), report);
        }

        private string ScreenshotName(string screenshotName, string screenSizeName)
        {
            return string.Format("{0} - {1}", screenshotName, screenSizeName);
        }

        private string ReportSummary
        {
            get
            {
                return _failedTests.Count == 0
                    ? "<p>All tests passed.</p>"
                    : string.Format("<p>{0} test{1} failed</p>", _failedTests.Count, _failedTests.Count > 1 ? "s" : string.Empty);
            }
        }

        private IEnumerable<Viewport> GetViewPorts()
        {
            return _generator.GetViewPorts(_config);
        }

        private string ReportTemplate
        {
            get
            {
                return @"<!DOCTYPE html>
                            <html lang=""en"" xmlns=""http://www.w3.org/1999/xhtml"">
                            <head>
                                <meta charset=""utf-8"" />
                                <title>Regression Results</title>
                            </head>
                            <body>
                                <h1>Regression Results</h1>
                                _RESULTS_
                            </body>
                            </html>";
            }
        }
    }
}
