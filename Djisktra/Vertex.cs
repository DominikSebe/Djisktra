using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Djisktra
{
    /// <summary>
    /// Represents a point in a Graph.
    /// </summary>
    /// <typeparam name="T">Type of value stored in the Vertex</typeparam>
    internal class Vertex<T>
    {
        #region Members
        private readonly T _value;
        private List<Edge<T>> _edges;
        #endregion

        #region Properties
        /// <summary>
        /// Get the value of the vertex.
        /// </summary>
        public T Value 
        { 
            get { return _value; } 
        
        }
        /// <summary>
        /// Get the Edge object to pointing to other vertices.
        /// </summary>
        public List<Edge<T>> Edges
        {
            get { return new List<Edge<T>>(this._edges); }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Initialize a new Vertex object.
        /// </summary>
        /// <param name="value"></param>
        public Vertex(T value)
        {
            this._value = value;
            this._edges = new List<Edge<T>>();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Create a new Edge object pointing to a specified Vertex object and store it.
        /// </summary>
        /// <param name="vertex">The Vertex the new Edge will point to.</param>
        /// <param name="cost">An integer value, that rates Edge itself.</param>
        /// <exception cref="ArgumentException">Thrown when the specified Vertex is this object, or when theres is already an Edge pointing to a Vertex of equal value.</exception>
        public void Connect(Vertex<T> vertex, int cost)
        {
            if (object.ReferenceEquals(this, vertex)) throw new ArgumentException("Vertex object is the same.", "vertex");
            if (this._edges.Exists(e => e.Vertex.Equals(vertex))) throw new ArgumentException("Vertex object of equal value is already connected.", "vertex");

            _edges.Add(new Edge<T>(vertex, cost));
        }
        /// <summary>
        /// Removes the Edge object pointing to a specified Vertex.
        /// </summary>
        /// <param name="vertex">The Vertex object, the Edge to be removed points to.</param>
        /// <exception cref="ArgumentException">Thrown when there is no Edge pointing to the specified Vertex object.</exception>
        public void Disconnect(Vertex<T> vertex)
        {
            Edge<T>? edge = this._edges.Find(e => object.ReferenceEquals(e, vertex));

            if (edge is null)
                throw new ArgumentException("Vertex is not connected.", "vertex");
            else
                this._edges.Remove(edge.Value);

        }
        #endregion

        #region Functions
        /// <summary>
        /// Check wether there is an Edge object pointing to a specified Vertex object.
        /// </summary>
        /// <param name="vertex">The Vertex object to check of connection.</param>
        /// <returns>True of False, depending wether the Vertex object is connected to this one.</returns>
        public bool Adjacent(Vertex<T> vertex)
        {
            return this._edges.Exists(e => e.Vertex.Equals(vertex));
        }
        /// <summary>
        /// Check whether the value of two Vertex objects are equal.
        /// </summary>
        /// <param name="obj">The Vertex object to compare to.</param>
        /// <returns>True or False, depending whether the two stored values are equal.</returns>
        public override bool Equals(object obj)
        {
            return (!(obj is null) && !(obj as Vertex<T> is null) && this._value.Equals((obj as Vertex<T>).Value));
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        #endregion
    }
}
