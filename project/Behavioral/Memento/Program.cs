using System;
using System.Collections.Generic;

public interface Memento
{
    void Show();
}

public class TrackMemento : Memento
{
    private string currentTrack;
    private string playedTime;

    public TrackMemento(string track, string time)
    {
        this.currentTrack = track;
        this.playedTime = time;
    }

    public string GetCurrentTrack()
    {
        return this.currentTrack;
    }

    public string GetPlayedTime()
    {
        return this.playedTime;
    }

    public void Show()
    {
        Console.WriteLine($"[สถานะที่ถูกบันทึก] เวลา {this.playedTime} น. | เพลง: '{this.currentTrack}'");
    }
}

public class DjBooth
{
    private string currentTrack;
    private string playedTime;

    public DjBooth(string track, string time)
    {
        this.currentTrack = track;
        this.playedTime = time;
        Console.WriteLine($"[DJ] เริ่มต้นเปิดเพลง: '{this.currentTrack}' (เวลา {this.playedTime} น.)");
    }

    public void SetCurrentTrack(string track, string time)
    {
        this.currentTrack = track;
        this.playedTime = time;
        Console.WriteLine($"[DJ] เปลี่ยนเพลงเป็น: '{this.currentTrack}' (เวลา {this.playedTime} น.)");
    }

    public string GetCurrentTrack()
    {
        return this.currentTrack;
    }

    public Memento SaveMemento()
    {
        return new TrackMemento(this.currentTrack, this.playedTime);
    }

    public void RestoreTo(Memento m)
    {
        if (m is TrackMemento)
        {
            TrackMemento concrete = (TrackMemento)m;
            this.currentTrack = concrete.GetCurrentTrack();
            this.playedTime = concrete.GetPlayedTime();

            Console.WriteLine($"[DJ] UNDO_COMPLETED ตอนนี้ย้อนกลับมาเล่นเพลง: '{this.currentTrack}' (ณ เวลา {this.playedTime} น.)");
        }
    }
}

public class PlaylistHistory
{
    private List<Memento> history;
    private DjBooth dj;

    public PlaylistHistory(DjBooth dj)
    {
        this.history = new List<Memento>();
        this.dj = dj;
    }

    public void AddHistory()
    {
        Console.WriteLine("[System] กำลังบันทึกเพลงและเวลาลงในประวัติ...");
        Memento memento = this.dj.SaveMemento();
        this.history.Add(memento);
        memento.Show();
    }

    public void Undo()
    {
        if (this.history.Count == 0)
        {
            Console.WriteLine("[System] ไม่มีประวัติเพลงให้ย้อน");
            return;
        }

        string trackToCancel = this.dj.GetCurrentTrack();
        Console.WriteLine($"\n[System] ไม่เอาเพลง '{trackToCancel}' กด Undo ย้อนกลับ");

        int lastIndex = this.history.Count - 1;
        this.history.RemoveAt(lastIndex);

        if (this.history.Count > 0)
        {
            int newLastIndex = this.history.Count - 1;
            Memento previousMemento = this.history[newLastIndex];
            this.dj.RestoreTo(previousMemento);
        }
        else
        {
            Memento emptyState = new TrackMemento("ไม่มี (บูธว่างเปล่า)", "-");
            this.dj.RestoreTo(emptyState);
        }
    }

    public void ShowHistory()
    {
        Console.WriteLine($"\n--- ประวัติใน List ตอนนี้มี {this.history.Count} รายการ ---");
        for (int i = 0; i < this.history.Count; i++)
        {
            TrackMemento m = (TrackMemento)this.history[i];
            Console.WriteLine($"  [{i}] เวลา {m.GetPlayedTime()} น. - เพลง {m.GetCurrentTrack()}");
        }
        Console.WriteLine("------------------------------------------");
    }
}

class Program
{
    static void Main(string[] args)
    {
        DjBooth myDj = new DjBooth("ไม่ให้เธอไป - potato", "20:00");
        PlaylistHistory historyManager = new PlaylistHistory(myDj);
        historyManager.AddHistory();
        Console.WriteLine("-------------------------------------------------");

        myDj.SetCurrentTrack("ทะเลสีดำ - Lula", "20:05");
        historyManager.AddHistory();
        Console.WriteLine("-------------------------------------------------");

        myDj.SetCurrentTrack("คนสุดท้าย - อัสนี-วสันต์", "20:10");
        historyManager.AddHistory();
        Console.WriteLine("-------------------------------------------------");

        historyManager.ShowHistory();

        historyManager.Undo();
        historyManager.Undo();
        historyManager.Undo();
        historyManager.Undo();

        historyManager.ShowHistory();

        Console.ReadLine();
    }
}