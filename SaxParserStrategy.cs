using System.Xml;

public class SaxParserStrategy : IXmlParserStrategy
{
    public List<Scientist> Parse(string filePath)
    {
        var scientists = new List<Scientist>();
        using var reader = XmlReader.Create(filePath);
        while (reader.Read())
        {
            if (reader.NodeType == XmlNodeType.Element && reader.Name == "Scientist")
            {
                var scientist = new Scientist
                {
                    FullName = reader.GetAttribute("FullName"),
                    Faculty = reader.GetAttribute("Faculty"),
                    Department = reader.GetAttribute("Department"),
                    Degree = reader.GetAttribute("Degree"),
                    Rank = reader.GetAttribute("Rank"),
                    RankDate = DateTime.Parse(reader.GetAttribute("RankDate"))
                };
                scientists.Add(scientist);
            }
        }
        return scientists;
    }
}