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
        foreach (var number in nums)
        {
            result.Add(number);
        }

        Console.WriteLine(string.Join(", ", result));
        return result.Count;
    }

    public static int RemoveDuplicatesV2(int[] nums)
    {
        List<int> result = new();
        Dictionary<int, long> countMap = new();

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
}
