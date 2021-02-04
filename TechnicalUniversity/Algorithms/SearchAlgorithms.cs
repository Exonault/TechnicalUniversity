using System.Security.Cryptography;

namespace Algorithms
{
    public class SearchAlgorithms
    {
        public int LinearSearch(int[] arr, int search)
        {
            int n = arr.Length;

            for (int i = 0; i < n; i++)
            {
                if (arr[i] == search)
                {
                    return i;
                }
            }

            return -1;
        }


        public int BinarySearchRecursive(int[] arr, int beginning, int end, int search)
        {
            if (end > beginning)
            {
                int mid = beginning + (end - beginning) / 2;

                if (arr[mid] == search)
                {
                    return search;
                }

                if (arr[mid] > search)
                {
                    return BinarySearchRecursive(arr, beginning, mid - 1, search);
                }
                
                else return BinarySearchRecursive(arr, mid + 1, end, search);
                
            }

            return -1;
        }

        public int BinarySearchIterative(int[] arr, int search)
        {
            int beginning = 0;
            int end = arr.Length - 1;
            while (beginning <= end)
            {
                int mid = beginning + (end - beginning) / 2;
                if (arr[mid] == search)
                {
                    return mid;
                }

                if (arr[mid] < search)
                {
                    beginning = mid + 1;
                }

                else end = mid - 1;
            }

            return -1;
        }
    }
}