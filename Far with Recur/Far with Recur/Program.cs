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
        // поле
        CustomFolderInfo prev;
        int index;
        DirectoryInfo[] dirs;

        //конструктор, получающий три аргумента
        public CustomFolderInfo(CustomFolderInfo prev, int index, DirectoryInfo[] directoryInfo)
        {
            this.prev = prev;
            this.index = index;
            this.dirs = directoryInfo;
        }

        //метод
        public void PrintFolderInfo()
        {
            Console.Clear();// очищает все лишнее

            for (int i = 0; i < dirs.Length; ++i)
            {
                //если курсор совпадает с текущим индексом - подсвечивает зеленым
                if (i == index)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                else //все остальное белым
                {
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.WriteLine(dirs[i]);
            }
        }

        //метод для уменьшения индекса при перемещении вверх
        public void DecreaseIndex()
        {
            if (index - 1 >= 0) index--; // чтобы не уйти за массив
        }

        //метод для увеличения индекса при перемещении вниз
        public void IncreaseIndex()
        {
            if (index + 1 < dirs.Length) index++;
        }

        //метод, отдающий новые данные
        public CustomFolderInfo GetNextItem()
        {
            return new CustomFolderInfo(this, 0, this.dirs[index].GetDirectories());
        }

        //метод, отдающий предыдущие данные
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

            //описывает нажатую клавишу с консоли
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
            /* создаем объект типа CustomFolderInfo класса и передаем свои параметры
             * предыдущий пути нет
             * передаем нулевой индекс
             * привязываем путь и возвращаем массив объектов с подкаталогами в текущем каталоге
            */
            CustomFolderInfo test = new CustomFolderInfo(null, 0, new DirectoryInfo(@"C:\").GetDirectories());
            ShowFolderInfo(test);
        }
    }
}