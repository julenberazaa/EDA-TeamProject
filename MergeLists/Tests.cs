using Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MergeLists
{
    public class Tests
    {
        public static bool RunBasicTests()
        {/*
            GenericList<int> list1 = new GenericList<int>();
            GenericList<int> list2 = new GenericList<int>();
            GenericList<int> merged;
            Console.Write("Merging {} with {}...");
            merged = GenericList<int>.Merge(list1, list2);
            if (!list1.Equals(list1))
            {
                Console.WriteLine("Error");
                return false;
            }
            Console.WriteLine("OK");
            list1.Add(5);
            Console.Write("Comparing {5} with {5}...");
            if (!list1.Equals(list1))
            {
                Console.WriteLine("Error");
                return false;
            }
            Console.WriteLine("OK");
            list1.Add(7);
            Console.Write("Comparing {5,7} with {5,7}...");
            if (!list1.Equals(list1))
            {
                Console.WriteLine("Error");
                return false;
            }
            Console.WriteLine("OK");
            list1.Add(2);
            Console.Write("Comparing {5,7,2} with {5,7,2}...");
            if (!list1.Equals(list1))
            {
                Console.WriteLine("Error");
                return false;
            }
            Console.WriteLine("OK");
            GenericList<int> list2 = new GenericList<int>();
            Console.Write("Comparing {5,7,2} with {}...");
            if (list1.Equals(list2))
            {
                Console.WriteLine("Error");
                return false;
            }
            Console.WriteLine("OK");
            Console.Write("Comparing {5,7,2} with {7}...");
            list2.Add(7);
            if (list1.Equals(list2))
            {
                Console.WriteLine("Error");
                return false;
            }
            Console.WriteLine("OK");
            Console.Write("Comparing {5,7,2} with {7,5}...");
            list2.Add(5);
            if (list1.Equals(list2))
            {
                Console.WriteLine("Error");
                return false;
            }
            Console.WriteLine("OK");
            Console.Write("Comparing {5,7,2} with {7,5,5}...");
            list2.Add(5);
            if (list1.Equals(list2))
            {
                Console.WriteLine("Error");
                return false;
            }
            Console.WriteLine("OK");
            Console.Write("Comparing {5,7,2} with {7,5,5,2,1}...");
            list2.Add(2);
            list2.Add(1);
            if (list1.Equals(list2))
            {
                Console.WriteLine("Error");
                return false;
            }
            Console.WriteLine("OK");
            Console.Write("Comparing {7,5,5,2,1} with {5,7,2}...");
            if (list2.Equals(list1))
            {
                Console.WriteLine("Error");
                return false;
            }
            Console.WriteLine("OK");
            list1 = new GenericList<int>();
            list1.Add(7);
            list1.Add(5);
            list1.Add(5);
            list2 = new GenericList<int>();
            list2.Add(5);
            list2.Add(7);
            list2.Add(7);
            Console.Write("Comparing {7,5,5} with {5,7,7}...");
            if (list2.Equals(list1))
            {
                Console.WriteLine("Error");
                return false;
            }
            Console.WriteLine("OK");
            Console.WriteLine("Basic tests passed. The method seems to work correctly...");*/
            return true;
        }

        class TestClass: IEquatable<TestClass>, IComparable<TestClass>
        {
            int Number;
            public TestClass(int number)
            {
                Number = number;
            }

            public int CompareTo(TestClass other)
            {
                return Number.CompareTo(other.Number);
            }

            public bool Equals(TestClass other)
            {
                return Number == other.Number;
            }

            public override bool Equals(object obj)
            {
                TestClass other = obj as TestClass;
                if (other == null)
                    return false;
                return Equals(other);
            }
        }

        public static SpeedMeasure MeasureSpeed()
        {
            Console.WriteLine("Preparing test data...");
            int numSamples = 1000000;
            int[] array = new int[numSamples];
            Random random = new Random();
            for (int i= 0; i< numSamples; i++)
            {
                array[i] = random.Next();
            }
            
            GenericList<TestClass> list1 = new GenericList<TestClass>();
            random.Shuffle(array);
            for (int i = 0; i < numSamples; i++)
                list1.Add(new TestClass(array[i]));

            GenericList<TestClass> list2 = new GenericList<TestClass>();
            random.Shuffle(array);
            for (int i = 0; i < numSamples; i++)
                list2.Add(new TestClass(array[i]));

            Console.WriteLine($"Running GenericList<T>.Equals with lists of size {numSamples}...");
            Stopwatch stopwatch = Stopwatch.StartNew();

            bool equal = list1.Equals(list2);
            if (!equal)
                return new SpeedMeasure() { Success = false };

            return new SpeedMeasure() { Success = true, Time = stopwatch.Elapsed.TotalSeconds };
        }
    }
    static class RandomExtensions
    {
        public static void Shuffle<T>(this Random rng, T[] array)
        {
            int n = array.Length;
            while (n > 1)
            {
                int k = rng.Next(n--);
                T temp = array[n];
                array[n] = array[k];
                array[k] = temp;
            }
        }
    }
}
