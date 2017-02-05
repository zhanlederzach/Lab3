using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example1
{
    class State
    {  
        private int index; 
        
        public int Index //свойства индекса
        {
            get { return index; } 

            set 

            {//обрабатываем исключение, чтобы он не вышел за предел массива
                int maxVal = Folder.GetDirectories().Length;
                if (value >= 0 && value < maxVal)
                {
                    index = value;
                }
            }
        }

        public DirectoryInfo Folder { get; set; } 
    }

    class Program
    {
        static void ShowFolderContent(State state)
        {
            Console.Clear();//очищает все лишнее

            DirectoryInfo[] list = state.Folder.GetDirectories(); //массив папок текущего состояния

            for (int i = 0; i < list.Length; ++i)
            {
                if (state.Index == i) // подсвечивает, если индекс совпадает с курсором
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                }

                Console.Write(list[i]);
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();
            }
 
            foreach (FileInfo f in state.Folder.GetFiles())
            {
                Console.WriteLine(f.Name);
            }
        }

        static void Main(string[] args)
        {
            bool alive = true;

            //создаем объект класса state с двумя параметрами
            State state = new State { Folder = new DirectoryInfo(@"D:\"), Index = 0 };
           
            Stack<State> layers = new Stack<State>(); //создаем stack

            layers.Push(state); //закидываем наше текущее состояние в первый слой стэка
            while (alive)
            {
                ShowFolderContent(layers.Peek());// показывает самый первый элемент

                ConsoleKeyInfo pressedKey = Console.ReadKey();// работа с набором клавиш консоля
                switch (pressedKey.Key)
                {
                    case ConsoleKey.UpArrow:
                        layers.Peek().Index--;
                        break;
                    case ConsoleKey.DownArrow:
                        layers.Peek().Index++;
                        break;
                    case ConsoleKey.Escape:
                        layers.Pop(); 
                        break;
                    case ConsoleKey.Enter:
                        //создает новый путь
                        DirectoryInfo[] list = layers.Peek().Folder.GetDirectories();

                        //создает новый state
                        State newState = new State
                        {
                            // выдает название новой папки
                            Folder = new DirectoryInfo(list[state.Index].FullName),

                            // курсор на первый элемент
                            Index = 0
                        };
                        layers.Push(newState);// закидывает новое состояние в следующий слой
                        break;
                }

            }
        }
    }
}