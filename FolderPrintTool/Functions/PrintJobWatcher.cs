using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Threading;

namespace ForderPrintTool.Function
{
    internal class PrintJobWatcher
    {
        public List<string> GetCurrentJobNames()
        {
            var jobNames = new List<string>();
            var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PrintJob");

            foreach (ManagementObject job in searcher.Get())
            {
                string name = job["Name"]?.ToString();
                if (!string.IsNullOrEmpty(name))
                    jobNames.Add(name);
            }

            return jobNames;
        }

        public bool WaitForNewPrintJob(int timeoutMilliseconds, List<string> beforeSnapshot, int pollInterval = 1000)
        {
            int waited = 0;
            while (waited < timeoutMilliseconds)
            {
                Thread.Sleep(pollInterval);
                waited += pollInterval;

                var after = GetCurrentJobNames();
                var newJobs = after.Except(beforeSnapshot).ToList();

                if (newJobs.Count > 0)
                    return true;
            }
            return false;
        }
    }
}
