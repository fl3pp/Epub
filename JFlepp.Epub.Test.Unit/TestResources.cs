using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JFlepp.Epub.Test.Unit
{
    public static class TestResources
    {
        public static string ContainerXmlSimple => GetStringResource(nameof(ContainerXmlSimple));
        public static string OpfManifestExample => GetStringResource(nameof(OpfManifestExample));
        public static string OpfManifestExtended => GetStringResource(nameof(OpfManifestExtended));
        public static string OpfMetadataExample1 => GetStringResource(nameof(OpfMetadataExample1));
        public static string OpfMetadataExample2 => GetStringResource(nameof(OpfMetadataExample2));
        public static string TocNcxExample1 => GetStringResource(nameof(TocNcxExample1));
        public static string TocNcxExample2 => GetStringResource(nameof(TocNcxExample2));
        public static string TocXHtmlExample1 => GetStringResource(nameof(TocXHtmlExample1));
        public static string TocXHtmlExample2 => GetStringResource(nameof(TocXHtmlExample2));

        private static string GetStringResource(string resourceName)
        {
            const string basePath = "JFlepp.Epub.Test.Unit.Resources.";
            using var stream = typeof(TestResources).Assembly.GetManifestResourceStream(basePath + resourceName);
            using var streamReader = new StreamReader(stream);
            return streamReader.ReadToEnd();
        } 
    }
}
