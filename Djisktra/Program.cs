using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Djisktra
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Create Vertices
            Vertex<char> A = new Vertex<char>('A');
            Vertex<char> B = new Vertex<char>('B');
            Vertex<char> C = new Vertex<char>('C');
            Vertex<char> D = new Vertex<char>('D');
            Vertex<char> E = new Vertex<char>('E');
            Vertex<char> F = new Vertex<char>('F');
            Vertex<char> G = new Vertex<char>('G');
            Vertex<char> H = new Vertex<char>('H');

            // Create Edges
            A.Connect(B, 20); A.Connect(D, 80); A.Connect(G, 90);
            B.Connect(F, 10);
            C.Connect(D, 10); C.Connect(F, 50); C.Connect(H, 20);
            D.Connect(C, 10); D.Connect(H, 20);
            E.Connect(B, 50); E.Connect(G, 30);
            F.Connect(C, 10); F.Connect(D, 40);
            G.Connect(A, 20);

            // Create Graph
            Graph<char> graph = new Graph<char>(new Vertex<char>[8] { A, B, C, D, E, F, G, H });
            // Get shortest paths
            Dictionary<char, int> totalweights = graph.Dijsktra(A);

            // Print out results
            foreach (KeyValuePair<char, int> item in totalweights)
                Console.WriteLine("From A to {0}, the cost is {1}.", item.Key, item.Value);

            Console.ReadLine();
        }
    }
}
