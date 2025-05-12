namespace practice.practiceFiles;

public class LeetCode
{
    public static void MergeSortedArrays(int[] nums1, int m, int[] nums2, int n)
    {
        int[] result = new int[m + n];
        int i = 0, j = 0, k = 0;

        while (i < m && j < n)
        {
            result[k++] = (nums1[i] < nums2[j]) ? nums1[i++] : nums2[j++];
        }

        if (i < m)
        {
            Array.Copy(nums1, i, result, k, m - i);
        }

        if (j < n)
        {
            Array.Copy(nums2, j, result, k, n - j);
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

}
