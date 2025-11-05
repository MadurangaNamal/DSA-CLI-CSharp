namespace practice.practiceFiles.Models;

public class ListNode
{
    public int Value { get; set; }
    public ListNode? Next { get; set; }

    public ListNode(int val = 0, ListNode? next = null)
    {
        Value = val;
        Next = next;
    }
}
