using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarFiles
{
    class CustomFolderInfo
    {
        CustomFolderInfo prev;
        int index;
        FileSystemInfo[] objs;

        public CustomFolderInfo(CustomFolderInfo prev, int index, FileSystemInfo[] list)
        {
            this.prev = prev;
            this.index = index;
            this.objs = list;
        }

        public void PrintFolderInfo()
        {
            Console.Clear();

            for (int i = 0; i < objs.Length; ++i)
            {
                if (i == index)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.WriteLine(objs[i]);
            }
        }

        public void DecreaseIndex()
        {
            if (index - 1 >= 0) index--;
        }

        public void IncreaseIndex()
        {
            if (index + 1 < objs.Length) index++;
        }

        public CustomFolderInfo GetNextItem()
        {
            FileSystemInfo active = objs[index];

            if (active.GetType() == typeof(DirectoryInfo))
            {
                List<FileSystemInfo> list = new List<FileSystemInfo>();

                DirectoryInfo d = (DirectoryInfo)active;

                list.AddRange(d.GetDirectories());
                list.AddRange(d.GetFiles());

                CustomFolderInfo x = new CustomFolderInfo(this, 0, list.ToArray());
                return x;
            }
            else
            {
                Console.Clear();
                FileStream fs = new FileStream(active.FullName, FileMode.Open, FileAccess.Read);
                StreamReader sr = new StreamReader(fs);
                Console.WriteLine(sr.ReadToEnd());

                // бесконечный цикл, выводит на экран содержимое до нажатия выхода
                while (true)
                {
                    if (Console.ReadKey().Key.Equals(ConsoleKey.Escape))
                        break;
                    Console.Clear();
                    Console.WriteLine(sr.ReadToEnd());
                }

                sr.Close();
                fs.Close();

                return this;
            }

           return null;
        }

        public CustomFolderInfo GetPrevItem()
        {
            return prev;
        }
    }

    class Program
    {
        static void ShowFolderInfo(CustomFolderInfo item)
        {
            item.PrintFolderInfo();

            ConsoleKeyInfo pressedKey = Console.ReadKey();

            if (pressedKey.Key == ConsoleKey.UpArrow)
            {
                item.DecreaseIndex();
                ShowFolderInfo(item);
            }
            else if (pressedKey.Key == ConsoleKey.DownArrow)
            {
                item.IncreaseIndex();
                ShowFolderInfo(item);
            }
            else if (pressedKey.Key == ConsoleKey.Enter)
            {
                CustomFolderInfo newItem = item.GetNextItem();
                ShowFolderInfo(newItem);
            }
            else if (pressedKey.Key == ConsoleKey.Escape)
            {
                CustomFolderInfo newItem = item.GetPrevItem();
                ShowFolderInfo(newItem);
            }
        }

        static void Main(string[] args)
        {
            // создаем вектор(массив)
            List<FileSystemInfo> list = new List<FileSystemInfo>();

            //прописываем путь
            var d = new DirectoryInfo(@"C:\Users\Admin\Source\Repos");
            list.AddRange(d.GetDirectories());
            list.AddRange(d.GetFiles());

            CustomFolderInfo test = new CustomFolderInfo(null, 0, list.ToArray());
            ShowFolderInfo(test);
        }
    }
}
