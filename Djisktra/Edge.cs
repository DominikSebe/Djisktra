using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Djisktra
{
    /// <summary>
    /// Represents a connection to a Vertex object.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal struct Edge<T>
    {
        #region Membets
        private readonly Vertex<T> _vertex;
        private readonly int _cost;
        #endregion

        #region Properties
        /// <summary>
        /// The Vertex object the connection is established with.
        /// </summary>
        public Vertex<T> Vertex 
        { 
            get { return this._vertex; }
        }
        /// <summary>
        /// The integer value that rates the connection.
        /// </summary>
        public int Cost
        { 
            get { return this._cost; } 
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Initialize a new Edge object.
        /// </summary>
        /// <param name="vertex">The target Vertex object of the connection.</param>
        /// <param name="cost">The rating of the connection.</param>
        public Edge(Vertex<T> vertex, int cost)
        {
            this._vertex = vertex;
            this._cost = cost;
        }
        #endregion

    }
}
