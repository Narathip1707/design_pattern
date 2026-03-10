namespace FacadeNara
{
    public class TradingFacade : ITradingService
{
    // สร้างอ็อบเจกต์ของระบบย่อยไว้ภายใน
    private Wallet wallet;
    private MarketService market;
    private Portfolio portfolio;
    private NotificationService notification;



    public TradingFacade(Wallet w, MarketService m, Portfolio p, NotificationService n)
    {
        wallet = w;
        market = m;
        portfolio = p;
        notification = n;
    }

    // เมธอดเดียวที่รวบขั้นตอนยุ่งยากทั้งหมดไว้
    public void ExecuteTrade(string symbol, int quantity)
    {
        Console.WriteLine($"\n>>> กำลังเริ่มทำรายการ: {symbol} จำนวน {quantity} หน่วย <<<");

        //1.เช็กราคาจากตลาด
        double price = market.GetLivePrice(symbol);
        if (price == -1)
        {
            Console.WriteLine($"!!! ข้อผิดพลาด: ไม่พบชื่อหุ้น {symbol} ในระบบตลาด !!!");
            return;
        }

        //2.คำนวณยอดรวมและเช็กเงินในกระเป๋า
        double totalCost = price * quantity;
        if (!wallet.CanAfford(totalCost))
        {
            Console.WriteLine($"!!! ข้อผิดพลาด: เงินไม่พอ (ขาดอีก ${ (totalCost - 20000):N2} ) ");
            return;
        }

        //3.ดำเนินการหักเงิน บันทึกพอร์ต และแจ้งเตือน
        wallet.Withdraw(totalCost);
        portfolio.UpdatePortfolio(symbol, quantity);
        notification.SendConfirmation(symbol, quantity, price);

        Console.WriteLine($">>> ทำรายการ {symbol} เสร็จสมบูรณ์ <<<");
    }
}
}