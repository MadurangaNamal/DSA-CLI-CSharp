namespace practice.practiceFiles;

public class LeetCode
{
    protected LeetCode()
    { }

    public static void MergeSortedArrays(int[] nums1, int nums1Size, int[] nums2, int nums2Size)
    {
        int[] result = new int[nums1Size + nums2Size];
        int nums1Index = 0, nums2Index = 0, resultIndex = 0;

        while (nums1Index < nums1Size && nums2Index < nums2Size)
        {
            result[resultIndex++] = (nums1[nums1Index] < nums2[nums2Index]) ? nums1[nums1Index++] : nums2[nums2Index++];
        }

        if (nums1Index < nums1Size)
        {
            Array.Copy(nums1, nums1Index, result, resultIndex, nums1Size - nums1Index);
        }

        if (nums2Index < nums2Size)
        {
            Array.Copy(nums2, nums2Index, result, resultIndex, nums2Size - nums2Index);
        }

        Console.WriteLine(string.Join(", ", result));
    }

    public static string RemoveElement(int[] nums, int val)
    {
        int k = 0;
        List<string> result = new();

        for (int i = 0; i < nums.Length; i++)
        {
            if (nums[i] != val)
            {
                result.Add(nums[i].ToString());
                k++;
            }
            else
            {
                result.Add("_");
            }
        }

        return $"{k}, nums = [{string.Join(", ", result)}]";
    }

    public static int RemoveDuplicates(int[] nums)
    {
        HashSet<int> result = new();

        foreach (int number in nums)
        {
            result.Add(number);
        }

        Console.WriteLine(string.Join(", ", result));

        return result.Count;
    }

    /*Given an integer array nums sorted in non-decreasing order, 
    * remove some duplicates in-place such that each unique element appears at most twice.
      The relative order of the elements should be kept the same.*/
    public static int RemoveDuplicatesV2(int[] nums)
    {
        List<int> result = new();
        Dictionary<int, int> countMap = new();

        foreach (int n in nums)
        {
            if (!countMap.ContainsKey(n))
            {
                countMap[n] = 0;
            }
            countMap[n]++;

            if (countMap[n] <= 2)
            {
                result.Add(n);
            }
        }

        Console.WriteLine(string.Join(", ", result));

        return result.Count;
    }

    /*
     Given an array nums of size n, return the majority element.
     The majority element is the element that appears more than ⌊n / 2⌋ times. 
     You may assume that the majority element always exists in the array.
     */

    public static int MajorityElement(int[] nums)
    {
        int majorityElement = nums[0];
        int maxCount = 0;
        Dictionary<int, int> countMap = new();

        foreach (int num in nums)
        {
            if (!countMap.ContainsKey(num))
            {
                countMap[num] = 0;
            }

            countMap[num]++;
        }

        foreach (var entry in countMap)
        {
            if (entry.Value > maxCount)
            {
                maxCount = entry.Value;
                majorityElement = entry.Key;
            }
        }

        return (maxCount > nums.Length / 2) ? majorityElement : -1;
    }

    public static void Rotate(int[] nums, int k)
    {
        int[] rotatedArray = new int[nums.Length];
        int position = k + 1;

        Console.WriteLine("Original: " + string.Join(", ", nums));

        for (int i = 0; i < nums.Length; i++)
        {
            rotatedArray[i] = nums[(position + i) % nums.Length];
        }

        Console.WriteLine($"Rotated by {k} elements: " + string.Join(", ", rotatedArray));
    }

    public static int MaxProfit(int[] prices)
    {
        int profit = 0;

        for (int i = 0; i < prices.Length; i++)
        {
            for (int j = i + 1; j < prices.Length; j++)
            {
                if ((prices[i] < prices[j]) && (prices[j] - prices[i] > profit))
                {
                    profit = prices[j] - prices[i];
                }
            }
        }

        return profit;
    }

    public static int MaxProfitV2(int[] prices)
    {
        int minPrice = int.MaxValue;
        int maxProfit = 0;

        foreach (int price in prices)
        {
            if (price < minPrice)
            {
                minPrice = price;
            }
            else if (price - minPrice > maxProfit)
            {
                maxProfit = price - minPrice;
            }
        }

        return maxProfit;
    }

    public static int MaxProfitQ2(int[] prices)
    {
        int maxProfit = 0;

        for (int i = 1; i < prices.Length; i++)
        {
            if (prices[i] > prices[i - 1])
            {
                maxProfit += prices[i] - prices[i - 1];
            }
        }

        return maxProfit;
    }

    /*You are given an integer array nums.You are initially positioned at the array's first index, 
     * and each element in the array represents your maximum jump length at that position.
        Return true if you can reach the last index, or false otherwise.*/

    public static bool CanJump(int[] nums)
    {
        int reachable = 0;

        for (int i = 0; i < nums.Length; i++)
        {
            if (i > reachable)
                return false;
            reachable = Math.Max(reachable, i + nums[i]);
            // Console.WriteLine(reachable);
        }

        return true;
    }

    /*
     * You are given a 0-indexed array of integers nums of length n. You are initially positioned at nums[0].
        Each element nums[i] represents the maximum length of a forward jump from index i. 
            In other words, if you are at nums[i], you can jump to any nums[i + j] where:
        0 <= j <= nums[i] and
        i + j < n
        Return the minimum number of jumps to reach nums[n - 1].
            The test cases are generated such that you can reach nums[n - 1].

            Input: nums = [2,3,1,1,4]
            Output: 2
     * */

    public static int CountMinJumps(int[] nums)
    {
        int jumps = 0;
        int currentEnd = 0;
        int farthest = 0;

        for (int i = 0; i < nums.Length - 1; i++)
        {
            farthest = Math.Max(farthest, i + nums[i]);

            if (i == currentEnd)
            {
                jumps++;
                currentEnd = farthest;
            }
        }

        return jumps;
    }

