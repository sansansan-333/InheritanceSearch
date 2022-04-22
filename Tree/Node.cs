namespace Tree
{
    using System.Collections.Generic;

    public class Node<T>
    {
        public T data;
        public Node<T> parent = null;
        public List<Node<T>> children = new List<Node<T>>();

        public bool IsRoot => parent == null;
        public bool IsLeaf => children.Count == 0;

        public Node(T data)
        {
            this.data = data;
        }
    }
}