using System;
using System.Diagnostics;

// ====== Abstract Prototype ======
public abstract class FootballShirt
{
    protected int id;
    protected string team;
    protected double price;
    protected string size;

    public FootballShirt(int id, string team, double price, string size)
    {
        this.id = id;
        this.team = team;
        this.price = price;
        this.size = size;
    }

    // Copy Constructor
    protected FootballShirt(FootballShirt prototype)
    {
        this.id = prototype.id;
        this.team = prototype.team;
        this.price = prototype.price;
        this.size = prototype.size;
    }

    public abstract FootballShirt Clone();

    public int GetId()
    {
        return id;
    }

    public string GetTeam()
    {
        return team;
    }

    public double GetPrice() {
        return price;
    }
    public void SetPrice(double newPrice) {
        price = newPrice;
    }
    public string GetSize() {
        return size;
    }
    public void SetSize(string newSize) {
        size = newSize;
    }



    public virtual void ShowDetails()
    {
        Console.WriteLine($"Team: {team}");
        Console.WriteLine($"Price: {price} THB");
        Console.WriteLine($"Size: {size}");
    }
}

// ====== Concrete Prototype: HomeKit ======
public class HomeKit : FootballShirt
{
    private string homePattern;

    public HomeKit(string homePattern, int id, string team, double price, string size) 
        : base(id, team, price, size)
    {
        this.homePattern = homePattern;
    }

    // Copy Constructor
    protected HomeKit(HomeKit prototype) : base(prototype)
    {
        this.homePattern = prototype.homePattern;
    }

    public override FootballShirt Clone()
    {
        return new HomeKit(this);
    }

    public override void ShowDetails()
    {
        Console.WriteLine("=== HOME KIT ===");
        base.ShowDetails();
        Console.WriteLine($"Pattern: {homePattern}");
    }
}

// ====== Concrete Prototype: AwayKit ======
public class AwayKit : FootballShirt
{
    private string awayPattern;

    public AwayKit(string awayPattern, int id, string team, double price, string size)
        : base(id, team, price, size)
    {
        this.awayPattern = awayPattern;
    }

    // Copy Constructor
    protected AwayKit(AwayKit prototype) : base(prototype)
    {
        this.awayPattern = prototype.awayPattern;
    }

    public override FootballShirt Clone()
    {
        return new AwayKit(this);
    }

    public override void ShowDetails()
    {
        Console.WriteLine("=== AWAY KIT ===");
        base.ShowDetails();
        Console.WriteLine($"Pattern: {awayPattern}");
    }
}

// ====== Concrete Prototype: ThirdKit ======
public class ThirdKit : FootballShirt
{
    private string thirdPattern;

    public ThirdKit(string thirdPattern, int id, string team, double price, string size)
        : base(id, team, price, size)
    {
        this.thirdPattern = thirdPattern;
    }

    // Copy Constructor
    protected ThirdKit(ThirdKit prototype) : base(prototype)
    {
        this.thirdPattern = prototype.thirdPattern;
    }

    public override FootballShirt Clone()
    {
        return new ThirdKit(this);
    }

    public override void ShowDetails()
    {
        Console.WriteLine("=== THIRD KIT ===");
        base.ShowDetails();
        Console.WriteLine($"Pattern: {thirdPattern}");
    }
}

// ====== SubClass: LimitedHomeKit ======
public class LimitedHomeKit : HomeKit
{
    private string limitedPattern;

    public LimitedHomeKit(string limitedPattern, string homePattern, int id, string team, double price, string size)
        : base(homePattern, id, team, price, size)
    {
        this.limitedPattern = limitedPattern;
    }

    // Copy Constructor
    private LimitedHomeKit(LimitedHomeKit prototype) : base(prototype)
    {
        this.limitedPattern = prototype.limitedPattern;
    }

    public override FootballShirt Clone()
    {
        return new LimitedHomeKit(this);
    }

    public override void ShowDetails()
    {
        Console.WriteLine("=== LIMITED EDITION HOME KIT ===");
        base.ShowDetails();
        Console.WriteLine($"Limited Pattern: {limitedPattern}");
    }
}

// ====== Shirt Registry ======
public class ShirtRegistry
{
    private FootballShirt[] items;
    private int itemCount;

