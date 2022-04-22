using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace DataStructures.Trees
{
    public abstract class AbstractNode<T> : MonoBehaviour, IComparable<AbstractNode<T>> where T : IComparable<T>
    {
        [SerializeField] private T id;
        [SerializeField] private AbstractNode<T> left;
        [SerializeField] private AbstractNode<T> right;
        [SerializeField] private AbstractNode<T> parent;

        internal T Id => id;

        public AbstractNode<T> Left
        {
            get => left;
            internal set => left = value;
        }
        
        public AbstractNode<T> Right
        {
            get => right;
            internal set => right = value;
        }
        
        public AbstractNode<T> Parent 
        {
            get => parent;
            internal set => parent = value;
        }

        public int CompareTo(AbstractNode<T> other)
        {
            return Id.CompareTo(other.Id);
        }
    }    
}
