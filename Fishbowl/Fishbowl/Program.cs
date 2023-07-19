using System;
using System.Collections.Generic;
using System.Threading;

namespace Fishbowl
{
    class Program
    {
        static void Main(string[] args)
        {
            Aquarium aquarium = new Aquarium();
            aquarium.Menu();
        }
    }

    class Aquarium
    {
        private static Random _random = new Random();
        private List<Fish> pisces = new List<Fish>();

        public Aquarium()
        {
            Create(3, pisces);
        }

        public void Menu()
        {
            const string CommandAddFish = "1";
            const string CommandRemoveFish = "2";
            const string CommandLifecycle = "3";
            const string CommandExit = "4";

            bool isExit = true;

            while (isExit)
            {
                Console.WriteLine();
                Console.WriteLine($"[{CommandAddFish}]: Добавить рыб");
                Console.WriteLine($"[{CommandRemoveFish}]: Достать рыбу из аквариума");
                Console.WriteLine($"[{CommandLifecycle}]: Посмтореть жизненный цикл");
                Console.WriteLine($"[{CommandExit}]: Выход из программы");

                string userInput = Console.ReadLine();
                Console.Clear();

                switch (userInput)
                {
                    case CommandAddFish:
                        AddFish();

                        break;

                    case CommandRemoveFish:
                        RemoveFish();

                        break;

                    case CommandLifecycle:
                        LifeCycle();

                        break;

                    case CommandExit:
                        isExit = false;

                        break;

                    default:
                        Console.WriteLine("There is no command");

                        break;
                }
            }
        }

        private void ShowFish()
        {
            int shift = 1;

            for (int i = 0; i < pisces.Count; i++)
            {
                Console.Write($"{i + shift} ");
                pisces[i].ShowInfo();
            }
        }

        private void RemoveFish()
        {
            ShowFish();

            Console.WriteLine("Введите номер рыбы");
            string userInput = Console.ReadLine();

            if (int.TryParse(userInput, out int index))
            {
                TryGetFish(out Fish fish, index);
                pisces.Remove(fish);
            }
            else
            {
                Console.WriteLine("Error");
            }
        }

        private bool TryGetFish(out Fish fish, int index)
        {
            if (index <= 0 || index > pisces.Count)
            {
                Console.WriteLine("fish not found");
                fish = null;
                return false;
            }
            else
            {
                fish = pisces[index - 1];
                return true;
            }
        }

        private void LifeCycle()
        {
            ShowFish();

            for (int i = 0; i < pisces.Count; i++)
            {
                pisces[i].AddAge();
                Destroy(i);
            }
        }

        private void Destroy(int index)
        {
            if (pisces[index].IsDead)
            {
                Console.WriteLine($"{pisces[index].GetType().Name} Отправился на небеса. Возраст :{pisces[index].Age}");
                pisces.Remove(pisces[index]);
            }
        }

        private void Create(int numberOfFish, List<Fish> fish)
        {
            Fish[] fishs = { new Dolphin(0), new Shark(0), new Whale(0) };

            for (int i = 0; i < numberOfFish; i++)
            {
                fish.Add(fishs[_random.Next(fishs.Length)].Clone());
            }
        }

        private void AddFish()
        {
            Console.WriteLine("Введите количество рыб");
            string userInput = Console.ReadLine();

            if (int.TryParse(userInput, out int count))
            {
                Create(count, pisces);
            }
            else
            {
                Console.WriteLine("Error");
            }
        }
    }

    abstract class Fish
    {
        private static Random _random = new Random();
        private int _age = 20;
        private int _addAge = 10;

        public Fish(int lifeExpectancy)
        {
            Age = _random.Next(_age);
            LifeExpectancy = lifeExpectancy;
        }

        public int Age { get; protected set; }
        public int LifeExpectancy { get; }
        public bool IsDead => Age >= LifeExpectancy;

        public abstract Fish Clone();

        public void AddAge()
        {
            Age += _random.Next(_addAge);
        }

        public void ShowInfo()
        {
            Console.WriteLine($"{GetType().Name} , Возраст : {Age} , средняя продолжительность жизни : {LifeExpectancy} лет");
        }
    }

    class Dolphin : Fish
    {
        public Dolphin(int lifeExpectancy) : base(lifeExpectancy) { }

        public override Fish Clone()
        {
            return new Dolphin(30);
        }
    }

    class Shark : Fish
    {
        public Shark(int lifeExpectancy) : base(lifeExpectancy) { }

        public override Fish Clone()
        {
            return new Shark(30);
        }
    }

    class Whale : Fish
    {
        public Whale(int lifeExpectancy) : base(lifeExpectancy) { }

        public override Fish Clone()
        {
            return new Whale(30);
        }
    }
}
