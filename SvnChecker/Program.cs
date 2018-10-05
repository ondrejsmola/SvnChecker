namespace SvnChecker
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var ignoreUser = args.Length > 0 ? args[0] : "";
            TaskRunner.Run(ignoreUser);
        }
    }
}
