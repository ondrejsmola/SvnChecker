using System.Diagnostics;
using System.Linq;

namespace SvnChecker
{
    public class Checker
    {
        public RevisionInfo CheckRevision(string directory)
        {
            var log = RunSvnLog(directory);
            var parser = new InfoParser();
            var revisionInfos = parser.Parse(log);
            var sortedRevisions = revisionInfos.OrderByDescending(x => x.DateTime);
            return sortedRevisions.FirstOrDefault();
        }

        private string RunSvnLog(string directory)
        {
            Process p = new Process();
            // Redirect the output stream of the child process.
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.FileName = "svn";
            p.StartInfo.Arguments = "log -r BASE:HEAD -q";
            p.StartInfo.WorkingDirectory = directory;
            p.Start();
            // Do not wait for the child process to exit before
            // reading to the end of its redirected stream.
            // p.WaitForExit();
            // Read the output stream first and then wait.
            string output = p.StandardOutput.ReadToEnd();
            p.WaitForExit();

            return output;
        }
    }
}
