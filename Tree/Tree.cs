namespace Tree
{
    using System.Collections.Generic;

    public class Tree<T>
    {
        public Node<T> root;
        public delegate void TraverseFunc(Node<T> node);

        public Tree()
        {

        }

        public List<Node<T>> Search(T t)
        {
            if(root is null) return null;

            List<Node<T>> results = new List<Node<T>>();
            Stack<Node<T>> roots = new Stack<Node<T>>();

            roots.Push(root);
            while(roots.Count != 0)
            {
                var node = roots.Pop();
                if(Equals(t, node.data)) results.Add(node);

                foreach(var child in node.children)
                {
                    roots.Push(child);
                }
            }

            return results;
        }

        public Node<T> SearchFirst(T t)
        {
            if(root is null) return null;

            Stack<Node<T>> roots = new Stack<Node<T>>();

            roots.Push(root);
            while(roots.Count != 0)
            {
                var node = roots.Pop();
                if(Equals(t, node.data)) return node;

                foreach(var child in node.children)
                {
                    roots.Push(child);
                }
            }

            return null;
        }

        public void Traverse(TraverseFunc func)
        {
            if(root is null) return;

            Stack<Node<T>> roots = new Stack<Node<T>>();

            roots.Push(root);
            while(roots.Count != 0)
            {
                var node = roots.Pop();
                func(node);

                foreach(var child in node.children)
                {
                    roots.Push(child);
                }
            }
        }
    }
}