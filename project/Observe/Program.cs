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
            Console.WriteLine($"+ เพิ่ม {observer.GetType().Name} เข้าสู่ระบบแจ้งเตือน");
        }

        public void Unsubscribe(Observer observer)
        {
            observers.Remove(observer);
            Console.WriteLine($"- ถอด {observer.GetType().Name} ออกจากระบบแจ้งเตือน");
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
        public void Update(string queueNumber)
        {
            Console.WriteLine($" [จอทีวี] อัปเดตตัวเลขหน้าจอเป็นคิว: {queueNumber}");
        }
    }

    public class AudioAnnouncer : Observer
    {
        public void Update(string queueNumber)
        {
            Console.WriteLine($" [ลำโพง] ประกาศ: 'ขอเชิญคิว {queueNumber} รับยาที่ช่อง 3 ' ");
        }
    }

    public class LineNotifyApp : Observer
    {
        public void Update(string queueNumber)
        {
            Console.WriteLine($" [Line] เด้งข้อความเข้ามือถือ: 'ถึงคิวของคุณแล้ว ({queueNumber})'");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            PharmacyQueue queueSystem = new PharmacyQueue();

            DisplayBoard display = new DisplayBoard();
            AudioAnnouncer audio = new AudioAnnouncer();
            LineNotifyApp lineApp = new LineNotifyApp();

            Console.WriteLine("--- ลิสต์ระบบที่ลงทะเบียน ---");
            queueSystem.Subscribe(display);
            queueSystem.Subscribe(audio);
            queueSystem.Subscribe(lineApp);

            queueSystem.CallQueue("A012");


            Console.WriteLine("\n---  ลำโพงขัดข้อง  ---");
            queueSystem.Unsubscribe(audio);

            queueSystem.CallQueue("A013");

            Console.ReadLine();
        }
    }
}