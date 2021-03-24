using System;
using System.Collections.Generic;
using System.Threading;

namespace Threads
{
    public static class BreadMakerThreads
    {
        private static Random _random = new Random();
        private const int Capacity = 5;
        private static Queue<int> rack = new Queue<int>(Capacity);

        private static Semaphore _semEmpty = new Semaphore(0,Capacity);
        private static Semaphore _semFull = new Semaphore(Capacity,Capacity);
        
        public static void Run()
        {
            var breadMakerThread = new Thread(BreadMaker);
            var breadMakerClient1 = new Thread(Client);
            var breadMakerClient2 = new Thread(Client);
            var breadMakerClient3 = new Thread(Client);

            breadMakerThread.Start();
            breadMakerClient1.Start();
            breadMakerClient2.Start();
            breadMakerClient3.Start();
        }

        public static void BreadMaker()
        {
            while (true)
            {
                _semFull.WaitOne();
                
                Thread.Sleep(_random.Next(1000, 2000));
                Console.WriteLine($"Making bread {rack.Count + 1}");
                lock (_random)
                {
                    if (rack.Count < Capacity)
                    {
                        rack.Enqueue(_random.Next());
                    }

                    _semEmpty.Release();
                }
            }
        }

        public static void Client()
        {
            while (true)
            {
                _semEmpty.WaitOne();
                
                Thread.Sleep(_random.Next(1000, 2000));
                Console.WriteLine($"Buying bread {rack.Count}");
                lock (_random)
                {
                    if (rack.Count > 0)
                    {
                        rack.Dequeue();
                    }

                    _semFull.Release();
                }
            }
        }
    }
}