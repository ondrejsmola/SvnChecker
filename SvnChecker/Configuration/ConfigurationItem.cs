namespace SvnChecker.Configuration
{
    public class ConfigurationItem
    {
        public string Caption { get; set; }
        public string Path { get; set; }
        public int LastRevision { get; set; }
        public int PollingInterval { get; set; } = 60;
    }
}
