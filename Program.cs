using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using ConsoleUtility;
using InheritanceSearch;

class Program
{
    static void Main(string[] args)
    {
        InheritanceGraph inhGraph = new InheritanceGraph();

        var allTypes = new List<Type>();
        foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies()) {
            foreach (var type in assembly.GetTypes()) {
                allTypes.Add(type);
            }
        }

        ConsoleIO.ClearConsoleBuffer();

        // choose types to construct graph
        while(true)
        {
            Console.Write("Add a type [a] or Generate a graph [g]: ");

            var c = Console.ReadKey().KeyChar;
            Console.WriteLine();
            if(c == 'a')
            {
                var t = ConsoleIO.WaitForInput(allTypes);
                inhGraph.ConstructGraph(t);
            }
            else if(c == 'g') break;
            else continue;
        }

        // dump graph data
        inhGraph.Dump();

        // call python script
        var terminalPath = "/bin/bash";
        var scriptPath   = Directory.GetCurrentDirectory() + "/python/start.sh";

        var info = new ProcessStartInfo(terminalPath)
        {
            Arguments = scriptPath,
        };

        Process.Start(info);
    }
}
