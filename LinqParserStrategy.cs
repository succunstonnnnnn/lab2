using System.Xml.Linq;

namespace AppParsers
{

    public class LinqParserStrategy : IXmlParserStrategy
    {
        public List<Scientist> Parse(string filePath)
        {
            var results = new List<Scientist>();
            var doc = XDocument.Load(filePath);
            var nodes = doc.Descendants("Scientist");

            foreach (var node in nodes)
            {
                results.Add(new Scientist
                {
                    FullName = node.Attribute("FullName")?.Value,
                    Faculty = node.Attribute("Faculty")?.Value,
                    Department = node.Attribute("Department")?.Value,
                    Degree = node.Attribute("Degree")?.Value,
                    Rank = node.Attribute("Rank")?.Value,
                    RankDate = DateTime.Parse(node.Attribute("RankDate")?.Value ?? DateTime.MinValue.ToString())
                });
            }

            return results;
        }
    }
}