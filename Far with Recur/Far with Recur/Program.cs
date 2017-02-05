using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Far_with_recur
{
    class CustomFolderInfo
    {
        CustomFolderInfo prev;
        int index;
        DirectoryInfo[] dirs;

        public CustomFolderInfo(CustomFolderInfo prev, int index, DirectoryInfo[] directoryInfo)
        {
            this.prev = prev;
            this.index = index;
            this.dirs = directoryInfo;
        }
        public void PrintFolderInfo()
        {
            Console.Clear();

            for (int i = 0; i < dirs.Length; ++i)
            {
                if (i == index)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.WriteLine(dirs[i]);
            }
        }

        public void DecreaseIndex()
        {
            if (index - 1 >= 0) index--;
        }

        public void IncreaseIndex()
        {
            if (index + 1 < dirs.Length) index++;
        }

        public CustomFolderInfo GetNextItem()
        {
            return new CustomFolderInfo(this, 0, this.dirs[index].GetDirectories());
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
            CustomFolderInfo test = new CustomFolderInfo(null, 0, new DirectoryInfo(@"C:\").GetDirectories());
            ShowFolderInfo(test);
        }
    }
}