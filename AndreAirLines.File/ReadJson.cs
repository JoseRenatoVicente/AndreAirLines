using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace AndreAirLines.File
{
    public class ReadJson
    {
        public static List<Entity>? getData<Entity>(string pathFile)
        {
            if (System.IO.File.Exists(pathFile))
                return JsonConvert.DeserializeObject<List<Entity>>(
                    new StreamReader(pathFile).ReadToEnd());

            return null;
        }

        /*public static void Create<Entity>(IEnumerable<Entity> entities) where Entity : class
        {
            // Allocate the XDocument and add an XML declaration.  
            XDocument RejectedXmlList = new XDocument(new XDeclaration("1.0", "utf-8", null));

            // At this point RejectedXmlList.Root is still null, so add a unique root element.
            RejectedXmlList.Add(new XElement("Rejectedparameters"));

            // Add elements for each Parameter to the root element
            foreach (Entity Myparameter in entities)
            {
                if (true)
                {
                    XElement xelement = new XElement(Myparameter, CurrentData.ToString());
                    RejectedXmlList.Root.Add(xelement);
                }
            }
        }*/
    }
}
