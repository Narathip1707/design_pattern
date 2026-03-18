using System;
using System.Collections.Generic;

public interface Transport
{
    string GetName();
    void SetName(string name);
    void SetFacilities(string facilities);
    string GetFacilities();
}

public class Airplane : Transport
{
    private string name = "";
    private string facilities = "";
    private string flightNumber = "";

    public Airplane(string name, string flightNumber)
    {
        this.name = name;
        this.flightNumber = flightNumber;

    }

    public string GetName()
    {
        return $"{this.name} (เที่ยวบิน: {this.flightNumber})";
    }

    public void SetName(string name)
    {
        this.name = name;
    }

    public string GetFlightNumber()
    {
        return this.flightNumber;
    }

    public void SetFacilities(string facilities)
    {
        this.facilities = facilities;
    }

    public string GetFacilities()
    {
        return this.facilities;
    }
}

public class Train : Transport
{
    private string name = "";
    private string facilities = "";
    private int carringCount;

    public Train(string name, int carringCount)
    {
        this.name = name;
        this.carringCount = carringCount;
    }

    public string GetName()
    {
        return $"{this.name} (จำนวน {this.carringCount} ตู้ขบวน)";
    }

    public int GetCarringCount()
    {
        return this.carringCount;
    }

    public void SetName(string name)
    {
        this.name = name;
    }

    public void SetFacilities(string facilities)
    {
        this.facilities = facilities;
    }

    public string GetFacilities()
    {
        return this.facilities;
    }
}

public class Van : Transport
{
    private string name = "";
    private string facilities = "";
    private string Plate = "";

    public Van(string name, string Plate)
    {
        this.name = name;
        this.Plate = Plate;

    }

    public string GetName()
    {
        return $"{this.name} (ทะเบียน: {this.Plate})";
    }

    public string GetPlate()
    {
        return this.Plate;
    }

    public void SetName(string name)
    {
        this.name = name;
    }

    public void SetFacilities(string facilities)
    {
        this.facilities = facilities;
    }

    public string GetFacilities()
    {
        return this.facilities;
    }
}

public abstract class Trip
{
    protected List<Transport> transports = new List<Transport>();
    protected string name;
    protected string destination;
    protected int days;

    public Trip(List<Transport> transports, string name, string destination, int days)
    {
        this.transports = transports;
        this.name = name;
        this.destination = destination;
        this.days = days;
    }

    public abstract string GetTripDetail();
}

public class LuxuryTrip : Trip
{
    private string Butler;
    private string guide;
    public LuxuryTrip(List<Transport> transports, string name, string destination, int days, string Butler, string guide)
        : base(transports, name, destination, days)
    {
        this.Butler = Butler;
        this.guide = guide;
    }
    public override string GetTripDetail()
    {
        string detail = $"[Luxury Trip] ชื่อทริป: {name}\n" +
                        $"ปลายทาง: {destination} ({days} วัน)\n" +
                        $"ผู้ดูแลส่วนตัว: {Butler}\n" +
                        $"ไกด์: {guide}\n" +
                        $"พาหนะในการเดินทาง ({transports.Count}):\n";

        for (int i = 0; i < transports.Count; i++)
        {
            detail += $"{i + 1}. {transports[i].GetName()}\n";
            detail += $"{transports[i].GetFacilities()}\n";
        }
        detail += new string('-', 40) + "\n";

        return detail;
    }
}


public class BackpackTrip : Trip
{
    private int BackpackWeight;
    private string accessories;
    public BackpackTrip(List<Transport> transports, string name, string destination, int days, int BackpackWeight, string accessories)
        : base(transports, name, destination, days)
    {
        this.BackpackWeight = BackpackWeight;
        this.accessories = accessories;
    }

    public override string GetTripDetail()
    {
        string detail = $"[Backpack Trip] ชื่อทริป: {name}\n" +
                        $"ปลายทาง: {destination} ({days} วัน)\n" +
                        $"น้ำหนักกระเป๋าเดินทาง: {BackpackWeight} kg\n" +
                        $"อุปกรณ์เสริม: {accessories}\n" +
                        $"พาหนะในการเดินทาง ({transports.Count}):\n";

        for (int i = 0; i < transports.Count; i++)
        {
            detail += $"{i + 1}. {transports[i].GetName()}\n";
            detail += $"{transports[i].GetFacilities()}\n";
        }
        detail += new string('-', 40) + "\n";

        return detail;
    }
}

public class BasicTrip : Trip
{
    private string guide;
    public BasicTrip(List<Transport> transports, string name, string destination, int days, string guide )
        : base(transports, name, destination, days)
    {
        this.guide = guide;
    }

    public override string GetTripDetail()
    {
        string detail = $"[Basic Trip] ชื่อทริป: {name}\n" +
                        $"ปลายทาง: {destination} ({days} วัน)\n" +
                        $"ไกด์: {guide}\n" +
                        $"พาหนะในการเดินทาง ({transports.Count}):\n";

        for (int i = 0; i < transports.Count; i++)
        {
            detail += $"{i + 1}. {transports[i].GetName()}\n";
            detail += $"{transports[i].GetFacilities()}\n";
        }
        detail += new string('-', 40) + "\n";

        return detail;
    }
}

class Program
{
    static void Main(string[] args)
    {
        //สร้างพาหนะ
        Transport myAirplane = new Airplane("narathipAirlines", "NNN123");
        myAirplane.SetFacilities("ที่นั่ง First Class, อาหารระดับมิชลิน, เลานจ์ส่วนตัว");

        Transport myAirplane2 = new Airplane("ResidenAirlines", "NNN456");
        myAirplane2.SetFacilities("ที่นั่ง Business Class, อาหารระดับมิชลิน, เลานจ์ส่วนตัว");

        Transport myTrain = new Train("Narathip Train", 10);
        myTrain.SetFacilities("ที่นั่งพัดลม, หน้าต่างเปิดรับลมธรรมชาติ, ข้าวกล่องสถานี");

        Transport myVan = new Van("Narathip Van", "กข2345");
        myVan.SetFacilities("ที่นั่ง 12 ที่, เครื่องเสียง, แอร์เย็นฉ่ำ");

        //สร้างทริป

        Trip trip1 = new LuxuryTrip([myAirplane, myAirplane2], "ทัวร์ดูแสงเหนือ", "ไอซ์แลนด์", 7, "ลีออน", "นราธิป");
        Console.WriteLine(trip1.GetTripDetail());

        Trip trip2 = new BackpackTrip([myTrain, myVan], "ทัวร์เดินป่า", "เชียงใหม่", 3, 20, "เต็นท์, ถุงนอน, อุปกรณ์ทำอาหาร,ชุดปฐมพยาบาล");
        Console.WriteLine(trip2.GetTripDetail());

        Trip trip3 = new LuxuryTrip([myVan], "ทัวร์บางแสน", "บางแสน", 2, "คริส", "นราธิป");
        Console.WriteLine(trip3.GetTripDetail());

        Trip trip4 = new BasicTrip([myAirplane2, myTrain], "ทัวร์สวนสัตว์", "สวนสัตว์เปิดเขาเขียว", 1,"นราธิป");
        Console.WriteLine(trip4.GetTripDetail());



        Console.ReadLine();
    }
}