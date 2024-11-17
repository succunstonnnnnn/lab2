using System.Collections.ObjectModel;
using System.Xml;
using System.Xml.Xsl;

public class XmlToHtmlTransformer
{
    public void TransformFilteredResults(ObservableCollection<Scientist> filteredScientists, string xslFile, string outputFile)
    {
        // Create a temporary XML document to hold filtered results
        XmlDocument tempXmlDoc = new XmlDocument();
        XmlElement root = tempXmlDoc.CreateElement("Scientists");

        foreach (var scientist in filteredScientists)
        {
            XmlElement scientistElem = tempXmlDoc.CreateElement("Scientist");
            scientistElem.SetAttribute("FullName", scientist.FullName);
            scientistElem.SetAttribute("Faculty", scientist.Faculty);
            scientistElem.SetAttribute("Department", scientist.Department);
            scientistElem.SetAttribute("Degree", scientist.Degree);
            scientistElem.SetAttribute("Rank", scientist.Rank);
            scientistElem.SetAttribute("RankDate", scientist.RankDate.ToString("yyyy-MM-dd"));

            root.AppendChild(scientistElem);
        }

        tempXmlDoc.AppendChild(root);

        // Perform XSLT transformation on the temporary document
        var transform = new XslCompiledTransform();
        transform.Load(xslFile);
        using (XmlWriter writer = XmlWriter.Create(outputFile))
        {
            transform.Transform(tempXmlDoc, writer);
        }
    }
}