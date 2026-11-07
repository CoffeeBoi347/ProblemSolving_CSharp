using System.Collections.Generic;

namespace Tantya.Heap
{
    public class Node
    {
        public int Value;
        public Node Left;
        public Node Right;

        public Node(int value)
        {
            Value = value;
        }
    }

    public class BinaryTree
    {
        public Node Root;

        public void Insert(int value)
        {
            Node newNode = new Node(value);

            if (Root == null)
            {
                Root = newNode;
                return;
            }

            Queue<Node> queue = new Queue<Node>();
            queue.Enqueue(Root);

            while (queue.Count > 0)
            {
                Node current = queue.Dequeue();

                if (current.Left == null)
                {
                    current.Left = newNode;
                    return;
                }

                queue.Enqueue(current.Left);

                if (current.Right == null)
                {
                    current.Right = newNode;
                    return;
                }

                queue.Enqueue(current.Right);
            }
        }

        public bool IsMinHeapOrder(Node node)
        {
            if (node == null)
                return true;

            if (node.Left != null && node.Value > node.Left.Value)
                return false;

            if (node.Right != null && node.Value > node.Right.Value)
                return false;

            return IsMinHeapOrder(node.Left) && IsMinHeapOrder(node.Right);
        }

        public bool IsCompleteTree(Node root)
        {
            if (root == null)
                return true;

            Queue<Node> queue = new Queue<Node>();
            queue.Enqueue(root);
            bool end = false;

            while (queue.Count > 0)
            {
                Node current = queue.Dequeue();
                if (current == null)
                {
                    end = true;
                }
                else
                {
                    if (end)
                        return false;
                    queue.Enqueue(current.Left);
                    queue.Enqueue(current.Right);
                }
            }

            return true;
        }

        public bool IsMinHeap(Node root)
        {
            return IsCompleteTree(root) && IsMinHeapOrder(root);
        }
    }
}