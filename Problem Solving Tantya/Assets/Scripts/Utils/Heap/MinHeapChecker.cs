using UnityEngine;

namespace Tantya.Heap
{
    public class MinHeapChecker : MonoBehaviour
    {
        private Node root;

        private void Start()
        {
            BinaryTree bTree = new BinaryTree();
            int[] values = { 5, 10, 15, 20, 25, 30, 35 };

            foreach (int value in values)
            {
                bTree.Insert(value);
            }

            Debug.Log($"Is Min Heap: {bTree.IsMinHeap(bTree.Root)}");
        }
    }
}