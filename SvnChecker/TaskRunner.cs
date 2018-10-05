using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SvnChecker
{
    public class TaskRunner
    {
        public static void Run(string ignoreUser)
        {
            var configuration = Configuration.Configuration.LoadFromFile(Configuration.Configuration.GetFileName());
            var tasks = new List<Task>();

            foreach (var configurationItem in configuration)
            {
                var checkingTask = new CheckingTask();
                tasks.Add(checkingTask.RunAsync(configurationItem, ignoreUser, CancellationToken.None));
            }

            Task.WaitAll(tasks.ToArray());
        }
    }
}
