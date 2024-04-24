using System.Text.RegularExpressions;
using System.Xml;

namespace CV5Text
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string regex = @"^[a-zA-Z0-9\.]+@[a-zA-Z0-9\.]+\.[a-z]{2,6}$";
            Regex loginRegex = new Regex(regex, RegexOptions.IgnoreCase | RegexOptions.Compiled);
            if (loginRegex.IsMatch("DUB0074@vsb.cz"))
            {
                Console.WriteLine("Je match");
            }
            else
            {
                Console.WriteLine("Není match");
            }
            string urlRegexString = @"^(https?):\\/\\/((([a-z]+)\\.)?[a-z]+\\.[a-z]{2,6})(\\/|\\?|$)";
            Regex urlRegex = new Regex(urlRegexString, RegexOptions.IgnoreCase | RegexOptions.Compiled);
            Match match = urlRegex.Match("https://katedrainformatiky.cz/cs/pro-uchazece/zamereni-studia");
            if(match.Success)
            {
                Console.WriteLine(match.Groups[1].Value);
            }
            else
            {
                Console.WriteLine("Chyba");
            }
            string attributeRegexString = @"\{([a-zA-Z]+)\}*";
            Regex attributeRegex = new Regex(attributeRegexString);
            string txt = "Ahoj {name}. Tvá objednávka „{orderName}“ v ceně {price} byla úspěšně uhrazena.";
            string finalString = attributeRegex.Replace(txt, (Match match) => {
                return match.Groups[1].Value.ToUpper();
            });
            Console.WriteLine(finalString);

            XmlDocument xDoc = new XmlDocument();
            xDoc.AppendChild(xDoc.CreateXmlDeclaration("1.0", "UTF-8", "yes"));
            XmlNode root = xDoc.CreateElement("koren");
            for (int i = 0; i < 4; i++)
            {
                XmlNode order = xDoc.CreateElement("objednavka");
                root.AppendChild(order);
                XmlNode customer = xDoc.CreateElement("zakaznik");
                order.AppendChild(customer);
                XmlNode textNode = xDoc.CreateTextNode("Jan Novak" + i);
                customer.AppendChild(textNode);

                XmlAttribute attr = xDoc.CreateAttribute("id");
                attr.Value = i.ToString();
                order.Attributes.Append(attr);
            }
            xDoc.AppendChild(root);

            Console.WriteLine(xDoc.DocumentElement.ChildNodes[0].ChildNodes[0].FirstChild.Value);
            xDoc.DocumentElement.ChildNodes[0].AppendChild(xDoc.DocumentElement.ChildNodes[2]);
            xDoc.DocumentElement.RemoveChild(xDoc.DocumentElement.ChildNodes[2]);

            xDoc.Save("test.xml");

            XmlDocument xDoc2 = new XmlDocument();
            xDoc2.Load("test.xml");
            xDoc2.Save("test2.xml");

            XmlDocument xmlClanky = new XmlDocument();
            xmlClanky.Load("clanky.xml");
            XmlNodeList list = xmlClanky.DocumentElement.ChildNodes[0].ChildNodes;
            foreach(XmlNode node in list)
            {
                if(node.Name == "item")
                {
                    Console.WriteLine(node.ChildNodes[0].ChildNodes[0].Value + " " + node.ChildNodes[4].ChildNodes[0].Value);
                }
            }
            XmlNodeList titles = xmlClanky.SelectNodes("/rss/channel/item/title/text()");//Dalo by se použít i //title/text(), to najde všechny titles, uplně kdekoliv, //item/title/text() najde všechny titles od všech items
            XmlNodeList pubTime = xmlClanky.SelectNodes("/rss/channel/item/pubDate/text()");
            string regexTimeString = @"([0-9]{2})\:([0-9]{2})";
            Regex regexTime = new Regex(regexTimeString);
            for (int i = 0;i < titles.Count;i++)
            {
                Match matcher = regexTime.Match(pubTime[i].Value);
                if(matcher.Success)
                {
                    Console.WriteLine("Sucess");
                }
                string hours = matcher.Groups[1].Value;
                string minutes = matcher.Groups[2].Value;
                Console.WriteLine(titles[i].Value + " " + hours + "h " + minutes + "m");
            }
            XmlNodeList items = xmlClanky.SelectNodes("//item");
            foreach(XmlNode node in items)
            {
                string tilte = node.SelectSingleNode("title/text()").Value;
            }
        }
    }
}
