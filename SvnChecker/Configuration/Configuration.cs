using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace SvnChecker.Configuration
{
    [XmlRoot("Configuration")]
    public class Configuration : List<ConfigurationItem>
    {

        public static Configuration LoadFromFile(string fileName)
        {
            var deserializer = new XmlSerializer(typeof(Configuration));
            var reader = new StreamReader(fileName);
            var items = (Configuration)(deserializer.Deserialize(reader));
            reader.Close();
            return items;
        }

        public void SaveToFile(string fileName)
        {
            var serializer = new XmlSerializer(typeof(Configuration));
            using (var writer = new StreamWriter(fileName))
            {
                serializer.Serialize(writer, this);
            }

        }
    }
}
