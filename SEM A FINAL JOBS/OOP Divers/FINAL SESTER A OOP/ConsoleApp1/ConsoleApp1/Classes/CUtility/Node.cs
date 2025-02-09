class Node<T>
{
    // Attributes \\ 
    T value;
    Node<T>? next; // ? = nullable

    // Constructors \\
    public Node(T value)
    {
        this.value = value;
        next = null;
    }
    public Node(T value, Node<T> next)
    {
        this.value = value;
        this.next = next;
    }

    // Class Methods \\

    // Getters \\ 
    public T GetValue()
    {
        return value;
    }
    public Node<T> GetNext()
    {
        return next;
    }

    // Setters \\
    public void SetValue(T value)
    {
        this.value = value;
    }
    public void SetNext(Node<T> next)
    {
        this.next = next;
    }

    // Others \\ 
    public bool HasNext()
    {
        return next != null;
    }

    public override string ToString()
    {
        // Trigger the private ToString() method
        return ToString(true);
    }

    private string ToString(bool rec, string fullList = "")
    {
        if (HasNext() == true)
        {
            fullList += value.ToString() + "=>";
            return next.ToString(true, fullList);
        }
        else
        {
            fullList += value.ToString() + "=>null";
            return fullList;
        }
    }
    // NumberOfNodes() \\
    public int NumberOfNodes(Node<T> headOfList)
    {
        return NumberOfNodes(headOfList, 0);
    }
    private int NumberOfNodes(Node<T> headOfList, int count = 0)
    {
        if (headOfList.HasNext() == false)
        {
            return count + 1;
        }
        return NumberOfNodes(headOfList.GetNext(), count + 1);
    }

    // Make LL From Array \\
    public static Node<T> MakeLLFromArr(T[] arr)
    {
        if (arr.Length == 0)
        {
            return null;
        }
        Node<T> head = new Node<T>(arr[0]);
        Node<T> current = head;
        for (int i = 1; i < arr.Length; i++)
        {
            Node<T> newNode = new Node<T>(arr[i]);
            current.SetNext(newNode);
            current = newNode;
        }
        return head;
    }
}