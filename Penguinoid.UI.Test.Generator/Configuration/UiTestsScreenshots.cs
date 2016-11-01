using System.Collections.Generic;
using System.Configuration;

namespace Penguinoid.UI.Test.Generator.Configuration
{
    public class UiTestsScreenshots : ConfigurationSection
    {
        [ConfigurationProperty("screenshots")]
        public ScreenshotsCollection Screenshots
        {
            get { return (ScreenshotsCollection)base["screenshots"]; }
            set { base["screenshots"] = value; }
        }
    }

    public class ScreenshotsCollection : ConfigurationElementCollection
    {
        private readonly List<Screenshot> _screenshots;

        public ScreenshotsCollection()
        {
            _screenshots = new List<Screenshot>();
        }

        public override ConfigurationElementCollectionType CollectionType
        {
            get { return ConfigurationElementCollectionType.BasicMap; }
        }

        protected override string ElementName
        {
            get { return "screenshot"; }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            var screenshot = new Screenshot();
            _screenshots.Add(screenshot);
            return screenshot;
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((Screenshot) element).Name;
        }

        public new IEnumerator<Screenshot> GetEnumerator()
        {
            return _screenshots.GetEnumerator();
        }
    }

    public class Screenshot : ConfigurationElement
    {
        [ConfigurationProperty("name")]
        public string Name
        {
            get { return base["name"] as string; }
            set { base["name"] = value; }
        }

        [ConfigurationProperty("url")]
        public string Url
        {
            get { return base["url"] as string; }
            set { base["url"] = value; }
        }

        [ConfigurationProperty("type")]
        public string Type
        {
            get { return base["type"] as string; }
            set { base["type"] = value; }
        }

        [ConfigurationProperty("options")]
        public string Options
        {
            get { return base["options"] as string; }
            set { base["options"] = value; }
        }
    }
}
