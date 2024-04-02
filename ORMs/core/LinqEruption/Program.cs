// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
List<Eruption> eruptions = new List<Eruption>()
{
    new Eruption("La Palma", 2021, "Canary Is", 2426, "Stratovolcano"),
    new Eruption("Villarrica", 1963, "Chile", 2847, "Stratovolcano"),
    new Eruption("Chaiten", 2008, "Chile", 1122, "Caldera"),
    new Eruption("Kilauea", 2018, "Hawaiian Is", 1122, "Shield Volcano"),
    new Eruption("Hekla", 1206, "Iceland", 1490, "Stratovolcano"),
    new Eruption("Taupo", 1910, "New Zealand", 760, "Caldera"),
    new Eruption("Lengai, Ol Doinyo", 1927, "Tanzania", 2962, "Stratovolcano"),
    new Eruption("Santorini", 46, "Greece", 367, "Shield Volcano"),
    new Eruption("Katla", 950, "Iceland", 1490, "Subglacial Volcano"),
    new Eruption("Aira", 766, "Japan", 1117, "Stratovolcano"),
    new Eruption("Ceboruco", 930, "Mexico", 2280, "Stratovolcano"),
    new Eruption("Etna", 1329, "Italy", 3320, "Stratovolcano"),
    new Eruption("Bardarbunga", 1477, "Iceland", 2000, "Stratovolcano")
};
// Example Query - Prints all Stratovolcano eruptions
IEnumerable<Eruption> stratovolcanoEruptions = eruptions.Where(c => c.Type == "Stratovolcano");
PrintEach(stratovolcanoEruptions, "Stratovolcano eruptions.");
// Execute Assignment Tasks here!

// 1.Use LINQ to find the first eruption that is in Chile and print the result
Eruption chileEruption = eruptions.FirstOrDefault(e => e.Location == "Chile");
Console.WriteLine(" --------");
Console.WriteLine(chileEruption);

// 2.Find the first eruption from the "Hawaiian Is" location and print it. If none is found, print "No Hawaiian Is Eruption found."

Eruption hawaiianEruption = eruptions.FirstOrDefault(e => e.Location == "Hawaiian Is");
{
    if (hawaiianEruption != null)
    {
        Console.WriteLine(hawaiianEruption);
    }
    else
    {
        Console.WriteLine("No Hawaiian Is Eruption found");
    }

}

// 3. Find the first eruption from the "Greenland" location and print it. If none is found, print "No Greenland Eruption found."
Eruption greenLandEruption = eruptions.FirstOrDefault(e => e.Location == "Greenland");
{
    if (greenLandEruption != null)
    {
        Console.WriteLine(greenLandEruption);
    }
    else
    {
        Console.WriteLine(" No Greenland Eruption found");
    }

}

// 4. Find the first eruption that is after the year 1900 AND in "New Zealand", then print it.
Eruption newZealandEruption = eruptions.FirstOrDefault(e => e.Year > 1900 && e.Location == "New Zealand");
Console.WriteLine(newZealandEruption);

// 5. Find all eruptions where the volcano's elevation is over 2000m and print them.
IEnumerable<Eruption> elevationEruption = eruptions.Where(e => e.ElevationInMeters > 2000);
Console.WriteLine("*********");
foreach (Eruption eruption in elevationEruption)
{

    Console.WriteLine(eruption);
}


// 6. Find all eruptions where the volcano's name starts with "L" and print them. Also print the number of eruptions found.
IEnumerable<Eruption> FirstLetterEruption = eruptions.Where(e => e.Volcano.StartsWith("L"));
int count = FirstLetterEruption.Count();
foreach (Eruption eruption in FirstLetterEruption)
{
    Console.WriteLine("======");
    Console.WriteLine(eruption);

}
Console.WriteLine($"the number of eruptions found is: {count}");

// 7.Find the highest elevation, and print only that integer 
int highestEruption = eruptions.Max(e => e.ElevationInMeters);
Console.WriteLine(highestEruption);

// 8. Use the highest elevation variable to find and print the name of the Volcano with that elevation.
Eruption volcanoWithHighestElevation = eruptions.FirstOrDefault(e => e.ElevationInMeters == highestEruption);
Console.WriteLine(volcanoWithHighestElevation);

// 9.Print all Volcano names alphabetically.
IEnumerable<string> allVolcano = eruptions.Select(e => e.Volcano).OrderBy(name => name);
foreach (string vol in allVolcano)
{

    Console.WriteLine(vol);
}

// 10.Print the sum of all the elevations of the volcanoes combined.
int elevationsSum = eruptions.Sum(e => e.ElevationInMeters);
Console.WriteLine($" Volcanoes Elevations sum : {elevationsSum}");

//  11.Print whether any volcanoes erupted in the year 2000 
bool eruptionYear = eruptions.Any(e => e.Year == 2000);
Console.WriteLine(eruptionYear);

// 12. Find all stratovolcanoes and print just the first three
IEnumerable<Eruption> takeThreeEruptions = eruptions.Where(e => e.Type == "Stratovolcano").Take(3);
foreach (Eruption er in takeThreeEruptions)
{
    Console.WriteLine(er);
}

// 13.Print all the eruptions that happened before the year 1000 CE alphabetically according to Volcano name.
IEnumerable<Eruption> allEruptions = eruptions.Where(e => e.Year < 1000).OrderBy(e => e.Volcano);
Console.WriteLine("========");
foreach (Eruption e in allEruptions)
{
    Console.WriteLine(e);
}

// 14. Redo the last query, but this time use LINQ to only select the volcano's name so that only the names are printed.
IEnumerable<string> allSelectedNames = eruptions.Where(e => e.Year < 1000).OrderBy(e => e.Volcano).Select(e =>e.Volcano);
Console.WriteLine("========");
foreach (string e in allSelectedNames)
{
    Console.WriteLine(e);
}
























// Helper method to print each item in a List or IEnumerable. This should remain at the bottom of your class!
static void PrintEach(IEnumerable<Eruption> items, string msg = "")
{
    Console.WriteLine("\n" + msg);
    foreach (Eruption item in items)
    {
        Console.WriteLine(item.ToString());
    }
}

