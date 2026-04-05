using System;
using System.Collections.Generic;

public enum CampaignCategory
{
    General,
    VIP,
    FreeShipping
}

//Flyweight
public class CampaignType
{
    private string name;
    private double discount;
    private string terms;
    private CampaignCategory category;

    public CampaignType(string name, double discount, string terms, CampaignCategory category)
    {
        this.name = name;
        this.discount = discount;
        this.terms = terms;
        this.category = category;
    }

    public string GetName() => name;
    public double GetDiscount() => discount;
    public string GetTerms() => terms;
    public CampaignCategory GetCategory() => category;

    public string ProcessVoucher(string pinCode)
    {
        string result = $"[{pinCode}] ";
        switch (category)
        {
            case CampaignCategory.VIP:
                result += $"คูปองระดับ VIP: {name} (ลด {discount} %)\n";
                break;
            case CampaignCategory.FreeShipping:
                result += $"คูปองส่งฟรี: {name}\n";
                break;
            default:
                result += $"คูปองทั่วไป: {name} (ลด {discount} %)\n";
                break;
        }
        result += $"[Memory ID: {this.GetHashCode()}]\n";
        return result;
    }
}


// 2. FlyweightFactory
public class CampaignFactory
{
    private Dictionary<(string, CampaignCategory), CampaignType> campaignTypes = new Dictionary<(string, CampaignCategory), CampaignType>();

    public CampaignFactory()
    {
        campaignTypes[("day-equals-month", CampaignCategory.General)] = new CampaignType("day-equals-month", 25, "ไม่มีขั้นต่ำ", CampaignCategory.General);
        campaignTypes[("Mid-Month", CampaignCategory.General)] = new CampaignType("Mid-Month", 20, "ไม่มีขั้นต่ำ", CampaignCategory.General);
        campaignTypes[("Free Shipping", CampaignCategory.FreeShipping)] = new CampaignType("Free Shipping", 0, "ไม่มีขั้นต่ำ", CampaignCategory.FreeShipping);
    }


    public CampaignType GetCampaignType(string name, double discount, string terms, CampaignCategory category)
    {
        var key = (name, category);

        if (!campaignTypes.ContainsKey(key))
        {
            campaignTypes[key] = new CampaignType(name, discount, terms, category);
        }

        return campaignTypes[key];
    }
}

//Context
public class Voucher
{
    private string pinCode;
    private CampaignType type;

    public Voucher(string pinCode, CampaignType type)
    {
        this.pinCode = pinCode;
        this.type = type;
    }

    public string GetpinCode() => pinCode;

    public double GetDiscount() => type.GetDiscount();

    public CampaignType GetCampaignType() => type;
    public string ProcessVoucher()
    {
        return type.ProcessVoucher(pinCode);
    }
}

public class VoucherSystem
{
    private List<Voucher> vouchers = new List<Voucher>();
    private Dictionary<CampaignType, List<Voucher>> redeemedHistory = new Dictionary<CampaignType, List<Voucher>>();
    public void AddVoucher(string pinCode, string name, double discount, string terms, CampaignFactory factory, CampaignCategory category)
    {
        CampaignType type = factory.GetCampaignType(name, discount, terms, category);
        Voucher v = new Voucher(pinCode, type);
        vouchers.Add(v);

        if (!redeemedHistory.ContainsKey(type))
        {
            redeemedHistory[type] = new List<Voucher>();
        }
    }

    public void Redeem(string pinCode, CampaignCategory category)
    {
        foreach (Voucher v in vouchers)
        {
            if (v.GetpinCode() == pinCode && v.GetCampaignType().GetCategory() == category)
            {
                Console.WriteLine($"คูปอง {pinCode} หมวดหมู่: {category} กำลังถูกใช้โดยผู้ใช้");
                CampaignType type = v.GetCampaignType();
                redeemedHistory[type].Add(v);

                Console.WriteLine($"ใช้งานสำเร็จ (ลดไป {v.GetDiscount()} %)");
                return;
            }
        }
        Console.WriteLine($"ไม่พบคูปองรหัส {pinCode} หมวดหมู่: {category} ในระบบ");
    }

    public string ProcessVoucher()
    {
        string result = "";
        foreach (Voucher v in vouchers)
        {
            CampaignType type = v.GetCampaignType();
            
            int useCount = redeemedHistory[type].Count(x => x.GetpinCode() == v.GetpinCode()); 
            string status = useCount > 0 ? $"ถูกใช้งานไปแล้ว {useCount} ครั้ง" : "ยังไม่ถูกใช้งาน";
            result += v.ProcessVoucher() + $"สถานะ: {status}\n\n";
        }
        return result;
    }
}

class Program
{
    static void Main(string[] args)
    {
        CampaignFactory factory = new CampaignFactory();
        VoucherSystem mySystem = new VoucherSystem();

        //แคมเปญที่สร้างไว้ใน Factory 
        // campaignTypes[("day-equals-month", CampaignCategory.General)] = new CampaignType("day-equals-month", 25, "ไม่มีขั้นต่ำ", CampaignCategory.General);
        // campaignTypes[("Mid-Month", CampaignCategory.General)] = new CampaignType("Mid-Month", 20, "ไม่มีขั้นต่ำ", CampaignCategory.General);
        // campaignTypes[("Free Shipping", CampaignCategory.FreeShipping)] = new CampaignType("Free Shipping", 0, "ไม่มีขั้นต่ำ", CampaignCategory.FreeShipping);

        mySystem.AddVoucher("NARA44", "day-equals-month", 25, "ทุกหมวดหมู่", factory, CampaignCategory.General);
        mySystem.AddVoucher("NARA44", "day-equals-month", 25, "ทุกหมวดหมู่", factory, CampaignCategory.General);
        //
        mySystem.AddVoucher("NARA55", "day-equals-month", 25, "ทุกหมวดหมู่", factory, CampaignCategory.General); //เปลี่ยนเดือนแต่ใช้เเคมเปญเดียวกัน
        mySystem.AddVoucher("NARA55", "day-equals-month", 25, "ทุกหมวดหมู่", factory, CampaignCategory.VIP); // ใช้ชื่อแคมเปญเดียวกันแต่หมวดหมู่ต่างกัน 
        //
        mySystem.AddVoucher("NARAMID4", "Mid-Month", 20, "ไม่มีขั้นต่ำ", factory, CampaignCategory.General);
        mySystem.AddVoucher("FREESHIP", "Free Shipping", 0, "ส่งฟรี", factory, CampaignCategory.FreeShipping);

        Console.WriteLine("=== สถานะคูปองทั้งหมด ===");
        Console.WriteLine(mySystem.ProcessVoucher());

        Console.WriteLine("=== ทดสอบใช้งานคูปอง ===");
        mySystem.Redeem("NARA44", CampaignCategory.General);
        mySystem.Redeem("NARA44", CampaignCategory.General);
        mySystem.Redeem("NARA55", CampaignCategory.VIP); 
        mySystem.Redeem("NARAMID4", CampaignCategory.General);
        mySystem.Redeem("FREESHIP", CampaignCategory.FreeShipping);
        mySystem.Redeem("GHOST111", CampaignCategory.General); 


        Console.WriteLine("\n=== สถานะคูปองหลังการใช้งาน ===");
        Console.WriteLine(mySystem.ProcessVoucher());

        Console.ReadLine();
    }
}