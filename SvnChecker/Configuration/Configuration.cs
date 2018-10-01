using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace SvnChecker.Configuration
{
    [XmlRoot("Configuration")]
    public class Configuration : List<ConfigurationItem>
    {
        private static object _lock = new object();
        public static string GetFileName()
        {
            var exeName = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
            return Path.ChangeExtension(exeName, "xml");
        }

        public static Configuration LoadFromFile()
        {
            return LoadFromFile(GetFileName());
        }

        public static Configuration LoadFromFile(string fileName)
        {
            var deserializer = new XmlSerializer(typeof(Configuration));
            var reader = new StreamReader(fileName);
            var items = (Configuration)(deserializer.Deserialize(reader));
            reader.Close();
            return items;
        }

        public void SaveToFile()
        {
            SaveToFile(GetFileName());
        }

        public void SaveToFile(string fileName)
        {
            var serializer = new XmlSerializer(typeof(Configuration));
            using (var writer = new StreamWriter(fileName))
            {
                serializer.Serialize(writer, this);
            }

        }

        public static void UpdateRevision(string path, int newRevision)
        {
            lock (_lock)
            {
                var configuration = LoadFromFile();
                var configurationItem = configuration.FirstOrDefault(item => item.Path == path);
                if (!(configurationItem is null))
                {
                    configurationItem.LastRevision = newRevision;
                    configuration.SaveToFile();
                }
            }
        }
    }
}
