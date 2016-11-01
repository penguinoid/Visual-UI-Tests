using System.Collections.Generic;

namespace Penguinoid.UI.Test.Generator.Structure
{
    public class NamedScreenshot
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string Type { get; set; }
        public Dictionary<string, string> Options { get; set; } 
    }
}
