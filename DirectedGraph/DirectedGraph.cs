namespace Graph
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    // 参考 https://www.hanachiru-blog.com/entry/2020/11/23/120000
    public class DirectedGraph<T>
    {
        public IReadOnlyDictionary<T, List<T>> Vertices => _vertices;
        private readonly Dictionary<T, List<T>> _vertices = new Dictionary<T, List<T>>();

        public delegate void TraverseFunc(T t);

        public DirectedGraph(){}

        public void AddVertex(T label)
        {
            if(!_vertices.ContainsKey(label)) 
            {
                _vertices[label] = new List<T>();
            }
        }

        public bool AddEdge(T from, T to)
        {
            if(!_vertices.ContainsKey(from) || !_vertices.ContainsKey(to))
                return false;

            if(_vertices[from].Contains(to)) 
                return false;

            _vertices[from].Add(to);
            return true;
        }

        public void Traverse(T entry, TraverseFunc func)
        {
            if(!_vertices.ContainsKey(entry)) return;

            Stack<T> vertices = new Stack<T>();
            Dictionary<T, bool> arrived = _vertices.Keys.ToDictionary(v => v, _ => false);
            vertices.Push(entry);
            while(vertices.Count != 0)
            {
                var arr = vertices.Pop();
                func(arr);
                arrived[arr] = true;

                foreach(var adj in _vertices[arr])
                {
                    if(!arrived[adj]) vertices.Push(adj);
                }
            }
        }
    }
}