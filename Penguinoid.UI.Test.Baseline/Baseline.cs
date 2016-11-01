using System.Configuration;
using System.Runtime.Remoting.Channels;
using NUnit.Framework;
using Penguinoid.UI.Test.Generator;
using Penguinoid.UI.Test.Generator.Configuration;

namespace Penguinoid.UI.Test.Baseline
{
    [TestFixture]
    public class Baseline
    {
        private readonly Generator.Generator _generator = new Generator.Generator();
        private readonly IConfig _config = new Config();

        [Test]
        public void Generate()
        {
            _generator.GenerateBaselineScreenshots(_config);
            Assert.AreEqual(true, true);
        }
    }
}
