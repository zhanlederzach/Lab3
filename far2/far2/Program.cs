using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace far2
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = "D:";

            string[] lastpath = new string[99];

            lastpath[0] = path; //начальный путь;
            int indexOfLast = 0; // сохраненные пути на которые будем ссылаться 
            int index = 0, last = 0; // индекс положения курсора

            int[] lastIndex = new int[99]; // последнее положение курсора

            DirectoryInfo dir = new DirectoryInfo(path); // получаем содержимое папки

            List<FileSystemInfo> items = new List<FileSystemInfo>(); // создаем вектор(массив) для свойств папок
            items.AddRange(dir.GetDirectories()); // сортируем папки
            items.AddRange(dir.GetFiles()); // сортируем файлы

            while (true)
            {
                for (int i = 0; i < items.Count; ++i)
                {
                    if (i == index) // если положение курсора совпадает с i индексом
                    {
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }
                    if (items[i].GetType() == typeof(FileInfo)) // если тип i элемента файл
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }

                    Console.WriteLine(items[i].Name); // название элемента
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                }


                ConsoleKeyInfo pressedKey = Console.ReadKey();
                switch (pressedKey.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (index > 0) index--;
                        break;
                    case ConsoleKey.DownArrow:
                        if (index < items.Count - 1) index++;
                        break;
                    case ConsoleKey.Enter:
                        if (items[index].GetType() == typeof(DirectoryInfo)) // если i элемент папка
                        {

                            lastIndex[last] = index;  // фиксируем положение "курсора"

                            indexOfLast++; // переход к следующей папке 
                            path = items[index].FullName; // путь меняется
                            lastpath[indexOfLast] = path; // сохраняем новый путь
                            dir = new DirectoryInfo(path);
                            items.Clear();
                            items.AddRange(dir.GetDirectories());
                            items.AddRange(dir.GetFiles());
                            index = 0; // положение курсора в новой папке 
                            last++; // новое положение 

                        }
                        break;
                    case ConsoleKey.Escape:
                        if (indexOfLast != 0)
                        {
                            indexOfLast--;
                            path = lastpath[indexOfLast]; // предыдущий путь
                            dir = new DirectoryInfo(lastpath[indexOfLast]);
                            items.Clear();
                            items.AddRange(dir.GetDirectories());
                            items.AddRange(dir.GetFiles());
                            last--; // возвращаемся к предыдущему положению
                            index = lastIndex[last]; // предыдущее положение                             

                        }
                        break;
                }
                Console.Clear();
            }
        }
    }
}