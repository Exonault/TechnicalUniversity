using System;
using System.Threading;

namespace Threads
{
    public class FindMaxThreads
    {
        public void Run()
        {
            Random r = new Random();
            int[] arr = new int[1000000];

            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = r.Next();
            }

            var t1 = DateTime.Now;

            int max = FindMax(arr);

            var t2 = DateTime.Now;

            Console.WriteLine($"{max} without threads found for {(t2 - t1).Milliseconds}");

            for (int i = 2; i < 16; i++)
            {
                t1 = DateTime.Now;

                int maxP = FindMaxParallel(arr, i);

                t2 = DateTime.Now;

                Console.WriteLine($"Max: {maxP} with {i} threads for {(t2 - t1).Milliseconds}");
            }
        }


        public static int FindMax(int[] arr, int from = 0, int to = Int32.MaxValue)
        {
            int max = arr[0];
            for (int i = from + 1; i < Math.Min(to, arr.Length); i++)
            {
                for (int j = 0; j < 1000; j++) ;

                if (arr[i] > max)
                {
                    max = arr[i];
                }
            }


            return max;
        }

        public static int FindMaxParallel(int[] arr, int threadsCount)
        {
            Thread[] threads = new Thread[threadsCount];
            ThreadParam[] threadParams = new ThreadParam[threadsCount];
            for (int i = 0; i < threadsCount; i++)
            {
                threads[i] = new Thread(ThreadJob);

                threadParams[i] = new ThreadParam()
                {
                    arr = arr,
                    threadNum = i,
                    threadsCounts = threadsCount
                };

                threads[i].Start(threadParams[i]);
            }

            int[] threadResult = new int[threadsCount];

            for (int i = 0; i < threadsCount; i++)
            {
                threads[i].Join();
                threadResult[i] = threadParams[i].max;
            }

            return FindMax(arr);
        }

        public static void ThreadJob(object obj)
        {
            ThreadParam param = (ThreadParam) obj;

            var size = param.arr.Length / param.threadsCounts;


            int from = param.threadNum * size;

            int to = param.threadNum * size + size;

            if (param.threadNum == param.threadsCounts - 1)
            {
                to += param.arr.Length % param.threadsCounts;
            }

            param.max = FindMax(param.arr, from, to);
        }
    }

    public struct ThreadParam
    {
        public int[] arr;
        public int threadNum;
        public int threadsCounts;
        public int max;
    }
}