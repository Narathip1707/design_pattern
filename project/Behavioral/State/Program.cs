using System;
public abstract class State
{
    protected Karaoke context;

    public void SetContext(Karaoke k)
    {
        context = k;
    }

    public abstract void InsertCoin();
    public abstract void SelectSong();
    public abstract void SkipSong();
    public abstract void EndProcess();
}


public class Karaoke
{
    private State state;
    public int Credit { get; set; }
    public int QueueCount { get; set; }

    public Karaoke()
    {
        Credit = 0;
        QueueCount = 0;
        ChangeState(new WaitingState());
    }

    public void ChangeState(State s)
    {
        state = s;
        state.SetContext(this);
        Console.WriteLine($"[State Changed] → {s.GetType().Name}");
    }

    public void InsertCoin() { state.InsertCoin(); }
    public void SelectSong() { state.SelectSong(); }
    public void SkipSong() { state.SkipSong(); }
    public void EndProcess() { state.EndProcess(); }
}


public class WaitingState : State
{
    public override void InsertCoin()
    {
        context.Credit++;
        Console.WriteLine($"[WaitingState]  หยอดเหรียญแล้ว (Credit: {context.Credit}) → ไป ReadyState");
        context.ChangeState(new ReadyState());
    }

    public override void SelectSong()
    {
        Console.WriteLine("[WaitingState]   เครดิตไม่เพียงพอ กรุณาหยอดเหรียญก่อนเลือกเพลง");
    }

    public override void SkipSong()
    {
        Console.WriteLine("[WaitingState] กดข้ามวิดีโอโฆษณาหน้าตู้ → กำลังเล่นโฆษณาตัวถัดไป...");
    }

    public override void EndProcess()
    {
        Console.WriteLine("[WaitingState] สั่งปิดระบบ");
        context.ChangeState(new EndState());
    }
}


public class ReadyState : State
{
    public override void InsertCoin()
    {
        context.Credit++;
        Console.WriteLine($"[ReadyState] เพิ่มเครดิต (Credit: {context.Credit})");
    }

    public override void SelectSong()
    {
        context.Credit--;
        context.QueueCount++;
        Console.WriteLine("[ReadyState] เลือกเพลงแล้ว → ไป PlayingState");
        context.ChangeState(new PlayingState());
    }

    public override void SkipSong()
    {
        context.Credit--;
        context.QueueCount++;
        Console.WriteLine("[ReadyState] สุ่มเพลง");
        Console.WriteLine("[ReadyState] สุ่มได้เพลง: '....' ");
        context.ChangeState(new PlayingState());
    }

    public override void EndProcess()
    {
        Console.WriteLine("[ReadyState] ลูกค้าต้องการคืนเงิน  ถอนเครดิตออก ");
        context.Credit = 0;
        context.ChangeState(new WaitingState());
    }
}


public class PlayingState : State
{
    public override void InsertCoin()
    {
        context.Credit++;
        Console.WriteLine($"[PlayingState] เพิ่มเครดิต (Credit: {context.Credit})");
    }

    public override void SelectSong()
    {
        if (context.Credit > 0)
        {
            context.Credit--;
            context.QueueCount++;
            Console.WriteLine($"[PlayingState] จองเพลงเพิ่มลงในคิว (ตอนนี้คิวมี {context.QueueCount} เพลง)");
        }
        else
        {
            Console.WriteLine("[PlayingState] เครดิตไม่พอสำหรับจองเพลง กรุณาเติมเครดิตเพิ่ม");
        }
    }

    public override void SkipSong()
    {
        context.QueueCount--;
        Console.WriteLine("[PlayingState] ข้ามเพลงไปเพลงถัดไปในคิว");
    }

    public override void EndProcess()
    {
        context.QueueCount--;
        Console.WriteLine("[PlayingState] เพลงจบ → ไป ScoringState");
        context.ChangeState(new ScoringState());
    }
}


public class ScoringState : State
{
    public override void InsertCoin()
    {
        context.Credit++;
        Console.WriteLine($"[ScoringState] เพิ่มเครดิต (Credit: {context.Credit})");
    }

