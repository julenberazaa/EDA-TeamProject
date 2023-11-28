using Common;

namespace TreePaths
{
    internal class Program
    {
        static void Main(string[] args)
        {
            StatsUpdater statsUpdater = new StatsUpdater();
            statsUpdater.CheckGitUsername();

            if (statsUpdater.GitUsername == null)
            {
                Console.WriteLine("Error getting Git info. You need to add this project to Git (fork from my repo and then clone your fork)");
                return;
            }

            if (!Tests.RunBasicTests())
                return;

            bool success = statsUpdater.AddResult(null);

            Console.WriteLine("\nAll tests passed and checkpoint saved on server. Speed is not measured in this assignment");
        }
    }
}
