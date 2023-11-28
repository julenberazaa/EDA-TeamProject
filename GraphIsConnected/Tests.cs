using Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace GraphIsConnected
{
    class UnDirectedGraph<T>
    {
        public bool IsConnected()
        {
            throw new NotImplementedException();
        }

        public void AddNode(T node)
        {
            throw new NotImplementedException();
        }
        public void AddEdge(T source, T target, double weight)
        {
            throw new NotImplementedException();
        }
    }
    class DirectedGraph<T> : UnDirectedGraph<T>
    {
    }

    public class Tests
    {
        public static bool RunBasicTests()
        {
            UnDirectedGraph<string> undirectedGraph = new UnDirectedGraph<string>();
            Console.Write("Checking UnDirectedGraphs.IsConnected with empty graph...");
            if (!undirectedGraph.IsConnected())
            {
                Console.WriteLine("Error");
                return false;
            }
            Console.WriteLine("OK");
            Console.Write("Checking UnDirectedGraphs.IsConnected with a connected 1-node/0-edges graph...");
            undirectedGraph.AddNode("Node-1");
            if (!undirectedGraph.IsConnected())
            {
                Console.WriteLine("Error");
                return false;
            }
            Console.WriteLine("OK");
            Console.Write("Checking UnDirectedGraphs.IsConnected with an unconnected 2-node/0-edges graph...");
            undirectedGraph.AddNode("Node-2");
            if (undirectedGraph.IsConnected())
            {
                Console.WriteLine("Error");
                return false;
            }
            Console.WriteLine("OK");
            Console.Write("Checking UnDirectedGraphs.IsConnected with an unconnected 3-node/0-edges graph...");
            undirectedGraph.AddNode("Node-3");
            if (undirectedGraph.IsConnected())
            {
                Console.WriteLine("Error");
                return false;
            }
            Console.WriteLine("OK");
            Console.Write("Checking UnDirectedGraphs.IsConnected with an unconnected 3-node/1-edges graph...");
            undirectedGraph.AddEdge("Node-1", "Node-2", 0);
            if (undirectedGraph.IsConnected())
            {
                Console.WriteLine("Error");
                return false;
            }
            Console.WriteLine("OK");
            Console.Write("Checking UnDirectedGraphs.IsConnected with an unconnected connected 3-node/2-edges graph...");
            undirectedGraph.AddEdge("Node-2", "Node-1", 0);
            if (undirectedGraph.IsConnected())
            {
                Console.WriteLine("Error");
                return false;
            }
            Console.WriteLine("OK");
            Console.Write("Checking UnDirectedGraphs.IsConnected with a connected connected 3-node/3-edges graph...");
            undirectedGraph.AddEdge("Node-2", "Node-3", 0);
            if (!undirectedGraph.IsConnected())
            {
                Console.WriteLine("Error");
                return false;
            }
            Console.WriteLine("OK");
            Console.Write("Checking UnDirectedGraphs.IsConnected with an unconnected connected 4-node/3-edges graph...");
            undirectedGraph.AddNode("Node-4");
            if (undirectedGraph.IsConnected())
            {
                Console.WriteLine("Error");
                return false;
            }
            Console.WriteLine("OK");


            DirectedGraph<string> directedGraph = new DirectedGraph<string>();
            Console.Write("Checking DirectedGraphs.IsConnected with empty graph...");
            if (!directedGraph.IsConnected())
            {
                Console.WriteLine("Error");
                return false;
            }
            Console.WriteLine("OK");
            Console.Write("Checking DirectedGraphs.IsConnected with a connected 1-node/0-edges graph...");
            directedGraph.AddNode("Node-1");
            if (!directedGraph.IsConnected())
            {
                Console.WriteLine("Error");
                return false;
            }
            Console.WriteLine("OK");
            Console.Write("Checking DirectedGraphs.IsConnected with an unconnected 2-node/0-edges graph...");
            directedGraph.AddNode("Node-2");
            if (directedGraph.IsConnected())
            {
                Console.WriteLine("Error");
                return false;
            }
            Console.WriteLine("OK");
            Console.Write("Checking DirectedGraphs.IsConnected with an unconnected 3-node/0-edges graph...");
            directedGraph.AddNode("Node-3");
            if (directedGraph.IsConnected())
            {
                Console.WriteLine("Error");
                return false;
            }
            Console.WriteLine("OK");
            Console.Write("Checking DirectedGraphs.IsConnected with an unconnected 3-node/2-edges graph...");
            directedGraph.AddEdge("Node-1", "Node-2", 0);
            if (directedGraph.IsConnected())
            {
                Console.WriteLine("Error");
                return false;
            }
            Console.WriteLine("OK");

            Console.Write("Checking DirectedGraphs.IsConnected with a connected connected 3-node/4-edges graph...");
            directedGraph.AddEdge("Node-2", "Node-3", 0);
            if (!directedGraph.IsConnected())
            {
                Console.WriteLine("Error");
                return false;
            }
            Console.WriteLine("OK");
            Console.Write("Checking DirectedGraphs.IsConnected with an unconnected connected 4-node/4-edges graph...");
            directedGraph.AddNode("Node-4");
            if (directedGraph.IsConnected())
            {
                Console.WriteLine("Error");
                return false;
            }
            Console.WriteLine("OK");

            Console.WriteLine("Basic tests passed. The method seems to work correctly...");
            return true;
        }

        

        public static SpeedMeasure MeasureSpeed()
        {
            Console.WriteLine("Preparing test data...");
            int numSamples = 500;
            int[] array = new int[numSamples];
            Random random = new Random();
            for (int i= 0; i< numSamples; i++)
            {
                array[i] = random.Next();
            }

            UnDirectedGraph<string> unDirectedGraph = new UnDirectedGraph<string>();
            
            for (int i = 0; i < numSamples; i++)
                unDirectedGraph.AddNode(array[i].ToString());

            //Connect all nodes
            for (int i = 0; i < numSamples; i++)
            {
                for (int j= 0; j< numSamples; j++)
                {
                    if (i == j) continue;

                    unDirectedGraph.AddEdge(array[i].ToString(), array[j].ToString(), 0);
                }
            }

            Console.WriteLine($"Running UnDirectedGraph<TKey,TValue>.IsConnected with {numSamples} nodes...");
            Stopwatch stopwatch = Stopwatch.StartNew();

            bool isConnected= unDirectedGraph.IsConnected();
            if (!isConnected)
                return new SpeedMeasure() { Success = false };

            return new SpeedMeasure() { Success = true, Time = stopwatch.Elapsed.TotalSeconds };
        }
    }
}