    public override void SelectSong()
    {
        if (context.Credit > 0)
        {
            context.Credit--;
            context.QueueCount++;
            Console.WriteLine($"[ScoringState] จองเพลงเดิมลงคิวอีกรอบ (ตอนนี้คิวมี {context.QueueCount} เพลง)");
        }
        else
        {
            Console.WriteLine("[ScoringState] เครดิตไม่พอ กรุณาเติมเครดิตเพิ่ม");
        }
    }

    public override void SkipSong()
    {
        Console.WriteLine("[ScoringState] กดข้ามหน้าจอคะแนน");
        this.EndProcess();
    }

    public override void EndProcess()
    {
        Console.WriteLine($"[ScoringState] แสดงคะแนนเสร็จ (Credit เหลือ: {context.Credit})");
        if (context.QueueCount > 0)
        {
            Console.WriteLine($"[ScoringState] มีเพลงในคิวรออยู่ {context.QueueCount} เพลง → กลับไป PlayingState (เล่นเพลงถัดไป)");
            context.ChangeState(new PlayingState());
        }
        else
        {
            if (context.Credit > 0)
            {
                Console.WriteLine($"[ScoringState] คิวเพลงหมดแล้ว แต่ยังมีเครดิตเหลือ {context.Credit} → ไป ReadyState (เลือกเพลงใหม่)");
                context.ChangeState(new ReadyState());
            }
            else
            {
                Console.WriteLine("[ScoringState] คิวเพลงหมด และเครดิตหมด → ไป WaitingState (หน้าโฆษณา)");
                context.ChangeState(new WaitingState());
            }
        }
    }
}

public class EndState : State
{
    public override void InsertCoin()
    {
        context.Credit++;
        Console.WriteLine($"[EndState] เพิ่มเครดิต (Credit: {context.Credit})");
        context.ChangeState(new ReadyState());
    }

    public override void SelectSong()
    {
        if (context.Credit > 0)
        {
            context.Credit--;
            context.QueueCount++;
            Console.WriteLine($"[EndState] จองเพลงเดิมลงคิวอีกรอบ (ตอนนี้คิวมี {context.QueueCount} เพลง)");
        }
        else
        {
            Console.WriteLine("[EndState] เครดิตไม่พอ กรุณาเติมเครดิตเพิ่ม");
        }
    }

    public override void SkipSong()
    {
        Console.WriteLine("[EndState] ไม่มีเพลงในคิว โปรดตรวจสอบเครดิตและเลือกเพลงก่อน");
    }

    public override void EndProcess()
    {
        Console.WriteLine("[EndState] ปิดระบบ");
    }
}


class Program
{
    static void Main()
    {

        Karaoke karaoke = new Karaoke(); // ระบบเริ่มที่ WaitingState

        Console.WriteLine("\nร้องเพลงปกติ และจองคิว");
        karaoke.InsertCoin();
        karaoke.InsertCoin();  // ตอนนี้ Credit = 2

        karaoke.SelectSong();  // ใช้ 1 Credit (คิว = 1) -> เปลี่ยนไป PlayingState
        karaoke.SelectSong();  // ใช้ 1 Credit (คิว = 2) -> ยังอยู่ PlayingState
        karaoke.SkipSong();    // ข้ามเพลง (คิว = 0) -> ยังอยู่ PlayingState

        karaoke.EndProcess();  // ร้องจบ (คิวเหลือ 0) -> ไป ScoringState
        karaoke.SkipSong();    // ข้ามหน้าจอคะแนน 
        karaoke.EndProcess();  // โชว์คะแนนเสร็จ ระบบเห็นว่าคิวเหลือ 0 และ Credit เหลือ 0 → ไป WaitingState (หน้าโฆษณา)

        Console.WriteLine("\nทดสอบการกดปุ่มต่างๆ ใน EndState");
        karaoke.EndProcess();  // กดปิดระบบอีกครั้ง (ยังอยู่ EndState)
        karaoke.SkipSong();    // กดข้ามเพลงใน EndState 
        karaoke.InsertCoin();  //หยอดเหรียญกลับไปที่ ReadyState




        Console.WriteLine("\nจบการทดสอบระบบคาราโอเกะ");
        Console.ReadLine();
    }
}