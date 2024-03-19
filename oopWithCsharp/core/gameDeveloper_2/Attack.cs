using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;

public  class Attack 
{
    public string Name {get;set;}
    public int DamageAmount {get;set;}

    public Attack (string n,int dmg)
    {
        Name = n;
        DamageAmount= dmg;
    }

    public void ShowInfo()
    {
        Console.WriteLine($"Name: {Name}");
        Console.WriteLine($"Damage: {DamageAmount}");
        Console.WriteLine("-------");

    }
}