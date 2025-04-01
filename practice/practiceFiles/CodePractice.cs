namespace practice.practiceFiles;

public class CodePractice
{
    //Find second largest
    public static int GetSecondLargestNumber(int[] itemList)
    {
        int largestNumber = int.MinValue;
        int secondLargestNumber = int.MinValue;

        foreach (int number in itemList)
        {
            if (number > largestNumber)
            {
                secondLargestNumber = largestNumber;
                largestNumber = number;
            }
            else if (number > secondLargestNumber && number != largestNumber)
            {
                secondLargestNumber = number;
            }
        }

        if (secondLargestNumber == int.MinValue)
        {
            Console.WriteLine("There is no second largest number");
        }

        return secondLargestNumber;
    }

    //Binary search
    public static int FindElementInSortedArray(int item, int[] sortedArray)
    {
        int low = 0;
        int high = sortedArray.Length - 1;

        while (low <= high)
        {
            int mid = low + (high - low) / 2;

            if (sortedArray[mid] == item)
            {
                return mid;
            }
            else if (sortedArray[mid] < item)
            {
                low = mid + 1;
            }
            else
            {
                high = mid - 1;
            }
        }

        return -1; // Element not found
    }

    //Merge two sorted linked lists into one sorted list
    /*
     *  Use a dummy node to simplify handling the head of the merged list.
        Use a pointer (current) to track the last node of the merged list.
        Compare nodes from both lists (l1 and l2), attaching the smaller one to current.
        Move current and the list pointer (l1 or l2) forward.
        If any list is not fully traversed, append the remaining part.
     * */
    public static ListNode? MergeTwoLists(ListNode l1, ListNode l2)
    {
        ListNode dummy = new(-1); // Dummy node to simplify handling
        ListNode current = dummy;

        while (l1 != null && l2 != null)
        {
            if (l1.Value < l2.Value)
            {
                current.Next = l1;
                l1 = l1.Next!;
            }
            else
            {
                current.Next = l2;
                l2 = l2.Next!;
            }
            current = current.Next;
        }

        // Attach the remaining elements
        current.Next = l1 ?? l2;

        return dummy.Next;
    }

    //Task Progress Reporting
    public static async Task ProcessDataAsync(string[] departments, IProgress<int> progress)
    {
        for (int i = 1; i <= departments.Length; i++)
        {
            progress.Report(i);
            await Task.Delay(1000);
        }

        Console.WriteLine("Done processing data");
    }


    //Helper methods
    public static void PrintList(ListNode? head)
    {
        while (head != null)
        {
            Console.Write(head.Value + " -> ");
            head = head.Next!;
        }
        Console.WriteLine("null");
    }

    //Using in built LinkedList
    public static void PrintLinkedListFunctions(string[]? values)
    {
        LinkedList<string>? linkedList = new(values!);

        linkedList.AddFirst("First Name");

        linkedList.AddLast("Last Name");

        LinkedListNode<string>? first = linkedList.First;
        LinkedListNode<string>? last = linkedList.Last;

        Console.WriteLine($"First: {first!.Value}, Last: {last!.Value}");

        linkedList.AddAfter(first, "Second Name");

        Console.WriteLine("--------------------------------------");

        foreach (string item in linkedList)
        {
            Console.WriteLine(item);
        }

        Console.WriteLine("--------------------------------------");

        Console.WriteLine(first.Next!.Value);
        Console.WriteLine(last.Previous!.Value);

        Console.WriteLine("--------------------------------------");

        Console.WriteLine(linkedList.Find("Second"));
        Console.WriteLine(linkedList.Find("Second Name"));
        Console.WriteLine(linkedList.Contains("Second Name"));
    }



}


