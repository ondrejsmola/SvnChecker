using System.Windows.Forms;

namespace SvnChecker
{
    public class Toast
    {
        public void Show(RevisionInfo revisionInfo, string repositoryName)
        {
            using (var notifyIcon = new NotifyIcon())
            {
                notifyIcon.Visible = true;
                notifyIcon.Icon = System.Drawing.SystemIcons.Information;
                notifyIcon.BalloonTipTitle = $"SVN repository {repositoryName} updated";
                notifyIcon.BalloonTipText = $"{revisionInfo.User} - {revisionInfo.Revision} - {revisionInfo.DateTime}\n{revisionInfo.LogMessage}";

                notifyIcon.ShowBalloonTip(5000);

                //Thread.Sleep(10000);
            }
        }
    }
}
