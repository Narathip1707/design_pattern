using System;
using System.Data;
namespace MoodWorld
{
    class MoodManager
    {
         // Singleton instance
         public static MoodManager instance;
         // แอทริบิว
         public MoodType currentMood;
         public int moodLevel;
         public string moodMessage;
         public DateTime lastUpdated;

         private MoodManager()
        {
            currentMood = MoodType.Relax;
            moodLevel = 50;
            moodMessage = "ระบบชิวชิว";
            lastUpdated = DateTime.Now;
        }

        public static MoodManager GetInstance()
        {
            if (instance == null)
            {
                instance = new MoodManager();
            }
                return instance;
        }

        public void SetMood(MoodType mood)
        {
            currentMood = mood;
            lastUpdated = DateTime.Now;
        }
        public void SetMoodLevel(int level)
        {
            moodLevel = level;
            lastUpdated = DateTime.Now;
        }
        public void SetMoodMessage(string message)
        {
            moodMessage = message;
            lastUpdated = DateTime.Now;
        }
        
        public MoodType GetMood()
        {
            return currentMood;
        }
        public int GetMoodLevel()
        {
            return moodLevel;
        }
        public string GetMoodMessage()
        {
            return moodMessage;
        }
        public DateTime GetLastUpdated()
        {
            return lastUpdated;
        }
        public void ShowMoodStatus()
        {
            Console.WriteLine("== World Mood Status ==");
            Console.WriteLine($"Mood : {currentMood}");
            Console.WriteLine($"Level : {moodLevel}");
            Console.WriteLine($"Message : {moodMessage}");
            Console.WriteLine($"Updated at : {lastUpdated}");
        }
    }
}