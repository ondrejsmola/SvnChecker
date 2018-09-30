using SvnChecker.Configuration;

namespace SvnChecker
{
    public static class Program
    {
        public static int Main()
        {
            var config = new Configuration.Configuration();

            config.Add(new ConfigurationItem()
            { Caption = "Item 1", LastRevision = 3232, Path = "Path 1" });
            config.Add(new ConfigurationItem()
            { Caption = "Item 2", LastRevision = 444, Path = "Path 2" });

            config.SaveToFile(@"D:\TMP\SvnChecker.config");

            var checker = new Checker();
            return checker.CheckRevision(@"d:\Source\BCS\Support\").Revision;
        }
    }
}
