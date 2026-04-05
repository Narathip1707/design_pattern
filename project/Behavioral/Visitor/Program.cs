using System;
using System.Collections.Generic;


public interface Visitor
{
    void VisitFootball(Football f);
    void VisitEsport(Esport e);
    void VisitMarathonRunner(MarathonRunner m);
}

public class DietPlanVisitor : Visitor
{
    public void VisitFootball(Football f)
    {
        Console.WriteLine($"[Diet] {f.GetName()} (ตำแหน่ง: {f.GetPosition()}) -> เน้นโปรตีนและคาร์บฟื้นฟูกล้ามเนื้อ");
    }

    public void VisitEsport(Esport e)
    {
        Console.WriteLine($"[Diet] {e.GetName()} (เวลาหน้าจอ {e.GetScreenTime()} ชม./วัน) -> เน้นโอเมก้า 3 บำรุงสายตา");
    }

    public void VisitMarathonRunner(MarathonRunner m)
    {
        Console.WriteLine($"[Diet] {m.GetName()} (ระยะทาง {m.GetDistance()} กม.) -> โหลดคาร์โบไฮเดรตก่อนแข่ง");
    }
}

public class PhysicalTherapyVisitor : Visitor
{
    public void VisitFootball(Football f)
    {
        Console.WriteLine($"[Physio] {f.GetName()} (ฟุตบอล) -> ประคบน้ำแข็งข้อเท้า และนวดคลายกล้ามเนื้อขา");
    }

    public void VisitEsport(Esport e)
    {
        Console.WriteLine($"[Physio] {e.GetName()} (อีสปอร์ต) -> นวดคลายบ่าไหล่ (ออฟฟิศซินโดรม)");
    }

    public void VisitMarathonRunner(MarathonRunner m)
    {
        Console.WriteLine($"[Physio] {m.GetName()} (มาราธอน) -> ทำ Ice Bath แช่น้ำแข็งทั้งตัว");
    }
}

public class SupplementVisitor : Visitor
{
    public void VisitFootball(Football f)
    {
        Console.WriteLine($"[Supplement] {f.GetName()} (ตำแหน่ง: {f.GetPosition()}) -> จ่ายเวย์โปรตีน และเกลือแร่ทดแทนเหงื่อ");
    }

    public void VisitEsport(Esport e)
    {
        Console.WriteLine($"[Supplement] {e.GetName()} (เวลาหน้าจอ {e.GetScreenTime()} ชม./วัน) -> จ่ายน้ำมันปลา (Fish Oil) และลูทีนบำรุงตา");
    }

    public void VisitMarathonRunner(MarathonRunner m)
    {
        Console.WriteLine($"[Supplement] {m.GetName()} (ระยะทาง {m.GetDistance()} กม.) -> จ่าย Energy Gel และ BCAA ลดความล้า");
    }
}
public interface Player
{
    void Accept(Visitor v);
}

public class Football : Player
{
    private string name;
    private string position;

    public Football(string name, string position)
    {
        this.name = name;
        this.position = position;
    }

    public void Accept(Visitor v) { v.VisitFootball(this); }

    public string GetName() { return name; }
    public string GetPosition() { return position; }
}

public class Esport : Player
{
    private string name;
    private int screenTime;

    public Esport(string name, int screenTime)
    {
        this.name = name;
        this.screenTime = screenTime;
    }

    public void Accept(Visitor v) { v.VisitEsport(this); }

    public string GetName() { return name; }
    public int GetScreenTime() { return screenTime; }
}

public class MarathonRunner : Player
{
    private string name;
    private int distance;

    public MarathonRunner(string name, int distance)
    {
        this.name = name;
        this.distance = distance;
    }

    public void Accept(Visitor v) { v.VisitMarathonRunner(this); }

    public string GetName() { return name; }
    public int GetDistance() { return distance; }
}

class Program
{
    static void Client(List<Player> players, Visitor v)
    {
        foreach (Player p in players)
        {
            p.Accept(v);
        }
    }

    static void Main(string[] args)
    {
        List<Player> players = new List<Player>();
        players.Add(new Football("โรนัลโด้", "กองหน้า"));
        players.Add(new Esport("เฟคเกอร์", 12));
        players.Add(new MarathonRunner("นราธิป", 10));

        Visitor dietVisitor = new DietPlanVisitor();
        Visitor physioVisitor = new PhysicalTherapyVisitor();
        Visitor supplementVisitor = new SupplementVisitor();

        Console.WriteLine("=== DietPlan ===");
        Client(players, dietVisitor);

        Console.WriteLine("\n=== PhysicalTherapy ===");
        Client(players, physioVisitor);

        Console.WriteLine("\n=== Supplement ===");
        Client(players, supplementVisitor);

        Console.ReadLine();
    }
}