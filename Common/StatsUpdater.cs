using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;

namespace Common
{
    public class StatsUpdater
    {
        public string GitUsername { get; private set; }

        public void CheckGitUsername()
        {
            GitUsername = GitHelper.GitUsername();
        }

        public bool AddResult(SpeedMeasure speedMeasure)
        {
            string projectName = Process.GetCurrentProcess().ProcessName;
            string machineName = Environment.MachineName;

            double time = speedMeasure != null ? speedMeasure.Time : 0;

            if (time != 0)
                Console.WriteLine($"Congratulations!!! You completed the assignment {projectName} :)\nYour time is {speedMeasure.Time}s");
            else
                Console.WriteLine($"Congratulations!!! You reached the checkpoint in the assignment {projectName} :)");

            HttpClient client = new HttpClient();

            string targetUrl = $"https://xerkariak.com/challenges/add-result.php?Username={GitUsername}&Project={projectName}&Time={time.ToString().Replace(",", ".")}&Subject=EDA&Machine={machineName.Replace(" ", "")}";
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, targetUrl);

            HttpResponseMessage response = client.SendAsync(request).Result;

            return response.IsSuccessStatusCode;
        }

    }
}
