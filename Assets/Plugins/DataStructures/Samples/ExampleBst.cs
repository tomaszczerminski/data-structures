using DataStructures.Trees;
using UnityEngine;

namespace Plugins.DataStructures.Examples
{
    public class ExampleBst : BinarySearchTree<int>
    {
        private void Awake()
        {
            Debug.Log("Traversing BST - in order");
            Traverse(element => Debug.Log($"In ORDER -> Current node: {element.name}"), TraversalType.InOrder);
            Debug.Log("Traversing BST - preorder");
            Traverse(element => Debug.Log($"PREORDER -> Current node: {element.name}"), TraversalType.PreOrder);
            Debug.Log("Traversing BST - postorder");
            Traverse(element => Debug.Log($"POSTORDER -> Current node: {element.name}"), TraversalType.PostOrder);
        }
    }
}