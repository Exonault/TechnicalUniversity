using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Threads
{
    public class MergeSortThreads
    {
        public void Run()
        {
            int n = 11;
            int[] arr = new int[n];
            Random random = new Random();
            for (int i = 0; i < n; i++)
            {
                arr[i] = random.Next() % 100;
            }

            DateTime t1 = DateTime.Now;

            Sort(arr, 0, n - 1);

            DateTime t2 = DateTime.Now;

            Console.WriteLine($"{string.Join(", ", arr)} sorted without threads for {(t2 - t1).TotalSeconds}");
            
            List<int> list = new List<int>();

            for (int i = 0; i < n; i++)
            {
                list.Add(random.Next() % 100);
            }

            t1 = DateTime.Now;

            List<int> list1 = Sort(list);

            t2 = DateTime.Now;

            Console.WriteLine($"{string.Join(", ", list1)} sorted with threads for {(t2 - t1).TotalSeconds}");
        }

        public static List<int> Sort(List<int> toSort, bool ascending = true, int threashold = 100)
        {
            if (toSort.Count == 1)
            {
                return toSort;
            }

            var mid = toSort.Count / 2;

            List<int> left = new List<int>();
            List<int> right = new List<int>();

            if (toSort.Count > threashold)
            {
                Task t = Task.Run(() => { left = Callback(toSort, mid, true, ascending); });
                Task t2 = Task.Run(() => { right = Callback(toSort, mid, false, ascending); });
                t.Wait();
                t2.Wait();
            }
            else
            {
                left = Callback(toSort, mid, true, ascending);
                right = Callback(toSort, mid, false, ascending);
            }

            return Merge(left, right, ascending);
        }

        public static List<int> Callback(List<int> mainList, int mid, bool left, bool ascending)
        {
            List<int> toSort = new List<int>();

            if (left)
            {
                for (var i = 0; i < mid; i++)
                {
                    toSort.Add(mainList[i]);
                }
            }
            else
            {
                for (var i = mid; i < mainList.Count; i++)
                {
                    toSort.Add(mainList[i]);
                }
            }

            return Sort(toSort, ascending);
        }

        public static List<int> Merge(List<int> left, List<int> right, bool ascending)
        {
            var result = new List<int>();

            while (left.Count > 0 || right.Count > 0)
            {
                if (left.Count > 0 && right.Count > 0)
                {
                    if (ascending)
                    {
                        if (left[0].CompareTo(right[0]) <= 0)
                        {
                            result.Add(left[0]);
                            left.Remove(left[0]);
                        }
                        else
                        {
                            result.Add(right[0]);
                            right.Remove(right[0]);
                        }
                    }
                    else
                    {
                        if (left[0].CompareTo(right[0]) >= 0)
                        {
                            result.Add(left[0]);
                            left.Remove(left[0]);
                        }
                        else
                        {
                            result.Add(right[0]);
                            right.Remove(right[0]);
                        }
                    }
                }

                else if (left.Count > 0)
                {
                    foreach (var item in left)
                    {
                        result.Add(item);
                    }

                    left = new List<int>();
                }

                else
                {
                    foreach (var item in right)
                    {
                        result.Add(item);
                    }

                    right = new List<int>();
                }
            }

            return result;
        }


        private void Merge(int[] arr, int left, int middle, int right)
        {
            int n1 = middle - left + 1;
            int n2 = right - middle;


            int[] leftArr = new int[n1];
            int[] rightArr = new int[n2];
            int i, j;


            for (i = 0; i < n1; i++)
            {
                leftArr[i] = arr[left + i];
            }

            for (j = 0; j < n2; j++)
            {
                rightArr[j] = arr[middle + 1 + j];
            }


            i = 0;
            j = 0;


            int k = left;
            while (i < n1 && j < n2)
            {
                if (leftArr[i] <= rightArr[j])
                {
                    arr[k] = leftArr[i];
                    i++;
                }
                else
                {
                    arr[k] = rightArr[j];
                    j++;
                }

                k++;
            }


            while (i < n1)
            {
                arr[k] = leftArr[i];
                i++;
                k++;
            }


            while (j < n2)
            {
                arr[k] = rightArr[j];
                j++;
                k++;
            }
        }

        private void Sort(int[] arr, int left, int right)
        {
            if (left < right)
            {
                int middle = left + (right - left) / 2;

                Sort(arr, left, middle);
                Sort(arr, middle + 1, right);

                Merge(arr, left, middle, right);
            }
        }
    }
}