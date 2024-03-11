// See https://aka.ms/new-console-template for more information
Attack punch = new Attack("Punch", 5);
Attack Throw = new Attack("Throw", 10);
Attack fireball = new Attack("Fireball", 25);

List<Attack> allAttacks = new List<Attack> { punch, Throw, fireball };


Enemy brand = new Enemy("Brand", "fireball",25);
Enemy sett = new Enemy("Sett", "Punch",5);
Enemy  draven = new Enemy("Draven", "Throw",10);
brand.performattack(draven);
draven.performattack(brand);





List<Enemy> enemies = new List<Enemy>();
{
    enemies.Add(brand);
    enemies.Add(sett);
    enemies.Add(draven);
}

foreach (Enemy A in enemies)
{
    
    Console.WriteLine($"{A}-{A.EnemyName}"  );
    Console.WriteLine("Health: " + A._Health );
    Console.WriteLine("Attack Name: " + A.Name );
    Console.WriteLine("Damage: " + A.DamageAmount);
    Console.WriteLine("********");
    
}

// brand.showInfo();
// sett.showInfo();

// punch.ShowInfo();
// Throw.ShowInfo();
// fireball.ShowInfo();






