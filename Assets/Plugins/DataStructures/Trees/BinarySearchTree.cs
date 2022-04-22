using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace DataStructures.Trees
{
    public enum TraversalType
    {
        InOrder,
        PreOrder,
        PostOrder
    }
    
    public class BinarySearchTree<T> : MonoBehaviour where T : IComparable<T>
    {
        protected delegate void Visitor(AbstractNode<T> node);

        [SerializeField] private AbstractNode<T> root;

        protected void Traverse(Visitor visitor, TraversalType type)
        {
            var nodes = type switch
            {
                TraversalType.InOrder => InOrder(root),
                TraversalType.PreOrder => PreOrder(root),
                TraversalType.PostOrder => PostOrder(root),
                _ => Array.Empty<AbstractNode<T>>()
            };

            foreach (var node in nodes)
            {
                visitor(node);
            }
        }

        public void Insert(AbstractNode<T> element)
        {
            AbstractNode<T> y = null;
            var x = root;
            while (x != null)
            {
                y = x;
                var result = element.CompareTo(x);
                if (result < 0)
                {
                    x = x.Left;
                    continue;
                }

                x = x.Right;
            }

            element.Parent = y;
            if (y == null)
            {
                root = element;
                return;
            }

            if (element.CompareTo(y) < 0)
            {
                y.Left = element;
                return;
            }

            y.Right = element;
        }

        public void Delete(AbstractNode<T> element)
        {
            if (element.Left == null)
            {
                Shift(element, element.Right);
            }
            else if (element.Right == null)
            {
                Shift(element, element.Left);
            }
            else
            {
                var x = Next(element);
                if (x.Parent != element)
                {
                    Shift(x, x.Right);
                    x.Right = element.Right;
                    x.Right.Parent = element;
                }

                Shift(element, x);
                x.Left = element.Left;
                x.Left.Parent = element;
            }
        }

        private void Shift(AbstractNode<T> x, AbstractNode<T> y)
        {
            if (x.Parent == null)
            {
                root = y;
            }
            else if (x == x.Parent.Left)
            {
                x.Parent.Left = y;
            }
            else
            {
                y.Parent.Right = y;
            }

            if (y.Parent != null)
            {
                y.Parent = x.Parent;
            }
        }

        public AbstractNode<T> Minimum() => Minimum(root);

        private static AbstractNode<T> Minimum(AbstractNode<T> node)
        {
            var x = node;
            while (x.Left != null)
            {
                x = x.Left;
            }

            return x;
        }

        public AbstractNode<T> Maximum() => Maximum(root);

        private static AbstractNode<T> Maximum(AbstractNode<T> node)
        {
            var x = node;
            while (x.Right != null)
            {
                x = x.Right;
            }

            return x;
        }

        public AbstractNode<T> Next(AbstractNode<T> element)
        {
            if (element.Right != null)
            {
                return Minimum(element.Right);
            }

            var x = element.Parent;
            while (x is {Right: { }} && element.CompareTo(x.Right) == 0)
            {
                element = x;
                x = x.Parent;
            }

            return x;
        }

        public AbstractNode<T> Search(T id) => Search(root, id);

        [SuppressMessage("ReSharper", "ConvertIfStatementToSwitchStatement")]
        private static AbstractNode<T> Search(AbstractNode<T> node, T id)
        {
            while (node != null)
            {
                var result = id.CompareTo(node.Id);
                if (result == 0)
                {
                    return node;
                }

                if (result < 0)
                {
                    node = node.Left;
                    continue;
                }

                node = node.Right;
            }

            return null;
        }

        public AbstractNode<T> Previous(AbstractNode<T> element)
        {
            if (element.Left != null)
            {
                return Maximum(element.Left);
            }

            var x = element.Parent;
            while (x != null && element == x.Left)
            {
                element = x;
                x = x.Parent;
            }

            return x;
        }

        public IEnumerable<AbstractNode<T>> Traverse() => InOrder(root);

        private static IEnumerable<AbstractNode<T>> InOrder(AbstractNode<T> x)
        {
            while (true)
            {
                if (x == null) yield break;
                foreach (var node in InOrder(x.Left)) yield return node;
                yield return x;
                x = x.Right;
            }
        }

        private static IEnumerable<AbstractNode<T>> PreOrder(AbstractNode<T> x)
        {
            while (true)
            {
                if (x == null) yield break;
                yield return x;
                foreach (var node in PreOrder(x.Left)) yield return node;
                x = x.Right;
            }
        }

        private static IEnumerable<AbstractNode<T>> PostOrder(AbstractNode<T> x)
        {
            if (x == null) yield break;
            foreach (var node in PostOrder(x.Left)) yield return node;
            foreach (var node in PostOrder(x.Right)) yield return node;
            yield return x;
        }
    }
}