// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

// Three Basic Arrays

int[] array = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
foreach (int arr in array)
{
    System.Console.WriteLine(arr);
}

string[] array2 = new string[] { "tim", "martin", "nikki", "sara" };
foreach (string arr in array2)
{

    System.Console.WriteLine(arr);
}

bool[] booleanArray = new bool[10];

for (int i = 0; i < booleanArray.Length; i++)
{
    booleanArray[i] = i % 2 == 0;
    System.Console.WriteLine($"{i}-{booleanArray[i]}"); 
}

// List of Flavors
List<string> iceCreamFlavors = new List<string>();
iceCreamFlavors.Add("vanilla");
iceCreamFlavors.Add("chocolate");
iceCreamFlavors.Add("strawberry");
iceCreamFlavors.Add("lemon");
iceCreamFlavors.Add("caramel");
iceCreamFlavors.Add("cookies");
System.Console.WriteLine("ice cream flavors: ");
for (int i = 0; i < iceCreamFlavors.Count; i++)
{
    System.Console.WriteLine($"{i}-{iceCreamFlavors[i]}");
}
System.Console.WriteLine("lenght of ice cream flavors is " + iceCreamFlavors.Count);
System.Console.WriteLine(iceCreamFlavors[2]);
iceCreamFlavors.RemoveAt(2);
foreach (string flavor in iceCreamFlavors)
{
    System.Console.WriteLine("-" + flavor);
}
System.Console.WriteLine("lenght of ice cream flavors is " + iceCreamFlavors.Count);

// User Dictionary
Dictionary<string, string> nameFlavorPairs = new Dictionary<string, string>();

Random rand = new Random();

foreach (string name in array2)
{
    int randomIndex = rand.Next(0, iceCreamFlavors.Count);
    string randomFlavor = iceCreamFlavors[randomIndex];
    nameFlavorPairs.Add(name, randomFlavor);

}
foreach(KeyValuePair<string,string> entry in nameFlavorPairs){
    System.Console.WriteLine($"{entry.Key} - {entry.Value}");
}





