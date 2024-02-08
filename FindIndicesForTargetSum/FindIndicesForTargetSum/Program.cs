
string choice = "Y";
int[] numArray = new int[];
do
{
    Console.WriteLine("Please type in the set of numbers comma searated : (1,2,5,7)");
    string userInput = Console.ReadLine();
    
    if(string.IsNullOrEmpty(userInput))
    {
        Console.WriteLine("Sorry No input found. Exiting the application");
        return;
    }
    string[] inputArray = userInput.Split(",");

    try
    {
        numArray = Array.ConvertAll(inputArray, int.Parse);
    }
    catch(FormatException)
    {
        Console.WriteLine("Numbers provided are not valid. Please provide valid format as (e.g., 1,2,5,7)");
        continue;
    }
    Console.WriteLine("Thanks, Please enter the target sum:");
    if (!int.TryParse(Console.ReadLine(), out int target))
    {
        Console.WriteLine("Invalid Target provided. Please enter a valid number as Target value.");
        continue;
    }
    var res = FindIndex.TwoSum(numArray, target);

    Console.WriteLine(res.Length > 0 ? $"Indices are : {res[0]} , {res[1]}" : "No Indices found which results into the Target sum");

    Console.WriteLine("Do you want to continue ? Default(yes). Press N if not.");
    choice = Console.ReadLine();
} while (choice?.ToUpper() != "N");

public class FindIndex
{
    public static int[] TwoSum(int[] nums, int target)
    {
        Dictionary<int, int> indexStore = new Dictionary<int, int>();

        int numCounter = 0;

        foreach (int num in nums)
        {
            int remiderSum = target - num;
            if (indexStore.ContainsKey(remiderSum))
                return new int[indexStore[remiderSum], numCounter];

            if (!indexStore.ContainsKey(num))
                indexStore[num] = numCounter;

            numCounter++;
        }
        return Array.Empty<int>();
    }
}
