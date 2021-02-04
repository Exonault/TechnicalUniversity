using System;
using System.IO;
using System.Text;

namespace Exercise
{
    public class Files
    {
        public void Run()
        {
            //C:\Users\k.krachmarov\source\repos\TechnicalUniversity\Exercise\file.txt
            using (FileStream f = File.Open("file.txt", FileMode.Create))
            {
                Random r = new Random();

                for (int i = 0; i < 100000; i++)
                {
                    string line = r.Next() + Environment.NewLine;
                    byte[] bytes = Encoding.UTF8.GetBytes(line);
                    f.Write(bytes, 0, bytes.Length);
                }
            }

            int[] arr = new int[100000];
            using (StreamReader sr = File.OpenText("file.txt"))
            {
                int i = 0;
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    arr[i++] = int.Parse(line);
                }
            }

            for (int i = 0; i < arr.Length; i++)
            {
                Console.WriteLine(arr[i]);
            }
        }
    }
}