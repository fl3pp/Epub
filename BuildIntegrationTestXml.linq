<Query Kind="Statements">
  <Reference Relative="..\..\..\BuildIntegrationTestXml.linq">C:\dev\JFlepp.JFlepp.Epub\binaries\bin\JFlepp.JFlepp.Epub\JFlepp.JFlepp.Epub.dll</Reference>
  <Namespace>JFlepp.JFlepp.Epub</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

const string baseDirectory = @"C:\dev\JFlepp.JFlepp.Epub\JFlepp.JFlepp.Epub.Test.Integration\Books\";

foreach (var epub in new DirectoryInfo(baseDirectory).GetFiles("*.epub"))
{
	var xmlFilePath = baseDirectory + epub.Name.Replace(".epub", ".xml", ignoreCase: true, null);
	
	if (System.IO.File.Exists(xmlFilePath)) continue;
	
	using (var stream = epub.OpenRead())
	{
		var book = await EPubReader.ReadFromStream(stream);
		System.IO.File.WriteAllText(xmlFilePath, book.MetaToXml());
	}
}