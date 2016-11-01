using System.Collections.Generic;
using System.Configuration;

namespace Penguinoid.UI.Test.Generator.Configuration
{
    public class UiTestsScreenSizes : ConfigurationSection
    {
        [ConfigurationProperty("screenSizes")]
        public ScreenSizesCollection ScreenSizes
        {
            get { return (ScreenSizesCollection)base["screenSizes"]; }
            set { base["screenSizes"] = value; }
        }
    }

    public class ScreenSizesCollection : ConfigurationElementCollection
    {
        private readonly List<ScreenSize> _screenSizes;

        public ScreenSizesCollection()
        {
            _screenSizes = new List<ScreenSize>();
        }

        public override ConfigurationElementCollectionType CollectionType
        {
            get { return ConfigurationElementCollectionType.BasicMap; }
        }

        protected override string ElementName
        {
            get { return "screenSize"; }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            var screenSize = new ScreenSize();
            _screenSizes.Add(screenSize);
            return screenSize;
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ScreenSize)element).Name;
        }

        public new IEnumerator<ScreenSize> GetEnumerator()
        {
            return _screenSizes.GetEnumerator();
        }
    }

    public class ScreenSize : ConfigurationElement
    {
        [ConfigurationProperty("name")]
        public string Name
        {
            get { return base["name"] as string; }
            set { base["name"] = value; }
        }

        [ConfigurationProperty("width")]
        public string Width
        {
            get { return base["width"] as string; }
            set { base["width"] = value; }
        }

        [ConfigurationProperty("height")]
        public string Height
        {
            get { return base["height"] as string; }
            set { base["height"] = value; }
        }
    }
}
