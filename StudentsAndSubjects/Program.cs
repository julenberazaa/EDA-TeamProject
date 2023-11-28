using Common;

namespace StudentsAndSubjects
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

            Console.WriteLine("\nAll tests passed and checkpoint saved on server. Measuring speed...");
            SpeedMeasure speedMeasure = Tests.MeasureSpeed();

            if (!speedMeasure.Success)
            {
                Console.WriteLine("An error was detected during the speed measurement. Probably something wrong with Add/Get");
                return;
            }
            if (speedMeasure.Time > 30)
            {
                Console.WriteLine($"Sorry, your algorithm took too long: {speedMeasure.Time}s");
                return;
            }
            Console.WriteLine($"OK. Your time is : {speedMeasure.Time}s");

            success = statsUpdater.AddResult(speedMeasure);
            if (success)
                Console.WriteLine("Your time was saved on the server");
            else
                Console.WriteLine("Something failed saving your time on the server");
        }
    }
}
