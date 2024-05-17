public class MeleeFighter : Attack
{
    public string _Name;
    
    public int Health = 120;

    public MeleeFighter(string n) : base("punch", 20)
    {
        _Name = n;
        Health = 120;
        
    }


    
    public void showInfo()
    {
        Console.WriteLine($"Name: {_Name}");
        Console.WriteLine($"Damage: {Health}");
        Console.WriteLine($"Damage: {Name}");
        Console.WriteLine($"Damage: {DamageAmount}");
        Console.WriteLine("-------");

    } 
    public void RageAttack(Enemy enemy)
    {
        int totalDamage = DamageAmount + 10;
        enemy.Health -= totalDamage;
        Console.WriteLine($"Rage attack dealing {totalDamage} damage");
        Console.WriteLine($"{_Name}  RageAttack   {enemy.GetType()} {enemy.EnemyName}  dealing {totalDamage} damage. brand's Health is {enemy.Health}");
    }
}