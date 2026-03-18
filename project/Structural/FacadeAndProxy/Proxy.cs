namespace FacadeNara
{
    // 1. สร้าง Interface 
    public interface ITradingService
    {
        void ExecuteTrade(string symbol, int quantity);
    }

    // 2. สร้างคลาส Proxy 
    public class TradingProxy : ITradingService
    {
        private TradingFacade realSystem;
        private string correctPin = "1234"; // รหัสผ่านที่ตั้งไว้

        public TradingProxy(TradingFacade realSystem)
        {
            this.realSystem = realSystem;
        }

        public void ExecuteTrade(string symbol, int quantity)
        {
            Console.Write($"\n[Security] ระบบกำลังดำเนินการซื้อ {symbol} , กรุณาใส่รหัส PIN: ");
            string inputPin = Console.ReadLine() ?? "";

            if (inputPin == correctPin)
            {
                Console.WriteLine("[Security] ยืนยันตัวตนสำเร็จ กำลังเชื่อมต่อระบบเทรด");
                realSystem.ExecuteTrade(symbol, quantity); // ส่งงานต่อให้ตัวจริง
            }
            else
            {
                Console.WriteLine("ปฏิเสธการเข้าถึง: รหัส PIN ไม่ถูกต้อง ");
            }
        }
    }
}