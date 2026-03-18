namespace FacadeNara
{

// ระบบจัดการเงินในบัญชี
public class Wallet
{
    private double balance = 20000.00; // ยอดเงินเริ่มต้น

    public bool CanAfford(double amount) => balance >= amount;
    // public bool CanAfford(double amount){
    //     return balance >= amount;}

    public void Withdraw(double amount)
    {
        balance -= amount;
        Console.WriteLine($"- [Wallet] หักเงินสำเร็จ: ${amount:N2} | ยอดคงเหลือปัจจุบัน: ${balance:N2}");
    }
}

// ระบบตรวจสอบราคาตลาดหุ้น
public class MarketService
{
    private Dictionary<string, double> prices = new Dictionary<string, double>
    {
        { "GOOGL", 145.50 },
        { "AMZN", 175.20 },
        { "NVDA", 720.10 },
        { "S&P500", 5000.00 }
    };

    public double GetLivePrice(string symbol)
    {
        if (prices.ContainsKey(symbol))
        {
            return prices[symbol];
        }
        return -1; // กรณีไม่พบชื่อหุ้น
    }
}

// ระบบบันทึกและจัดการพอร์ตหุ้นของผู้ใช้
public class Portfolio
{
    private Dictionary<string, int> myStocks = new Dictionary<string, int>();

    public void UpdatePortfolio(string symbol, int quantity)
    {
        if (myStocks.ContainsKey(symbol))
            myStocks[symbol] += quantity;
        else
            myStocks[symbol] = quantity;

        Console.WriteLine($"- [Portfolio] บันทึกสำเร็จ: ปัจจุบันถือครอง {symbol} ทั้งหมด {myStocks[symbol]} หน่วย");
    }
}

// ระบบส่งข้อความยืนยัน
public class NotificationService
{
    public void SendConfirmation(string symbol, int qty, double price)
    {
        Console.WriteLine($"- [Notify] ส่งอีเมลยืนยันการซื้อ {symbol} จำนวน {qty} หน่วย ที่ราคา ${price:N2} เรียบร้อยแล้ว");
    }
}
}