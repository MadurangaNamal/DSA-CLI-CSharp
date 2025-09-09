namespace practice.practiceFiles;

/*
     This C# code defines a generic singly linked list class called GenericList<T>,
     which allows storing and managing elements of any type T.
 */
public class GenericList<T>
{
    private Node? head;

    private class Node(T t)
    {
        public T Data { get; set; } = t;

        public Node? Next { get; set; }
    }

    public void AddHead(T t)
    {
        Node n = new(t);
        n.Next = head;
        head = n;
    }

    public IEnumerator<T> GetEnumerator()
    {
        Node? current = head;

        while (current != null)
        {
            yield return current.Data;
            current = current.Next;
        }
    }
}
