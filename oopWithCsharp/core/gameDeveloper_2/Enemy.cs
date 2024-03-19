public class Enemy : Attack
{
    public string EnemyName;
    public int Health = 100;
    



    public Enemy(string name, string n, int dmg) : base(n, dmg)
    {
        EnemyName = name;
    }
    public virtual void showInfo()
    {
        base.ShowInfo();
        Console.WriteLine($"Name: {EnemyName}");
        Console.WriteLine($"Health: {Health}");
        // Console.WriteLine($"Attack Name: {Name}");
        // Console.WriteLine($"Damage: {DamageAmount}");
        // Console.WriteLine("-------");

    }
    public void performattack(Enemy target)
    {
        Console.WriteLine($"{EnemyName} performs {Name} on {target.EnemyName}   ");
        target.Health -= DamageAmount;
        Console.WriteLine($"{target.EnemyName} takes {DamageAmount} damage. {target.EnemyName}'s Health: {target.Health}");

    }

}
