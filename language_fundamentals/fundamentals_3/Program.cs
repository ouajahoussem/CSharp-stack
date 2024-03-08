// See https://aka.ms/new-console-template for more information

// 1. Iterate and print values
static void PrintList(List<string> MyList)
{
    foreach (string name in MyList)
    {
        Console.WriteLine(name);
    }
}
List<string> TestStringList = new List<string>() { "Harry", "Steve", "Carla", "Jeanne" };
PrintList(TestStringList);

// 2. Print Sum
static void SumOfNumbers(List<int> IntList)
{
    int sum = IntList.Sum();
    Console.WriteLine(sum);
}
List<int> TestIntList = new List<int>() { 2, 7, 12, 9, 3 };
// You should get back 33 in this example
SumOfNumbers(TestIntList);

// 3. Find Max
static int FindMax(List<int> IntList)
{
    int max = IntList.Max();
    return max;
}
List<int> TestIntList2 = new List<int>() { -9, 12, 10, 3, 17, 5 };
int max = FindMax(TestIntList2);
Console.WriteLine("max value in the list: " + max);
// You should get back 17 in this example
FindMax(TestIntList2);

// 4. Square the Values
static List<int> SquareValues(List<int> IntList)
{
    List<int> squaredList = new List<int>();
    foreach (int num in IntList)
    {
        squaredList.Add(num * num);
    }
    return squaredList;
}
List<int> TestIntList3 = new List<int>() { 1, 2, 3, 4, 5 };
List<int> squaredList = SquareValues(TestIntList3);
Console.WriteLine(string.Join(",", squaredList));
// You should get back [1,4,9,16,25], think about how you will show that this worked
SquareValues(TestIntList3);

// 5. Replace Negative Numbers with 0
static int[] NonNegatives(int[] IntArray)
{
    for (int i = 0; i < IntArray.Length; i++)
    {
        if (IntArray[i] < 0)
        {
            IntArray[i] = 0;
        }
    }
    return IntArray;
}

int[] TestIntArray = new int[] { -1, 2, 3, -4, 5 };
int[] resultArray = NonNegatives(TestIntArray);
// You should get back [0,2,3,0,5], think about how you will show that this worked
Console.WriteLine(string.Join(",", resultArray));
NonNegatives(TestIntArray);

// 6. Print Dictionary
static void PrintDictionary(Dictionary<string, string> MyDictionary)
{
    foreach (KeyValuePair<string, string> entry in MyDictionary)
    {
        Console.WriteLine($"{entry.Key}-{entry.Value}");
    }

}
Dictionary<string, string> TestDict = new Dictionary<string, string>();
TestDict.Add("HeroName", "Iron Man");
TestDict.Add("RealName", "Tony Stark");
TestDict.Add("Powers", "Money and intelligence");
PrintDictionary(TestDict);

// // 7. Find Key
static bool FindKey(Dictionary<string, string> MyDictionary, string SearchTerm)
{
    return MyDictionary.ContainsKey(SearchTerm);
}
// Use the TestDict from the earlier example or make your own
// This should print true
Console.WriteLine(FindKey(TestDict, "RealName"));
// This should print false
Console.WriteLine(FindKey(TestDict, "Name"));

// 8. Generate a Dictionary
// Ex: Given ["Julie", "Harold", "James", "Monica"] and [6,12,7,10], return a dictionary
// {
//	"Julie": 6,
//	"Harold": 12,
//	"James": 7,
//	"Monica": 10
// } 
// List<string> names = new List<string> { "Julie", "Harold", "James", "Monica" };
// List<int> numbers = new List<int> { 6, 12, 7, 10 };

// Dictionary<string, int> resultDict = GenerateDictionary(names, numbers);
// static Dictionary<string, int> GenerateDictionary(List<string> Names, List<int> Numbers);
// {
//     for (int i = 0; i < names.Count; i++)
//     {
//         resultDict.Add(names[i], numbers[i]);
//     }
//     return resultDict;

// }

// foreach (var entry in resultDict)
// {
//     Console.WriteLine($"{entry.Key}: {entry.Value}");

// }
















