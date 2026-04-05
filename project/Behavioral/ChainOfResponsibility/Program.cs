using System;

public class Document
{
    public string customerName { get; set; }
    public bool hasIdCard { get; set; }       // มีบัตรประชาชนไหม
    public bool isNDIDVerified { get; set; }  // ยืนยันผ่านแอปธนาคารหรือยัง
    public int age { get; set; }              // อายุ
    public bool isBlacklisted { get; set; }   // ติดแบล็คลิสต์ไหม
    public int riskScore { get; set; }        // คะแนนประเมินความเสี่ยง
    public bool isProfessional { get; set; }  // เป็นเทรดเดอร์มืออาชีพไหม
    public bool isDemoAccount { get; set; }   // ขอเปิดแค่พอร์ตจำลองใช่ไหม

}

public interface IVerificationHandler
{
    IVerificationHandler SetNext(IVerificationHandler handler);
    void Verify(Document doc);
}

public abstract class VerificationBase : IVerificationHandler
{
    private IVerificationHandler nextHandler;

    public IVerificationHandler SetNext(IVerificationHandler handler)
    {
        nextHandler = handler;
        return nextHandler;
    }

    public virtual void Verify(Document doc)
    {
        if (nextHandler != null)
        {
            nextHandler.Verify(doc);
        }
    }
}

public class DocumentCheck : VerificationBase
{
    public override void Verify(Document doc)
    {
        if (doc.isNDIDVerified)
        {
            Console.WriteLine($"[DocumentCheck] ข้ามขั้นตอนตรวจเอกสาร (ยืนยันผ่าน NDID มาแล้ว)");
            base.Verify(doc);
            return;
        }

        Console.WriteLine($"[DocumentCheck] ตรวจสอบเอกสารบัตรประชาชน...");
        if (!doc.hasIdCard)
        {
            Console.WriteLine($"ENDPROCESS {doc.customerName}: ขาดเอกสารสำคัญ (บัตรประชาชน)\n");
            return;
        }

        Console.WriteLine($"SUCCESS {doc.customerName}: เอกสารครบถ้วน");
        base.Verify(doc);
    }
}

public class AgeVerification : VerificationBase
{
    private int minAge;
    public AgeVerification(int minAge) { this.minAge = minAge; }

    public override void Verify(Document doc)
    {
        if (doc.isDemoAccount || doc.isNDIDVerified)
        {
            Console.WriteLine($"[AgeVerification] ข้ามขั้นตอนตรวจอายุ (Demo หรือ NDID)");
            base.Verify(doc);
            return;
        }

        Console.WriteLine($"[AgeVerification] ตรวจอายุ (เกณฑ์: {minAge} ปีขึ้นไป)");
        if (doc.age < minAge)
        {
            Console.WriteLine($"ENDPROCESS {doc.customerName}: อายุ {doc.age} ปี ไม่ผ่านเกณฑ์\n");
            return;
        }

        Console.WriteLine($"SUCCESS {doc.customerName}: อายุผ่านเกณฑ์");
        base.Verify(doc);
    }
}

public class AMLCheck : VerificationBase
{
    public override void Verify(Document doc)
    {
        if (doc.isDemoAccount || doc.isNDIDVerified)
        {
            Console.WriteLine($"[AMLCheck] ข้ามขั้นตอนตรวจประวัติการเงิน");
            base.Verify(doc);
            return;
        }

        Console.WriteLine($"[AMLCheck] ตรวจประวัติการฟอกเงิน...");
        if (doc.isBlacklisted) // เช็คจากฐานข้อมูลที่จำลองไว้ในเอกสาร
        {
            Console.WriteLine($"Oh game แล้ว {doc.customerName}: พบประวัติฟอกเงิน แจ้งแบนบัญชี\n");
            return;
        }

        Console.WriteLine($"SUCCESS {doc.customerName}: ประวัติขาวสะอาด");
        base.Verify(doc);
    }
}

public class RiskAssessment : VerificationBase
{
    private int passScore;
    public RiskAssessment(int passScore) { this.passScore = passScore; }

    public override void Verify(Document doc)
    {
        if (doc.isProfessional || doc.isDemoAccount)
        {
            base.Verify(doc);
            return;
        }

        Console.WriteLine($"[RiskAssessment] ประเมินความเสี่ยง (คะแนนขั้นต่ำ: {this.passScore})");
        if (doc.riskScore < passScore) // ดูคะแนนจากเอกสาร
        {
            Console.WriteLine($"ENDPROCESS {doc.customerName}: ความเสี่ยงสูงเกินไป (คะแนน: {doc.riskScore}) ส่งให้พนักงานประเมินซ้ำ\n");
            return;
        }

        Console.WriteLine($"SUCCESS {doc.customerName}: ความเสี่ยงอยู่ในเกณฑ์ปกติ");
        base.Verify(doc);
    }
}

public class Approval : VerificationBase
{
    public override void Verify(Document doc)
    {
        Console.WriteLine($"[Approval] ขั้นตอนอนุมัติ...");
        if (doc.isDemoAccount)
        {
            Console.WriteLine($"SUCCESS {doc.customerName}: อนุมัติเปิดพอร์ตจำลอง (Demo) สำเร็จ รับเงินจำลอง $100,000\n");
        }
        else
        {
            Console.WriteLine($"SUCCESS {doc.customerName}: อนุมัติเปิดพอร์ตเทรดจริง สำเร็จ\n");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        IVerificationHandler h1 = new DocumentCheck();
        IVerificationHandler h2 = new AgeVerification(20);
        IVerificationHandler h3 = new AMLCheck();
        IVerificationHandler h4 = new RiskAssessment(50);
        IVerificationHandler h5 = new Approval();

        //h1.SetNext(h2)
        //h2.SetNext(h3)
        //h3.SetNext(h4)
        //h4.SetNext(h5);
        h1.SetNext(h2).SetNext(h3).SetNext(h4).SetNext(h5);

        // h1.SetNext(h2).SetNext(h4).SetNext(h5);



        Console.WriteLine("=== เคสที่ 1: ลูกค้าทั่วไป ===");
        Document doc1 = new Document { customerName = "เนติธร", hasIdCard = true, age = 25, riskScore = 80 };
        h1.Verify(doc1);

        Console.WriteLine("=== เคสที่ 2: เอกสารไม่ครบ ===");
        Document doc2 = new Document { customerName = "ธนโชติ", hasIdCard = false };
        h1.Verify(doc2);

        Console.WriteLine("=== เคสที่ 3: ยืนยันตัวผ่าน NDID ===");
        Document doc3 = new Document { customerName = "นราธิป", isNDIDVerified = true, riskScore = 90 };
        h1.Verify(doc3);

        Console.WriteLine("=== เคสที่ 4: สมัครพอร์ตจำลอง (Demo) ===");
        Document doc4 = new Document { customerName = "เด็กชายบอย", isDemoAccount = true };
        h1.Verify(doc4);

        Console.WriteLine("=== เคสที่ 6: แก๊งฟอกเงิน ===");
        Document doc6 = new Document { customerName = "มิจจี้", hasIdCard = true, age = 40, isBlacklisted = true };
        h1.Verify(doc6);

        Console.ReadLine();
    }
}