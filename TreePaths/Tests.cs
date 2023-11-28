using Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace TreePaths
{
    class GenericTreeNode<T>
    {
        T Value = default(T);
        public GenericTreeNode(T value) { }

        public string ValueAsString() { return Value.ToString(); }
        public GenericTreeNode<string> Add(T value)
        {
            throw new NotImplementedException();
        }

        public GenericTreeNode<T> GetByPath(string path)
        {
            return null;
        }
    }
    class GenericTree<T>
    {
        public GenericTreeNode<T> RootNode { get; set; } = null;
        public GenericTreeNode<T> GetByPath(string path) 
        {
            throw new NotImplementedException();
        }

    }
    public class Tests
    {
        public static bool RunBasicTests()
        {
            GenericTree<string> tree = new GenericTree<string>();
            Console.Write("Checking GenericTree.GetByPath(null) [should return null]...");
            if (tree.GetByPath(null) != null)
            {
                Console.WriteLine("Error");
                return false;
            }
            Console.WriteLine("OK");
            Console.Write("Checking GenericTree.GetByPath(\"Root\") on empty tree [should return null]...");
            if (tree.GetByPath(null) != null)
            {
                Console.WriteLine("Error");
                return false;
            }
            Console.WriteLine("OK");
            Console.Write("Checking GenericTree.GetByPath(\"Root->\") on empty tree [should return null because it's invalid]...");
            if (tree.GetByPath(null) != null)
            {
                Console.WriteLine("Error");
                return false;
            }
            Console.WriteLine("OK");
            tree.RootNode = new GenericTreeNode<string>("Root");

            Console.Write("Checking GenericTree.GetByPath(\"Root\")...");
            GenericTreeNode<string> node = tree.GetByPath("Root");
            if (node == null || node.ValueAsString() != "Root")
            {
                Console.WriteLine("Error");
                return false;
            }
            Console.WriteLine("OK");
            Console.Write("Checking GenericTree.GetByPath(\"Root->Child-1\")...");
            GenericTreeNode<string> child1 = tree.RootNode.Add("Child-1");
            node = tree.GetByPath("Root->Child-1");
            if (node == null || node.ValueAsString() != "Child-1") //Should find the node
            {
                Console.WriteLine("Error");
                return false;
            }
            Console.WriteLine("OK");
            Console.Write("Checking GenericTree.GetByPath(\"Root->Child-2\")...");
            node = tree.GetByPath("Root->Child-2");
            if (node != null) //Child-2 node hasn't been added yet, should return null
            {
                Console.WriteLine("Error");
                return false;
            }
            Console.WriteLine("OK");
            GenericTreeNode<string> child2 = tree.RootNode.Add("Child-2");
            Console.Write("Checking GenericTree.GetByPath(\"Root->Child-2\")...");
            node = tree.GetByPath("Root->Child-2");
            if (node == null || node.ValueAsString() != "Child-2") //Should find the node
            {
                Console.WriteLine("Error");
                return false;
            }
            Console.WriteLine("OK");
            GenericTreeNode<string> grandChild21 = child2.Add("Grandchild-2-1");
            Console.Write("Checking GenericTree.GetByPath(\"Root->Child-2->Grandchild-2-1\")...");
            node = tree.GetByPath("Root->Child-2->Grandchild-2-1");
            if (node == null || node.ValueAsString() != "Grandchild-2-1") //Should find the node
            {
                Console.WriteLine("Error");
                return false;
            }
            Console.WriteLine("OK");
            GenericTreeNode<string> grandChild22 = child2.Add("Grandchild-2-2");
            Console.Write("Checking GenericTree.GetByPath(\"Root->Child-2->Grandchild-2-2\")...");
            node = tree.GetByPath("Root->Child-2->Grandchild-2-2");
            if (node == null || node.ValueAsString() != "Grandchild-2-2") //Should find the node
            {
                Console.WriteLine("Error");
                return false;
            }
            Console.WriteLine("OK");
            Console.Write("Checking GenericTree.GetByPath(\"Root->Child-1->Grandchild-2-2\")...");
            node = tree.GetByPath("Root->Child-1->Grandchild-2-2");
            if (node != null) //Child-1 has no children, should return null
            {
                Console.WriteLine("Error");
                return false;
            }
            Console.WriteLine("OK");
            Console.Write("Checking GenericTree.GetByPath(\"Root->Grandchild-2-2\")...");
            node = tree.GetByPath("Root->Grandchild-2-2");
            if (node != null) //Grandchild-2-2 doesn't hang from Root node
            {
                Console.WriteLine("Error");
                return false;
            }
            Console.WriteLine("OK");

            Console.WriteLine("Basic tests passed. The method seems to work correctly...");
            return true;
        }
    }
}
