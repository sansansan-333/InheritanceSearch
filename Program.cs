﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using InheritanceSearch;
using static System.Console;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        InheritanceGraph inhGraph = new InheritanceGraph();
        inhGraph.ConstructGraph(typeof(List<>));
        inhGraph.ConstructGraph(typeof(Dictionary<,>));
        inhGraph.ConstructGraph(typeof(Queue<>));
        
        inhGraph.Graph.Vertices[new SerializableType(typeof(Queue<>))].ForEach(node => {
            WriteLine(node.typeName);
        });

        inhGraph.Dump();

        var terminalPath = "/bin/bash";
        var scriptPath   = Directory.GetCurrentDirectory() + "/python/start.sh";

        var info = new ProcessStartInfo(terminalPath)
        {
            Arguments = scriptPath,
        };

        Process.Start(info);
    }
}