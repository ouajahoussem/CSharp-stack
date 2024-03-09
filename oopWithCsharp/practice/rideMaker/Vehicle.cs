public class Vehicle
{
    public string Name ;
    public int NumPassengers;
    public string Color;
    public bool HasEngine;
    private int DistanceTravelled = 0;
    
    public Vehicle (string n, int np, string c, bool he)
    {
        Name =n;
        NumPassengers = np;
        Color = c;
        HasEngine = he;
    }
    public Vehicle (string n, string c)
    {
        Name = n;
        NumPassengers = 5;
        Color = c;
        HasEngine = true;
    }

    public void showInfo()
    {
        Console.WriteLine($"Name: {Name}");
        Console.WriteLine($"Number of passengers: {NumPassengers}");
        Console.WriteLine($"Color: {Color}");
        Console.WriteLine($"Has an engine: {HasEngine}");
        Console.WriteLine($"Distance Travelled: {DistanceTravelled}");
        System.Console.WriteLine("-----------");
    }
    public void Travel(int amount)
    {
        DistanceTravelled += amount;
        Console.WriteLine($"Traveled amount {amount} miles. Totaldistance travelled: {DistanceTravelled} ");
    }

}