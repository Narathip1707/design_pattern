using System;
using System.Collections.Generic;
//Interface 
public interface RiderService
{
    string GetPickup();
    string GetDestination();
    double GetTotalPrice();
    void Confirm();
}

// Adaptee 1 - ระบบของ Grab
public class GrabSystem
{
    private string startPoint;
    private string dropPoint; 
    private double cost;

    public GrabSystem(string startPoint, string dropPoint, double cost)
    {
        this.startPoint = startPoint;
        this.dropPoint = dropPoint;
        this.cost = cost;
    }

    public string FetchGrabStore()
    {
        return $"[GRAB_API] => Location: {startPoint}";
    }

    public string GetDropPoint()
    {
        return $"[GRAB_API] => Target: {dropPoint}";
    }

    public double CalculateGrabCost()
    {
        return cost;
    }

    public void AcceptGrabJob()
    {
        Console.WriteLine("[Grab] ยืนยันรับงานในระบบ Grab แล้ว");
    }
}

// Adaptee 2 - ระบบของ LineMan
public class LineManSystem
{   
    private string lmAddress;
    private string customerAddress;
    private double cost;

    public LineManSystem(string lmAddress, string customerAddress, double cost)
    {
        this.lmAddress = lmAddress;
        this.customerAddress = customerAddress;
        this.cost = cost;
    }
    public string GetLMAddress()
    {
        return $"[LineMan_API] => Location: {lmAddress}";
    }
    public string GetCustomerAddress()
    {
        return $"[LineMan_API] => Customer Address: {customerAddress}";
    }
    public double CalculateLineManCost()
    {
        return cost;
    }
    public void PressConfirm()
    {
        Console.WriteLine("[LineMan] ยืนยันรับงานในระบบ LineMan แล้ว");
    }
}

// Adapter 1 
public class GrabAdapter : RiderService
{
    private GrabSystem adaptee;

    public GrabAdapter(GrabSystem grabSystem)
    {
        this.adaptee = grabSystem;
    }

    public string GetPickup()
    {
        return adaptee.FetchGrabStore();
    }

    public string GetDestination()
    {
        return adaptee.GetDropPoint();
    }
    public double GetTotalPrice()
    {
        return adaptee.CalculateGrabCost();
    }

    public void Confirm()
    {
        adaptee.AcceptGrabJob();
    }
}

// Adapter 2 
public class LineManAdapter : RiderService
{
    private LineManSystem adaptee;

    public LineManAdapter(LineManSystem lineManSystem)
    {
        this.adaptee = lineManSystem;
    }

    public string GetPickup()
    {
        return adaptee.GetLMAddress();
    }

    public string GetDestination()
    {
        return adaptee.GetCustomerAddress();
    }

    public double GetTotalPrice()
    {
        return adaptee.CalculateLineManCost();
    }
    public void Confirm()
    {
        adaptee.PressConfirm();
    }
}

// Client 
public class RiderApp
{
    private string riderName;
    private double totalEarnings;

    public RiderApp(string name)
    {
        this.riderName = name;
        this.totalEarnings = 0.0;
    }   

    public double CalculateEarnings(RiderService service)
    {
        return service.GetTotalPrice();
    }
   
    public void AcceptJob(RiderService service)
    {
        Console.WriteLine($"\nไรเดอร์: {riderName}");
        Console.WriteLine("จุดรับ: " + service.GetPickup());
        Console.WriteLine("จุดส่ง: " + service.GetDestination());
        double jobPrice = CalculateEarnings(service);
        totalEarnings += jobPrice;
        Console.WriteLine($"รับงานราคา: {jobPrice} บาท | ยอดสะสม: {totalEarnings}");
        service.Confirm();
    }
}


public class Program
{
    public static void Main()
    {
        Console.WriteLine("=== RiderApp (Client) ===\n");

        // สร้าง Client ไม่ใช้List
        Console.WriteLine("=== เรียกปกติ ====\n");
        RiderApp rider = new RiderApp("นราธิป");
        GrabSystem grabData = new GrabSystem("ร้านข้าวมันไก่", "ตึกSCL", 20.0);
        RiderService job1 = new GrabAdapter(grabData);
        rider.AcceptJob(job1);

        LineManSystem lineManData = new LineManSystem("เดอะมอลล์บางกะปิ", "ห้องค้นคว้าตึกวิทย์คอม", 25.0);
        RiderService job2 = new LineManAdapter(lineManData);
        rider.AcceptJob(job2);

        Console.WriteLine("\nList");
        RiderApp rider2 = new RiderApp("สมพง");

        List<RiderService> jobList = new List<RiderService>(); 
        jobList.Add(new GrabAdapter(new GrabSystem("ร้านส้มตำ", "ตึกวิทย์คอม", 30.0)));
        jobList.Add(new LineManAdapter(new LineManSystem("ฟู้ดคอร์ท", "ห้องสมุดกลาง", 15.0)));
        for (int i = 0; i < jobList.Count; i++)
        {
            Console.WriteLine($"งานที่: {i + 1}");
            RiderService currentJob = jobList[i];
            rider2.AcceptJob(currentJob);
            Console.WriteLine("-----------------------\n");
        }
    }
}