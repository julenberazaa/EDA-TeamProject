using System;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Common
{
    public class GitHelper
    {
        public static string GitUsername()
        {
            try
            {
                string output = null, errors = null;

                Process process = new Process()
                {
                    StartInfo = new ProcessStartInfo()
                    {
                        FileName = "git",
                        Arguments = "remote get-url origin",
                        RedirectStandardError = true,
                        RedirectStandardOutput = true
                    }
                };
                process.Start();

                process.WaitForExit();

                output = process.StandardOutput.ReadToEnd();
                errors = process.StandardOutput.ReadToEnd();

                if (!string.IsNullOrEmpty(errors))
                {
                    Console.WriteLine(errors);
                    return null;
                }
                Match match = Regex.Match(output, "https://github.com/([^/]+)/");
                if (match.Success)
                    return match.Groups[1].Value;

                Console.WriteLine($"Failed to parse Github user from remote repository url: {output}");
                return null;
            }
            catch { }

            return null;
        }
    }
}
