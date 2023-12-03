using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Djisktra
{
    /// <summary>
    /// Represents a graph of vertices.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class Graph<T>
    {
        #region Members
        private Vertex<T>[] _vertices;
        #endregion

        #region Properties
        /// <summary>
        /// Get a copy of the vertices of the graph.
        /// </summary>
        public Vertex<T>[] Vertices
        {
            get { return this._vertices.Clone() as Vertex<T>[]; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Initialize a new empty Graph object.
        /// </summary>
        public Graph()
        {
            this._vertices = new Vertex<T>[0];
        }
        /// <summary>
        /// Intizialie a new Graph object and populate it with the specified Vertex objects.
        /// </summary>
        /// <param name="vertices">An iterable collection of Vertex objects to add to the Graph.</param>
        public Graph(IEnumerable<Vertex<T>> vertices): this()
        {
            foreach (var vertex in vertices)
                this.Add(vertex);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Make a Vertex object the part of the Graph.
        /// </summary>
        /// <param name="vertex">A vertex object to add.</param>
        /// <exception cref="ArgumentException">Thrown when a Vertex of equal value is alread part of the Graph.</exception>
        public void Add(Vertex<T> vertex)
        { 
            if(this._vertices.Contains(vertex))
                throw new ArgumentException("Vertex is already contained within the Graph.", "vertex");
            else
            {
                Vertex<T>[] new_vertices = new Vertex<T>[this._vertices.Length + 1];
                for(int i = 0; i < this._vertices.Length; i++)
                    new_vertices[i] = this._vertices[i];

                new_vertices[new_vertices.Length - 1] = vertex;

                this._vertices = new_vertices;
            }
                
        }
        #endregion

        #region Functions
        /// <summary>
        /// Check if a Vertex of a certain value os already part of the Graph.
        /// </summary>
        /// <param name="vertex">The vertex to check.</param>
        /// <returns>True or False, depending on, whether the specified object is part of the Graph or not.</returns>
        public bool Contains(Vertex<T> vertex) => this._vertices.Contains(vertex);
        /// <summary>
        /// Calculate the lowest cost path between a specified Vertex and all others of the Graph using the Dijsktra algorythm.
        /// </summary>
        /// <param name="vertex">The Vertex object to start the paths from.</param>
        /// <returns>A Dcitionary with the values of the vertices as the keys and the cost of the paths as the values.</returns>
        /// <exception cref="ArgumentException">Thrown when the specified vertex is not part of the graph.</exception>
        public Dictionary<T, int> Dijsktra(Vertex<T> vertex)
        {
            if (!this._vertices.Contains(vertex)) throw new ArgumentException("Vertex is not contained within the Graph.", "vertex");

            List<Vertex<T>> processed = new List<Vertex<T>>(); // List of already processed vertices
            List<Vertex<T>> other_vertices = new List<Vertex<T>>(this._vertices); // List of all vertices of the graph.
            other_vertices.Remove(vertex); // Except the starting vertex (used for indexing).
            int[,] paths = new int[this._vertices.Length, this._vertices.Length - 1]; // 2D Array used for finding the shortest paths (no path calculation for start, so 1 less column)
            Dictionary<T, int> result = new Dictionary<T, int>(this._vertices.Length - 1); // The result dictionary.

            int i = 0;
            int j = 0;
            // Fill the 2D array up with maximum values.
            for (; i < paths.GetLength(0); i++)
                for (j = 0; j < paths.GetLength(1); j++)
                    paths[i, j] = int.MaxValue;

            i = 0;
            j = 0;

            // Manually process the start
            foreach (Edge<T> edge in vertex.Edges)      // Go throught all Edges
            {
                Vertex<T> adjacent = edge.Vertex;       // Get Vertex the Edge points to
                int cost = edge.Cost;                   // Get rating of the Edge
                
                j = other_vertices.IndexOf(adjacent);   // Get index of Vertex in Graph
                paths[i, j] = cost;                     // Set the the cost of path at index of the Vertex
            }
            // Add start to processed vertices
            processed.Add(vertex);

            // Go through the processed vertices (the list will fill up while looping)
            for (i = 0; i < processed.Count; i++)
            {
                // Copy previous row for default values
                for(j = 0; j < paths.GetLength(1); j++)
                    paths[i + 1, j] = paths[i, j];

                // Loop through the Edges of the processed Vertex
                foreach (Edge<T> processed_edge in processed[i].Edges)
                {
                    // Get index of the Vertex the Edge points to
                    int k = other_vertices.IndexOf(processed_edge.Vertex);

                    // Only process the Vertex the Edge points to if its not already processed and if its not the starting Vertex
                    if (!processed.Contains(processed_edge.Vertex) && !processed_edge.Vertex.Equals(vertex)){
                        // Go throught all the Edges of Vertex that the original Edge pointed to (process this Vertex)
                        foreach (Edge<T> edge in processed_edge.Vertex.Edges) if(!edge.Vertex.Equals(vertex))
                        {
                            Vertex<T> adjacent = edge.Vertex;       // Get the Vertex the Edge points to
                            int cost = edge.Cost;                   // Get the rating of the Edge

                            j = other_vertices.IndexOf(adjacent);   // Get index of the Vertex
                            int totalCost = paths[i, k] + cost;     // Calculate the total cost of the full path to the Vertex
                                                                    // (total cost of the full path to Vertex this Edge belongs to and the cost of the Edge)

                            if (totalCost < paths[i, j]) paths[i + 1, j] = totalCost;   // If this cost is less then the lowst cost to that Vertex so far, store it.

                        }
                        // Add the original Vertex to the list of processed
                        processed.Add(processed_edge.Vertex);
                    }

                }
            }

            // Add the results to the dictionary
            // Last row of 2D array contains the lowest possible cost to each Vertex
            // Using the index of the cost in the last row, and the List of other vertices, get the values of the verticies for the keys
            for (i = 0; i < paths.GetLength(1); i++)
                result.Add(other_vertices.ElementAt(i).Value, paths[paths.GetLength(0) - 1, i]);

            return result;
            
        }
        #endregion
    }
}
