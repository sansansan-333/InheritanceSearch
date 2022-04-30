namespace InheritanceSearch
{
    using System;
    using System.Collections.Generic;
    using Graph;
    using System.Text.Json;
    using System.Text.Encodings.Web;
    using System.IO;
    using System.Text.Unicode;

    public class InheritanceGraph
    {
        public DirectedGraph<SerializableType> Graph{ get; private set; }

        public InheritanceGraph()
        {
            Graph = new DirectedGraph<SerializableType>();
        }

        public void ConstructGraph(Type targetType)
        {
            Graph.AddVertex(new SerializableType(targetType));
            Stack<Type> interfaces = new Stack<Type>();
            interfaces.Push(targetType);

            // base class

            if(!targetType.IsInterface)
            {
                var tclass = targetType;

                while(tclass != typeof(object))
                {
                    var super = tclass.BaseType;
                    Graph.AddVertex(new SerializableType(super));
                    Graph.AddEdge(new SerializableType(tclass), new SerializableType(super));
                    interfaces.Push(super);

                    tclass = super;
                }
            }

            // interfaces
            while(interfaces.Count != 0)
            {
                Type tinterface = interfaces.Pop();

                foreach(var baseInterface in tinterface.GetInterfaces(false))
                {
                    Graph.AddVertex(new SerializableType(baseInterface));
                    Graph.AddEdge(new SerializableType(tinterface), new SerializableType(baseInterface));
                    interfaces.Push(baseInterface);
                }
            }
        }

        public void Dump()
        {
            List<NodeData> nodeDataList = new List<NodeData>();
            foreach(var from in Graph.Vertices.Keys)
            {
                NodeData nodeData = new NodeData();
                nodeData.Name = from.ToString();
                nodeData.NodeType = from.type.IsClass ? "class" : "interface";
                nodeData.Adj = new List<string>();
                foreach(var to in Graph.Vertices[from])
                {
                    nodeData.Adj.Add(to.ToString());
                }

                nodeDataList.Add(nodeData);
            }
            
            var encoderSettings = new TextEncoderSettings();
            encoderSettings.AllowRanges(UnicodeRanges.All);
            string jsonString = JsonSerializer.Serialize(nodeDataList, new JsonSerializerOptions{
                Encoder = JavaScriptEncoder.Create(encoderSettings),
                WriteIndented = true,
            });

            File.WriteAllText("GraphData/inheritance_graph.json", jsonString);
        }

        private class NodeData
        {
            public string Name { get; set; }
            public List<string> Adj { get; set; }
            public string NodeType { get; set; }
        }
    }
}