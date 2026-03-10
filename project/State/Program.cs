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
    public abstract void SystemFinish();
}


public class Karaoke
{
    private State state;
    public int Credit { get; set; }

    public Karaoke()
    {
        Credit = 0;
        ChangeState(new WaitingState());
    }

    public void ChangeState(State s)
    {
        state = s;
        state.SetContext(this);
        Console.WriteLine($"[State Changed] → {s.GetType().Name}");
    }

    public void InsertCoin()   { state.InsertCoin(); }
    public void SelectSong()   { state.SelectSong(); }
    public void SkipSong()     { state.SkipSong(); }
    public void SystemFinish() { state.SystemFinish(); }
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
        Console.WriteLine("[WaitingState]   เครดิตไม่เพียงพอ");
    }

    public override void SystemFinish()
    {
        Console.WriteLine("[WaitingState]   ล็อก");
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
        Console.WriteLine("[ReadyState] เลือกเพลงแล้ว → ไป PlayingState");
        context.ChangeState(new PlayingState());
    }

    public override void SkipSong()
    {
        Console.WriteLine("[ReadyState]   ล็อก");
    }

    public override void SystemFinish()
    {
        Console.WriteLine("[ReadyState]   ล็อก");
    }
}


public class PlayingState : State
{
    public override void InsertCoin()
    {
        Console.WriteLine("[PlayingState]   ล็อก (กำลังเล่นเพลง)");
    }

    public override void SelectSong()
    {
        Console.WriteLine("[PlayingState]   ล็อก (กำลังเล่นเพลง)");
    }

    public override void SkipSong()
    {
        Console.WriteLine("[PlayingState] ข้ามเพลง → ไป ScoringState");
        context.ChangeState(new ScoringState());
    }

    public override void SystemFinish()
    {
        Console.WriteLine("[PlayingState] เพลงจบ → ไป ScoringState");
        context.ChangeState(new ScoringState());
    }
}


public class ScoringState : State
{
    public override void InsertCoin()
    {
        Console.WriteLine("[ScoringState]   ล็อก (กำลังแสดงคะแนน)");
    }

    public override void SelectSong()
    {
        Console.WriteLine("[ScoringState]   ล็อก (กำลังแสดงคะแนน)");
    }

    public override void SkipSong()
    {
        Console.WriteLine("[ScoringState]   ล็อก (กำลังแสดงคะแนน)");
    }

    public override void SystemFinish()
    {
        // ใช้ credit ที่เหลืออยู่ใน context
        context.Credit--;
        Console.WriteLine($"[ScoringState] แสดงคะแนนเสร็จ (Credit เหลือ: {context.Credit})");

        if (context.Credit > 0)
        {
            Console.WriteLine("[ScoringState] ยังมีเครดิต → ไป ReadyState");
            context.ChangeState(new ReadyState());
        }
        else
        {
            Console.WriteLine("[ScoringState] เครดิตหมด → ไป WaitingState");
            context.ChangeState(new WaitingState());
        }
    }
}

class Program
{
    static void Main()
    {
        Karaoke karaoke = new Karaoke();

        Console.WriteLine("\n--- ทดสอบ: เลือกเพลงก่อนหยอดเหรียญ ---");
        karaoke.SelectSong();  // แจ้งเตือน

        Console.WriteLine("\n--- ทดสอบ: หยอดเหรียญ 2 เหรียญ แล้วเล่นเพลง ---");
        karaoke.InsertCoin();  
        karaoke.InsertCoin();  // Credit = 2
        karaoke.SelectSong();  // Ready → Playing
        karaoke.SystemFinish(); // Playing → Scoring Credit = 1 → Ready 

        Console.WriteLine("\n--- ทดสอบ: จบ Scoring (ยังมีเครดิต) ---");
        karaoke.SystemFinish(); // Scoring → Ready (Credit = 1)

        Console.WriteLine("\n--- ทดสอบ: เล่นรอบสอง แล้ว Skip ---");
        karaoke.SelectSong();  // Ready → Playing
        karaoke.SkipSong();    // Playing → Scoring
        karaoke.SystemFinish(); // Scoring → Waiting (Credit = 0)
    }
}