using System;

public interface Mediator
{
    void Notify(RestaurantUnit sender, string message);
}

public abstract class RestaurantUnit
{
    protected Mediator? mediator;
    public string UnitName { get; protected set; }

    public RestaurantUnit(string name)
    {
        UnitName = name;
    }

    public void SetMediator(Mediator mediator)
    {
        this.mediator = mediator;
    }
}

public class Waiter : RestaurantUnit
{
    public string? CurrentTable { get; private set; }
    public string? CurrentFood { get; private set; }
    public string? CurrentDrink { get; private set; }
    public int CurrentPrice { get; private set; }

    public Waiter(string name) : base(name) { }

    // public void TakeOrder(string tableNo, string food, string drink, int price)
    // {
    //     CurrentTable = tableNo;
    //     CurrentFood = food;
    //     CurrentDrink = drink;
    //     CurrentPrice = price;

    //     Console.WriteLine($"[{UnitName}] รับออเดอร์ {tableNo}: อาหาร={food}, เครื่องดื่ม={drink}, ราคา={price} บาท");

    //     mediator.Notify(this, "NewOrder");
    // }

    public void TakeOrder(string tableNo, string? food, string? drink, int price)
    {
        CurrentTable = tableNo;
        CurrentFood = food;
        CurrentDrink = drink;
        CurrentPrice = price;

        string displayFood = string.IsNullOrWhiteSpace(food) ? "ไม่มี" : food;
        string displayDrink = string.IsNullOrWhiteSpace(drink) ? "ไม่มี" : drink;

        Console.WriteLine($"[{UnitName}] รับออเดอร์ {tableNo}: อาหาร={displayFood}, เครื่องดื่ม={displayDrink}, ราคา={price} บาท");

        mediator?.Notify(this, "NewOrder");
    }

    public void ReceiveAlert(string message)
    {
        Console.WriteLine($"[{UnitName} ได้รับการแจ้งเตือน] {message}");
    }
}

public class Kitchen : RestaurantUnit
{
    private string stationName = "";

    public Kitchen(string name, string station) : base(name)
    {
        this.stationName = station;
    }

    public void PrepareFood(string tableNo, string? food)
    {
        Console.WriteLine($"[{UnitName} - {this.stationName}] กำลังทำอาหาร: {food} สำหรับ {tableNo}");
        Console.WriteLine($"[{UnitName}] ทำอาหารเสร็จแล้ว!");

        mediator.Notify(this, "FoodReady");
    }

    public string GetStationName() { return this.stationName; }
}

public class Bar : RestaurantUnit
{
    private string barZone = "";

    public Bar(string name, string zone) : base(name)
    {
        this.barZone = zone;
    }

    public void PrepareDrink(string tableNo, string? drink)
    {
        Console.WriteLine($"[{UnitName} - {this.barZone}] กำลังชงเครื่องดื่ม: {drink} สำหรับ {tableNo}");
        Console.WriteLine($"[{UnitName}] ชงเครื่องดื่มเสร็จแล้ว!");

        mediator?.Notify(this, "DrinkReady");
    }

    public string GetBarZone() { return this.barZone; }
}

public class Cashier : RestaurantUnit
{
    private int registerId;
    private int totalRevenue = 0;

    public Cashier(string name, int regId) : base(name)
    {
        this.registerId = regId;
    }

    public void AddToBill(string? tableNo, int amount)
    {
        this.totalRevenue += amount;

        Console.WriteLine($"[{UnitName} เครื่องที่ {this.registerId}] บันทึกยอด {amount} บาท ลงในบิลของ {tableNo}");
        Console.WriteLine($"[{UnitName}] ยอดรวมรายได้ปัจจุบันของเครื่องนี้: {this.totalRevenue} บาท");
    }

    public int GetRegisterId() { return this.registerId; }
    public int GetTotalRevenue() { return this.totalRevenue; }
}

public class PosSystem : Mediator
{
    private Waiter waiter;
    private Kitchen kitchen;
    private Bar bar;
    private Cashier cashier;

    public PosSystem(Waiter waiter, Kitchen kitchen, Bar bar, Cashier cashier)
    {
        this.waiter = waiter;
        this.kitchen = kitchen;
        this.bar = bar;
        this.cashier = cashier;

        this.waiter.SetMediator(this);
        this.kitchen.SetMediator(this);
        this.bar.SetMediator(this);
        this.cashier.SetMediator(this);
    }

    public void Notify(RestaurantUnit sender, string message)
    {
        if (sender == this.waiter && message == "NewOrder")
        {
            string? table = this.waiter.CurrentTable;
            string? food = this.waiter.CurrentFood;
            string? drink = this.waiter.CurrentDrink;
            int price = this.waiter.CurrentPrice;

            Console.WriteLine("\n[POS System] ได้รับออเดอร์ใหม่ กำลังตรวจสอบและกระจายงาน...");

            if (table != null)
            {
                if (!string.IsNullOrWhiteSpace(food))
                {
                    this.kitchen.PrepareFood(table, food);
                }

                if (!string.IsNullOrWhiteSpace(drink))
                {
                    this.bar.PrepareDrink(table, drink);
                }

                this.cashier.AddToBill(table, price);
            }
        }
        else if (sender == this.kitchen && message == "FoodReady")
        {
            string? table = this.waiter.CurrentTable;
            Console.WriteLine("\n[POS System] ได้รับแจ้งจากครัว กำลังเรียกเด็กเสิร์ฟ...");
            this.waiter.ReceiveAlert($"มารับอาหารของ {table} ");
        }
        else if (sender == this.bar && message == "DrinkReady")
        {
            string? table = this.waiter.CurrentTable;
            Console.WriteLine("\n[POS System] ได้รับแจ้งจากบาร์น้ำ กำลังเรียกเด็กเสิร์ฟ...");
            this.waiter.ReceiveAlert($"มารับเครื่องดื่มของ {table} ");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Waiter waiter = new Waiter("พนักงานเสิร์ฟ");
        Kitchen kitchen = new Kitchen("พ่อครัว", "ครัวหลัก");
        Bar bar = new Bar("บาร์เทนเดอร์", "โซนเครื่องดื่มเย็น");
        Cashier cashier101 = new Cashier("แคชเชียร์", 101);

        PosSystem pos = new PosSystem(waiter, kitchen, bar, cashier101);

        Console.WriteLine("=== กรณีที่ 1: สั่งครบทั้งข้าวและน้ำ ===");
        waiter.TakeOrder("โต๊ะ 1", "ไก่ย่างส้มตำ", "โค้ก", 220);

        Console.WriteLine("\n=== กรณีที่ 2: สั่งแค่อาหาร ไม่รับเครื่องดื่ม ===");
        waiter.TakeOrder("โต๊ะ 2", "ต้มแซ่บกระดูกหมู", null, 80);

        Console.WriteLine("\n=== กรณีที่ 3: สั่งแค่เครื่องดื่ม ไม่รับอาหาร ===");
        waiter.TakeOrder("โต๊ะ 3", "", "กาแฟเย็น", 45);

        Console.ReadLine();
    }
}