    public ShirtRegistry()
    {
        this.items = new FootballShirt[10];
        this.itemCount = 0;
    }

    public void AddItem(FootballShirt shirt)
    {
        if (itemCount < items.Length)
        {
            items[itemCount] = shirt;
            itemCount++;
            Console.WriteLine($"เพิ่มรายการเสื้อ {shirt.GetTeam()} หมายเลขสินค้า (ID: {shirt.GetId()}) แล้ว");
        }
        else
        {
            Console.WriteLine("คลังเต็มแล้ว");
        }
    }

    public FootballShirt GetById(int id)
    {
        for (int i = 0; i < itemCount; i++)
        {
            if (items[i].GetId() == id)
            {
                return items[i].Clone();
            }
        }
        Console.WriteLine($"เสื้อ ID {id} ไม่พบ/หมด");
        return null!;
    }

    public void ListAllShirts()
    {
        Console.WriteLine("\n เสื้อที่พร้อมขายในร้าน:");
        Console.WriteLine("================================");
        for (int i = 0; i < itemCount; i++)
        {
            Console.WriteLine($"ID {items[i].GetId()}: {items[i].GetTeam()}");
        }
        Console.WriteLine("================================\n");
    }
}

// ====== Client Program ======
class Program
{
    static void Main()
    {   
        ShirtRegistry registry = new ShirtRegistry();

        // สร้าง Prototype 
        HomeKit manUtdHome = new HomeKit("Red Devils Stripes", 101, "Manchester United", 1500, "M");
        AwayKit realMadridAway = new AwayKit("Galacticos White", 102, "Real Madrid", 1800, "L");
        ThirdKit chelseaThird = new ThirdKit("Blue Pride Pattern", 103, "Chelsea", 1600, "XL");
        LimitedHomeKit barcelonaLimited = new LimitedHomeKit("Retro 1992 Special", "Blaugrana Classic", 104, "Barcelona", 2500, "L");

        // เพิ่มเข้า Registry
        registry.AddItem(manUtdHome);
        registry.AddItem(realMadridAway);
        registry.AddItem(chelseaThird);
        registry.AddItem(barcelonaLimited);

        // แสดงรายการเสื้อทั้งหมด
        registry.ListAllShirts();

        // ลูกค้าสั่งซื้อ Clone จาก prototype
        Console.WriteLine("ออเดอร์ของลูกค้า:\n");

        Console.WriteLine("Order #1:");
        FootballShirt order1 = registry.GetById(101);
        order1.ShowDetails();
        Console.WriteLine();
        

        Console.WriteLine("Order #2:");
        FootballShirt order2 = registry.GetById(104);
        order2.ShowDetails();
        Console.WriteLine();

        Console.WriteLine("Order #3:");
        FootballShirt order3 = registry.GetById(103);
        order3.ShowDetails();
        Console.WriteLine();
        

        // ลองสั่งเสื้อที่ไม่มีใน Registry
        Console.WriteLine("Order #4:");
        FootballShirt order4 = registry.GetById(999);

        // ====== Clone ผ่าน Main ======
        Console.WriteLine("\nClone ผ่าน Main :\n");

        HomeKit arsenalHome = new HomeKit("Gunner Pattern", 201, "Arsenal", 1700, "M");
       
        FootballShirt[] clonedArsenal =  new FootballShirt[5];
        for (int i = 0; i < 5; i++)
        {
            clonedArsenal[i] = arsenalHome.Clone();
            // Console.WriteLine($"Clone :{i + 1}");
            // clonedArsenal[i].ShowDetails();
            
        }
        for (int i = 0; i < 5; i++)
        {
            Console.WriteLine($"Clone :{i + 1}");
            clonedArsenal[i].ShowDetails();
            Console.WriteLine();
            
        }

        FootballShirt[] cloned101 =  new FootballShirt[10];

        // สั่งซื้อหลายชิ้นจากร้าน

        Console.WriteLine("Clone 101");
        for (int i = 0; i < 10; i++)
        {
            Console.WriteLine($"\nClone101 Shirt {i + 1}:");
            cloned101[i] = registry.GetById(101).Clone();
            cloned101[i].ShowDetails();
            Console.WriteLine();
        }



        Console.WriteLine("----------");
        Console.ReadLine();
    }
}