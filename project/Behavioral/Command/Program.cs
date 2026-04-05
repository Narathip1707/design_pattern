using System;

public interface Command
{
    void Execute();
}

//Receiver
public class Hero
{
    private string name;
    private string role;
    private string weapon;
    private int mana;

    public Hero(string name, string role, string weapon, int mana)
    {
        this.name = name;
        this.role = role;
        this.weapon = weapon;
        this.mana = mana;
    }

    public string GetName() { return name; }

    public void PerformNormalAttack(string skillName, int comboHits, double damage)
    {
        Console.WriteLine($"[{name}] ใช้สกิล: {skillName}");
        Console.WriteLine($"  Combo Hits: {comboHits} ครั้ง | Damage: {damage}");
    }

    public void PerformActiveSkill(string skillName, int manaCost, double damage)
    {
        mana -= manaCost;
        Console.WriteLine($"[{name}] ใช้สกิล: {skillName}");
        Console.WriteLine($"  Mana Cost: {manaCost} | Damage: {damage} | Mana เหลือ: {mana}");
    }

    public void PerformBuff(string skillName, int manaCost, double duration)
    {
        mana -= manaCost;
        Console.WriteLine($"[{name}] ใช้สกิล: {skillName}");
        Console.WriteLine($"  Mana Cost: {manaCost} | Duration: {duration}s | Mana เหลือ: {mana}");
    }

    public void PerformUltimate(string skillName, int manaCost, double damage, string cutscene)
    {
        mana -= manaCost;
        Console.WriteLine($"[{name}] ใช้สกิล: {skillName}");
        Console.WriteLine($"  Cutscene: {cutscene}");
        Console.WriteLine($"  Mana Cost: {manaCost} | Damage: {damage} | Mana เหลือ: {mana}");
    }
}

//Concrete Commands
public class NormalAttack : Command
{
    private Hero hero;
    private string skillName;
    private int comboHits;
    private double damage;

    public NormalAttack(Hero h, string skillName, int comboHits, double damage)
    {
        this.hero = h;
        this.skillName = skillName;
        this.comboHits = comboHits;
        this.damage = damage;
    }

    public void Execute()
    {
        hero.PerformNormalAttack(skillName, comboHits, damage);
    }
}

public class ActiveSkill : Command
{
    private Hero hero;
    private string skillName;
    private int manaCost;
    private double damage;

    public ActiveSkill(Hero h, string skillName, int manaCost, double damage)
    {
        this.hero = h;
        this.skillName = skillName;
        this.manaCost = manaCost;
        this.damage = damage;
    }

    public void Execute()
    {
        hero.PerformActiveSkill(skillName, manaCost, damage);
    }
}

public class BuffSkill : Command
{
    private Hero hero;
    private string skillName;
    private int manaCost;
    private double duration;

    public BuffSkill(Hero h, string skillName, int manaCost, double duration)
    {
        this.hero = h;
        this.skillName = skillName;
        this.manaCost = manaCost;
        this.duration = duration;
    }

    public void Execute()
    {
        hero.PerformBuff(skillName, manaCost, duration);
    }
}

public class UltimateSkill : Command
{
    private Hero hero;
    private string skillName;
    private int manaCost;
    private double damage;
    private string cutscene;

    public UltimateSkill(Hero h, string skillName, int manaCost, double damage, string cutscene)
    {
        this.hero = h;
        this.skillName = skillName;
        this.manaCost = manaCost;
        this.damage = damage;
        this.cutscene = cutscene;
    }

    public void Execute()
    {
        hero.PerformUltimate(skillName, manaCost, damage, cutscene);
    }
}

//Invoker
public class SkillButton
{
    private Command command;

    public SkillButton() { }

    public void SetCommand(Command command)
    {
        this.command = command;
    }

    public void PressButton()
    {
        ExecuteCommand();
    }

    private void ExecuteCommand()
    {
        command.Execute();
    }
}

class Program
{
    static void Main(string[] args)
    {
        //Receiver
        Hero rudy = new Hero("รูดี้", "Warrior", "Sword&Shield", 200);
        Hero karin = new Hero("คาริน", "Support", "Staff", 400);
        Hero Teo = new Hero("เเทโอ", "Attacker", "Katana", 200);

        //ลำดับการใส่ค่า
        // Hero, Skill Name, Mana Cost (ถ้ามี), Damage/Duration, Cutscene (ถ้ามี)
        List<Command> skillQueue = new List<Command>();

        skillQueue.Add(new BuffSkill(karin, "บัพพลังโจมตี", 50, 10));
        skillQueue.Add(new BuffSkill(rudy, "บัพพลังป้องกัน", 30, 15));
        skillQueue.Add(new NormalAttack(Teo, "โจมตีปกติ", 0, 150));
        skillQueue.Add(new ActiveSkill(Teo, "สกิลโจมตี", 50, 400));
        skillQueue.Add(new UltimateSkill(Teo, "อัลติเมท", 100, 1000, "ฉากคัทซีนเอฟเฟคฟาดฟันดาบศัตรูด้วยความไวแสง"));


        SkillButton button = new SkillButton();
        Console.WriteLine("=== เริ่มการต่อสู้ ===\n");

        foreach (Command skill in skillQueue) 
        {
            button.SetCommand(skill);
            button.PressButton();
            Console.WriteLine();
        }

        Console.WriteLine("=== จบการต่อสู้ ===");
        Console.ReadLine();
    }
}
