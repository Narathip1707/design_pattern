using System;

namespace MoodWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            //A
            MoodManager worldA = MoodManager.GetInstance();
            worldA.ShowMoodStatus();
            Console.WriteLine();

            worldA.SetMood(MoodType.Happy);
            worldA.SetMoodLevel(80);
            worldA.SetMoodMessage("โลกแฮปปี้");
            worldA.ShowMoodStatus();

            //B
            MoodManager world2 = MoodManager.GetInstance();
            Console.WriteLine("After update from another client:");
            world2.ShowMoodStatus();

            Console.ReadLine();
        }
    }
}
