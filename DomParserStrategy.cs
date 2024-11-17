using System.Xml;

public class DomParserStrategy : IXmlParserStrategy
{
    public List<Scientist> Parse(string filePath)
    {
        var results = new List<Scientist>();
        var doc = new XmlDocument();
        doc.Load(filePath);
        var nodes = doc.GetElementsByTagName("Scientist");

        foreach (XmlNode node in nodes)
        {
            results.Add(new Scientist
            {
                FullName = node.Attributes["FullName"].Value,
                Faculty = node.Attributes["Faculty"].Value,
                Department = node.Attributes["Department"].Value,
                Degree = node.Attributes["Degree"].Value,
                Rank = node.Attributes["Rank"].Value,
                RankDate = DateTime.Parse(node.Attributes["RankDate"].Value)
            });
        }
        return results;
    }
}