    /*
     * Given an integer array nums, return an array answer such that answer[i] 
     * is equal to the product of all the elements of nums except nums[i].
     * */
    public static int[] ProductExceptSelf(int[] nums)
    {
        int n = nums.Length;
        int[] result = new int[n];

        // Step 1: Calculate prefix products
        result[0] = 1;
        for (int i = 1; i < n; i++)
        {
            result[i] = result[i - 1] * nums[i - 1];
        }

        // Step 2: Calculate suffix products and final result
        int suffix = 1;
        for (int i = n - 1; i >= 0; i--)
        {
            result[i] *= suffix;
            suffix *= nums[i];
        }

        return result;
    }

    /*
     *  There are n gas stations along a circular route, where the amount of gas at the ith station is gas[i].
        You have a car with an unlimited gas tank and 
        it costs cost[i] of gas to travel from the ith station to its next (i + 1)th station. 
        You begin the journey with an empty tank at one of the gas stations.
        Given two integer arrays gas and cost, 
        return the starting gas station's index if you can travel around the circuit once in the clockwise direction,
        otherwise return -1. 
        If there exists a solution, it is guaranteed to be unique.
     */
    public static int CanCompleteCircuit(int[] gas, int[] cost)
    {
        int totalGas = 0;
        int totalCost = 0;

        for (int i = 0; i < gas.Length; i++)
        {
            totalGas += gas[i];
            totalCost += cost[i];
        }

        if (totalGas < totalCost)
        {
            return -1;
        }

        int tank = 0;
        int startIndex = 0;

        for (int i = 0; i < gas.Length; i++)
        {
            tank += gas[i] - cost[i];
            if (tank < 0)
            {
                tank = 0;
                startIndex = i + 1;
            }
        }

        return startIndex;
    }

    // Convert Roman numerals to integers
    public static int RomanToInteger(string? romanNumber)
    {
        int integerValue = default;
        Dictionary<char, int> romanMap = new()
        {
            { 'I', 1 },
            { 'V', 5 },
            { 'X', 10 },
            { 'L', 50 },
            { 'C', 100 },
            { 'D', 500 },
            { 'M', 1000 }
        };

        var romanValue = string.IsNullOrEmpty(romanNumber) ?
            throw new ArgumentNullException(nameof(romanNumber), "Invalid Input") : romanNumber.ToUpper();

        Console.WriteLine($"Roman Number: {romanValue}");

        for (int i = 0; i < romanValue.Length; i++)
        {
            if (!romanMap.ContainsKey(romanValue[i]))
                throw new ArgumentException("Invalid Roman numeral character", nameof(romanNumber));

            int current = romanMap[romanValue[i]];

            if (i + 1 < romanValue.Length)
            {
                int next = romanMap[romanValue[i + 1]];

                if (current < next)
                {
                    integerValue += next - current;
                    i++;
                }
                else
                {
                    integerValue += current;
                }
            }
            else
            {
                integerValue += current;
            }

        }

        return integerValue;
    }

    /*
     * * Valid Parentheses
     * 
     * Given a string s containing just the characters '(', ')', '{', '}', '[' and ']', determine if the input string is valid. 
     * An input string is valid if open brackets are closed by the same type of brackets, in the correct order.
     * 
     */

    public static bool IsValidString(string s)
    {
        Stack<char> stack = new();
        Dictionary<char, char> map = new() { { ')', '(' }, { '}', '{' }, { ']', '[' } };

        foreach (char c in s)
        {
            if (map.ContainsKey(c))
            {
                if (stack.Count == 0 || stack.Pop() != map[c])
                    return false;
            }
            else
            {
                stack.Push(c);
            }
        }
        return stack.Count == 0;
    }

    /*
     * Given an integer array nums, return true if any value appears at least twice in the array, 
     * and return false if every element is distinct.
     */
    public static bool ContainsDuplicate(int[] numbers)
    {
        HashSet<int> set = new();

        foreach (int number in numbers)
        {
            if (!set.Add(number))
                return true;
        }

        return false;
    }

    /*
     * Given two strings s and t, return true if t is an anagram of s, and false otherwise. 
     * An anagram is a word or phrase formed by rearranging the letters of a different word or phrase, 
     * typically using all the original letters exactly once. 
     */

    public static bool IsAnagram(string s, string t)
    {
        if (s.Length != t.Length)
            return false;

        int[] count = new int[26];

        for (int i = 0; i < s.Length; i++)
        {
            count[s[i] - 'a']++;
            count[t[i] - 'a']--;
        }

        foreach (int c in count)
        {
            if (c != 0)
                return false;
        }

        return true;
    }
}

//Implement the RandomizedSet class
// RandomizedSet() Initializes the RandomizedSet object.
// bool insert(int val) Inserts an item val into the set if not present. Returns true if the item was not present, false otherwise.
// bool remove(int val) Removes an item val from the set if present. Returns true if the item was present, false otherwise.
// int getRandom() Returns a random element from the current set of elements (it's guaranteed that at least one element exists when this method is called). 
// Each element must have the same probability of being returned.

class RandomizedSet
{
    private readonly HashSet<int> _randomizedSet;

    public RandomizedSet()
    {
        _randomizedSet = new HashSet<int>();
    }

    public bool Insert(int val)
    {
        return _randomizedSet.Add(val);
    }

    public bool Remove(int val)
    {
        return _randomizedSet.Remove(val);
    }

    public int GetRandom()
    {
        int randomIndex = Random.Shared.Next(_randomizedSet.Count);
        return _randomizedSet.ElementAt(randomIndex);
    }
}
