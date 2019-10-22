using System.Linq;
using System.Xml.Linq;

namespace JFlepp.Epub.Processing
{
    internal abstract class NavigationOrderProcessor 
    { 
        public abstract int GetOrder(XElement element); 

        public static NavigationOrderProcessor Create(XElement? navMap = null)
        {
            if (navMap == null) return new CountingOrderNavigationProcessor();
            return navMap.Elements()
                .First(e => e.Name.Equals(XmlNamespaces.Ncx + NcxXmlNames.NavPointElementName))
                .Attributes().Any(a => a.Name.Equals((XName)NcxXmlNames.PlayOrderAttributeName))
                ? (NavigationOrderProcessor)new PlayOrderOrderNavigationProcessor()
                : new CountingOrderNavigationProcessor();
        }
    }

    internal sealed class CountingOrderNavigationProcessor : NavigationOrderProcessor
    {
        private int counter = 0;

        public override int GetOrder(XElement element)
        {
            counter++;
            return counter;
        }
    }

    internal sealed class PlayOrderOrderNavigationProcessor : NavigationOrderProcessor
    {
        public override int GetOrder(XElement element)
        {
            return int.Parse(
                element.Attribute((XName)NcxXmlNames.PlayOrderAttributeName).Value);
        }
    }
}
