using JFlepp.Epub.Processing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace JFlepp.Epub.Test.Unit.Processing
{

    [TestClass]
    public class NavigationOrderProcessorsTests
    {
        [TestMethod]
        public void Create_WithNavMapNull_ReturnsCountingOrderProcessor()
        {
            var result = NavigationOrderProcessor.Create(null);

            Assert.IsInstanceOfType(result, typeof(CountingOrderNavigationProcessor));
        }

        [TestMethod]
        public void Create_WithNavMapButWithoutPlayOrderAttributeOnPoints_CountingOrderNavigationProcessor()
        {
            var element = XElement.Parse($@"<{NcxXmlNames.NavMapElementName} xmlns=""{XmlNamespaces.Ncx}"">
                <{NcxXmlNames.NavPointElementName} />
            </{NcxXmlNames.NavMapElementName}>");

            var result = NavigationOrderProcessor.Create(element);

            Assert.IsInstanceOfType(result, typeof(CountingOrderNavigationProcessor));
        }

        [TestMethod]
        public void Create_WithNavMapAndPlayOrderOnPoints_ReturnsPlayOrderOrderNavigationProcessor()
        {
            var element = XElement.Parse($@"<{NcxXmlNames.NavMapElementName} xmlns=""{XmlNamespaces.Ncx}"">
                <{NcxXmlNames.NavPointElementName} {NcxXmlNames.PlayOrderAttributeName}=""0"" />
            </{NcxXmlNames.NavMapElementName}>");

            var result = NavigationOrderProcessor.Create(element);

            Assert.IsInstanceOfType(result, typeof(PlayOrderOrderNavigationProcessor));
        }
    }
}
