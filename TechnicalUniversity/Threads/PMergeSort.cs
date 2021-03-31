using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Threads
{
    public static class PMergeSort<T> where T : IComparable
    {
        public static List<T> Sort(List<T> toSort, bool ascending = true, int threashold = 100)
        {
            if (toSort.Count == 1)
            {
                return toSort;
            }

            var mid = toSort.Count / 2;

            List<T> left = new List<T>();
            List<T> right = new List<T>();

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

        //private helper method
        //the main function for this method is to separate the list form given mid point and rescursively call Sort method
        public static List<T> Callback(List<T> mainList, int mid, bool left, bool ascending)
        {
            List<T> toSort = new List<T>();

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

        public static List<T> Merge(List<T> left, List<T> right, bool ascending)
        {
            var result = new List<T>();

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

                    left = new List<T>();
                }

                else
                {
                    foreach (var item in right)
                    {
                        result.Add(item);
                    }

                    right = new List<T>();
                }
            }

            return result;
        }
    }
}