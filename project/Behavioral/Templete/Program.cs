using System;

namespace FootballShirtStore
{

    public abstract class OrderFulfillment
    {

        public void ProcessOrder()
        {
            Console.WriteLine("------------------------------------------------");
            CheckStock();   //optional step 
            ProcessPayment(); //optional step
            CustomizeShirt(); //hook step
            PackProduct();  //abstract step
            ShipOrder();  //optional step
            Console.WriteLine("------------------------------------------------\n");
        }



        // Optional Step
        protected virtual void CheckStock()
        {
            Console.WriteLine("[สเตป 1 - CheckStock] : เช็กสต็อกเสื้อมาตรฐานจากหน้าร้าน");
        }

        // Optional Step
        protected virtual void ProcessPayment()
        {
            Console.WriteLine("[สเตป 2 - Payment]    : ตัดเงินผ่านระบบบัตรเครดิต/โอนเงินมาตรฐาน");
        }

        // Hook Step
        protected virtual void CustomizeShirt()
        {
    
        }

        // Abstract Step
        protected abstract void PackProduct();

        // Optional Step
        protected virtual void ShipOrder()
        {
            Console.WriteLine("[สเตป 5 - ShipOrder]  : ส่งไปรษณีย์ EMS มาตรฐาน");
        }
    }

    public class ReadyToWearOrder : OrderFulfillment
    {
        private bool isGiftWrap; 

        public ReadyToWearOrder(bool isGiftWrap)
        {
            this.isGiftWrap = isGiftWrap;
        }

        protected override void PackProduct()
        {
            if (isGiftWrap)
                Console.WriteLine("[สเตป 4 - PackProduct]: พับเสื้อใส่กล่องของขวัญผูกโบว์");
            else
                Console.WriteLine("[สเตป 4 - PackProduct]: พับเสื้อใส่ถุงซิปล็อกพลาสติก");
        }
    }


    public class CustomPrintOrder : OrderFulfillment
    {
        private string printName;   
        private int printNumber;

        public CustomPrintOrder(string printName, int printNumber)
        {
            this.printName = printName;
            this.printNumber = printNumber;
        }

        // ใช้ Hook
        protected override void CustomizeShirt()
        {
            Console.WriteLine($"[สเตป 3 - Customize]  : ส่งเสื้อไปสกรีนชื่อ '{printName}' และเบอร์ '{printNumber}'");
        }

        protected override void PackProduct()
        {
            Console.WriteLine("[สเตป 4 - PackProduct]: แพ็กใส่กล่องพรีเมียมกันยับ");
        }
    }

    public class TeamBulkOrder : OrderFulfillment
    {
        private string teamName;    
        private int totalQuantity;

        public TeamBulkOrder(string teamName, int totalQuantity)
        {
            this.teamName = teamName;
            this.totalQuantity = totalQuantity;
        }

        protected override void CheckStock()
        {
            Console.WriteLine($"[สเตป 1 - CheckStock] : เช็กสต็อกโกดังมีเสื้อครบจำนวน {totalQuantity} ตัวหรือไม่");
        }

        // ใช้ Hook
        protected override void CustomizeShirt()
        {
            Console.WriteLine($"[สเตป 3 - Customize]  : อัดโลโก้ทีม '{teamName}' ลงบนเสื้อทั้ง {totalQuantity} ตัว");
        }

        protected override void PackProduct()
        {
            Console.WriteLine($"[สเตป 4 - PackProduct]: พับเสื้อทั้งหมดเรียงลงลังกระดาษ");
        }

        protected override void ShipOrder()
        {
            Console.WriteLine("[สเตป 5 - ShipOrder]  : เรียกรถมารับลังสินค้าที่โกดัง");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("ออร์เดอร์ที่ 1: เสื้อเปล่า (ห่อของขวัญ)");
            OrderFulfillment order1 = new ReadyToWearOrder(isGiftWrap: true);
            order1.ProcessOrder(); 

            Console.WriteLine("ออร์เดอร์ที่ 2: เสื้อสกรีนชื่อ NARATHIP เบอร์ 10");
            OrderFulfillment order2 = new CustomPrintOrder("NARATHIP", 10);
            order2.ProcessOrder();

            Console.WriteLine("ออร์เดอร์ที่ 3: เสื้อทีมโรงเรียน 50 ตัว");
            OrderFulfillment order3 = new TeamBulkOrder("FC United", 50);
            order3.ProcessOrder();

            Console.ReadLine();
        }
    }
}