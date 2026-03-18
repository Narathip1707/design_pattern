using System;
using System.Collections.Generic;

namespace PharmacyQueueObserver
{
    public interface Observer
    {
        void Update(string queueNumber);
    }

    //Publisher
    public class PharmacyQueue
    {
        private List<Observer> observers = new List<Observer>();
        private string currentQueue = "";

        public void Subscribe(Observer observer)
        {
            observers.Add(observer);
            Console.WriteLine($"+ เพิ่ม {observer.ToString()}  เข้าสู่ระบบแจ้งเตือน");
        }

        public void Unsubscribe(Observer observer)
        {
            observers.Remove(observer);
            Console.WriteLine($"- ถอด {observer.ToString()} ออกจากระบบแจ้งเตือน");
        }

        public void Notify()
        {
            Console.WriteLine($"\nระบบห้องยา กำลังส่งแจ้งเตือนคิวที่: {currentQueue}...");

            foreach (Observer observer in observers)
            {
                observer.Update(currentQueue);
            }
        }

        public void CallQueue(string queueNumber)
        {
            this.currentQueue = queueNumber;
            this.Notify();
        }
    }


    public class DisplayBoard : Observer
    {
        private string location;

        public DisplayBoard(string location)
        {
            this.location = location;
        }
        public void Update(string queueNumber)
        {
            Console.WriteLine($" [จอทีวี:{location}] อัปเดตตัวเลขหน้าจอเป็นคิว: {queueNumber}");
        }
        public override string ToString()
        {
            return $"DisplayBoard({location})";
        }
    }

    public class AudioAnnouncer : Observer
    {
        private string location;
        public AudioAnnouncer(string location)
        {
            this.location = location;
        }
        public void Update(string queueNumber)
        {
            Console.WriteLine($" [ลำโพง:{location}] ประกาศ: 'ขอเชิญคิว {queueNumber} รับยาที่ช่อง 3 ' ");
        }
        public override string ToString()
        {
            return $"AudioAnnouncer({location})";
        }
    }

    public class LineNotifyApp : Observer
    {
        private string lineId;

        public LineNotifyApp(string lineId)
        {
            this.lineId = lineId;
        }
        public void Update(string queueNumber)
        {
            Console.WriteLine($" [Line:{lineId}] เด้งข้อความเข้ามือถือ: 'ถึงคิวของคุณแล้ว ({queueNumber})'");
        }
        public override string ToString()
        {
            return $"LineNotifyApp({lineId})";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            PharmacyQueue queueSystem = new PharmacyQueue();

            DisplayBoard display = new DisplayBoard("ห้องยา");
            DisplayBoard display2 = new DisplayBoard("รอบๆห้องยา");
            AudioAnnouncer audio = new AudioAnnouncer("ห้องยา");
            LineNotifyApp lineApp = new LineNotifyApp("narathip");

            Console.WriteLine("--- ลิสต์ระบบที่ลงทะเบียน ---");
            queueSystem.Subscribe(display);
            queueSystem.Subscribe(display2);
            queueSystem.Subscribe(audio);
            queueSystem.Subscribe(lineApp);

            queueSystem.CallQueue("A012");

            Console.WriteLine("\nลำโพงขัดข้อง");
            queueSystem.Unsubscribe(audio);

            queueSystem.CallQueue("A013");

            Console.ReadLine();
        }
    }
}