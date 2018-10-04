using SvnChecker.Configuration;
using System.Threading;
using System.Threading.Tasks;

namespace SvnChecker
{
    public class CheckingTask
    {
        public async Task RunAsync(ConfigurationItem configuration, string ignoreUser, CancellationToken cancellationToken)
        {
            await Task.Run(() =>
            {
                var lastRevision = configuration.LastRevision;
                while (!cancellationToken.IsCancellationRequested)
                {
                    var checker = new Checker();
                    var revisionInfo = checker.CheckRevision(configuration.Path);
                    if (revisionInfo is null) continue;
                    if ((revisionInfo.Revision > lastRevision) && (revisionInfo.User != ignoreUser))
                    {
                        var toast = new Toast();
                        toast.Show(revisionInfo, configuration.Caption);
                        lastRevision = revisionInfo.Revision;
                        Configuration.Configuration.UpdateRevision(configuration.Path, lastRevision);
                    }
                    Thread.Sleep(1000 * configuration.PollingInterval);
                }
            },
            cancellationToken);
        }
    }
}
