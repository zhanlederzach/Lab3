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
        public int Index
        {
            get { return index; }
            set
            {
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
            Console.Clear();
            DirectoryInfo[] list = state.Folder.GetDirectories();
            for (int i = 0; i < list.Length; ++i)
            {
                if (state.Index == i)
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
            State state = new State { Folder = new DirectoryInfo(@"D:\"), Index = 0 };
            Stack<State> layers = new Stack<State>();
            layers.Push(state);
            while (alive)
            {
                ShowFolderContent(layers.Peek());
                ConsoleKeyInfo pressedKey = Console.ReadKey();
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
                        DirectoryInfo[] list = layers.Peek().Folder.GetDirectories();
                        State newState = new State
                        {
                            Folder = new DirectoryInfo(list[state.Index].FullName),
                            Index = 0
                        };
                        layers.Push(newState);
                        break;
                }

            }
        }
    }
}