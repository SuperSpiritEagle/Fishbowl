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
        private List<Fish> pisces = new List<Fish>();
        private static Random _random = new Random();

        public Aquarium()
        {
            Create(5, pisces);
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
                    case "1":
                        AddFish();

                        break;

                    case "2":
                        RemoveFish();

                        break;

                    case "3":
                        lifeCycle();

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
            for (int i = 0; i < pisces.Count; i++)
            {
                Console.Write($"{i + 1} ");
                pisces[i].ShowInfo();
            }
        }

        private void RemoveFish()
        {
            Fish fish;

            ShowFish();
            Console.WriteLine("Введите номер рыбы");

            string userInput = Console.ReadLine();
            int.TryParse(userInput, out int index);

            fish = pisces[index - 1];
            pisces.Remove(fish);
        }

        private void lifeCycle()
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

            int.TryParse(userInput, out int count);
            isNumber(count);

            Create(count, pisces);
        }

        private bool isNumber(int count)
        {
            if (count <= 0)
            {
                Console.WriteLine("Ошибка! Введены не коректный данные. Повторите попытку.");
                return false;
            }
            else
            {
                return true;
            }
        }
    }

    abstract class Fish
    {
        private static Random _random = new Random();
        public Fish(int lifeExpectancy)
        {
            Age = _random.Next(20);
            LifeExpectancy = lifeExpectancy;
        }

        public int Age { get; protected set; }
        public bool IsDead => Age >= LifeExpectancy;
        public int LifeExpectancy { get; }

        public abstract Fish Clone();

        public void AddAge()
        {
            Age += _random.Next(10);
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
