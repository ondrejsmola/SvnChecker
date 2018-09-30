using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace SvnChecker
{
    public class InfoParser
    {
        public IEnumerable<RevisionInfo> Parse(string data)
        {
            var result = new List<RevisionInfo>();

            var splitData = data.Split('\r', '\n').ToList();

            var regex = new Regex(@"r(\d+)\s\|\s(\S+)\s\|\s(.+)\s\+.*");
            foreach (var line in splitData)
            {
                var match = regex.Match(line);
                if (match.Success)
                {
                    var revisionInfo = new RevisionInfo()
                    {
                        Revision = Int32.Parse(match.Groups[1].Value),
                        User = match.Groups[2].Value,
                        DateTime = ParseDateTime(match.Groups[3].Value)
                    };
                    result.Add(revisionInfo);
                }
            }

            return result;
        }

        private DateTime ParseDateTime(string data)
        {
            return DateTime.Parse(data);
        }
    }
}
