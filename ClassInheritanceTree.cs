namespace InheritanceSearch
{
    using System;
    using Tree;

    public class ClassInheritanceTree
    {
        public Tree<Type> tree = new Tree<Type>();

        public ClassInheritanceTree()
        {
            tree.root = new Node<Type>(typeof(object));
        }

        public void ConstructTree(Type targetType)
        {
            Type baseType = targetType.BaseType;
            Node<Type> child = new Node<Type>(targetType);
            while(baseType != null)
            {
                var parent = tree.SearchFirst(baseType);
                if(parent != null)
                {
                    parent.children.Add(child);
                    child.parent = parent;
                    break;
                }

                parent = new Node<Type>(baseType);
                parent.children.Add(child);
                child.parent = parent;

                child = parent;
                baseType = baseType.BaseType;
            }
        }
    }
}