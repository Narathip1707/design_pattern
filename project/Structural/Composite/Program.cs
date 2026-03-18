using System;
using System.Collections.Generic;

interface FarmUnit
{
    string GetInfo();
    void Feed();
    double GetWeight();
    double GetPrice();
}


class FishCageGroup : FarmUnit
{
    private List<FarmUnit> members = new List<FarmUnit>();
    private string name = "";

    public FishCageGroup(string name)
    {
        this.name = name;
    }

    public void Add(FarmUnit unit)
    {
        members.Add(unit);
    }

    public void Remove(FarmUnit unit)
    {
        members.Remove(unit);
    }

    public string GetInfo()
    {
        string result = name + ":\n";
        foreach (FarmUnit unit in members)
        {
            result += $"    {unit.GetInfo()}";
        }
        return result;
    }

    public void Feed()
    {
        Console.WriteLine($"\n[ประกาศ] กำลังหว่านอาหารลงใน: {name}");
        foreach (FarmUnit unit in members)
        {
            unit.Feed();
        }
    }
    
    public double GetWeight()
    {
        double totalWeight = 0;
        foreach (FarmUnit unit in members)
        {
            totalWeight += unit.GetWeight();
        }
        return totalWeight;
    }

    public double GetPrice()
    {
        double totalPrice = 0;
        foreach (FarmUnit unit in members)
        {
            totalPrice += unit.GetPrice();
        }
        return totalPrice;
    }
}


class PlaNil : FarmUnit // ปลานิล
{
    private string name;
    private double weightKg;

    public PlaNil(string name, double weightKg)
    {
        this.name = name;
        this.weightKg = weightKg;
    }

    public string GetInfo()
    {
        return $"ปลานิล: {name} (น้ำหนัก {weightKg} กก.)\n";
    }

    public void Feed()
    {
        Console.WriteLine($"   - {name} ฮุบอาหารเสียงดังจ๊วบ!");
    }
    public double GetWeight() { return weightKg; }
    public double GetPrice() { return weightKg * 60; } // โลละ 60

}

class PlaTubtim : FarmUnit // ปลาทับทิม
{
    private string name;
    private double weightKg;

    public PlaTubtim(string name, double weightKg)
    {
        this.name = name;
        this.weightKg = weightKg;
    }

    public string GetInfo()
    {
        return $"ปลาทับทิม: {name} (น้ำหนัก {weightKg} กก.)\n";
    }

    public void Feed()
    {
        Console.WriteLine($"   - {name} ว่ายขึ้นมากินอาหาร");
    }

    public double GetWeight() { return weightKg; }
    public double GetPrice() { return weightKg * 80; } // โลละ 80
}

class PlaKang : FarmUnit // ปลาคังแม่น้ำโขง
{
    private string name;
    private int ageMonths;

    public PlaKang(string name, int ageMonths)
    {
        this.name = name;
        this.ageMonths = ageMonths;
    }

    public string GetInfo()
    {
        return $"ปลาคัง (แม่น้ำโขง): {name} (อายุ {ageMonths} เดือน / หนัก {GetWeight()} กก.)\n";
    }

    public void Feed()
    {
        Console.WriteLine($"   - {name} ซุ่มกินอาหารอยู่ใต้น้ำลึก");
    }
    public double GetWeight() { return ageMonths * 0.5; } // สมมติว่าโตเดือนละ 0.5 โล
    public double GetPrice() { return GetWeight() * 150; } // โลละ 150
}

class Program
{
    static void Main(string[] args)
    {
        //1.สร้าง(Leaf)
        FarmUnit nil1 = new PlaNil("นิล-01", 1.2);
        FarmUnit nil2 = new PlaNil("นิล-02", 1.5);
        FarmUnit tubtim1 = new PlaTubtim("ทับทิม-A", 1.5);
        FarmUnit kang1 = new PlaKang("คังยักษ์-01", 24);

        //2.สร้างกระชังปลา (Composite ชั้นที่ 1)
        FishCageGroup cage1 = new FishCageGroup("กระชังที่ 1 (ปลาเศรษฐกิจ)");
        cage1.Add(nil1);
        cage1.Add(nil2);
        cage1.Add(tubtim1);

        FishCageGroup deepWaterCage = new FishCageGroup("กระชังน้ำลึกพิเศษ");
        deepWaterCage.Add(kang1);

        //3.Composite ชั้นที่ 2 - ซ้อนทับกัน
        FishCageGroup myFarm = new FishCageGroup("ฟาร์มนราธิป ปลาน้ำโขง");
        myFarm.Add(cage1);
        myFarm.Add(deepWaterCage);

        // --- แสดงผล ---
        Console.WriteLine("===  ข้อมูลสรุปฟาร์มกระชังปลา ===");
        Console.WriteLine(myFarm.GetInfo());

        Console.WriteLine("=== ประเมินราคา ===");
        Console.WriteLine($"น้ำหนักปลารวมทั้งฟาร์ม: {myFarm.GetWeight()} กิโลกรัม");
        Console.WriteLine($"มูลค่าที่ขายได้ทั้งหมด: {myFarm.GetPrice()} บาท"); 

        Console.WriteLine("===  ถึงเวลาให้อาหารปลา! ===");
        myFarm.Feed(); // สั่งให้อาหารครั้งเดียวที่ระดับฟาร์มใหญ่

        Console.ReadLine();
    }
}