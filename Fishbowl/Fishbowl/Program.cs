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
        private List<Fish> _fish = new List<Fish>();

        public Aquarium()
        {
            Create(3, _fish);
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
                Console.WriteLine($"[{CommandAddFish}] Добавить рыб");
                Console.WriteLine($"[{CommandRemoveFish}] Достать рыбу из аквариума");
                Console.WriteLine($"[{CommandLifecycle}] Посмтореть жизненный цикл");
                Console.WriteLine($"[{CommandExit}] Выход из программы");

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

        private void ShowFishs()
        {
            int shift = 1;

            for (int i = 0; i < _fish.Count; i++)
            {
                Console.Write($"{i + shift} ");
                _fish[i].ShowInfo();
            }
        }

        private void RemoveFish()
        {
            ShowFishs();

            Console.WriteLine("Введите номер рыбы");
            string userInput = Console.ReadLine();

            if (int.TryParse(userInput, out int index))
            {
                if (TryGetFish(out Fish fish, index))
                {
                    Console.WriteLine($"{fish.GetType().Name} покинул аквариум");
                    _fish.Remove(fish);
                }
            }
            else
            {
                Console.WriteLine("Error");
            }
        }

        private bool TryGetFish(out Fish fish, int index)
        {
            if (index <= 0 || index > _fish.Count)
            {
                Console.WriteLine("fish not found");
                fish = null;
                return false;
            }
            else
            {
                fish = _fish[index - 1];
                return true;
            }
        }

        private void LifeCycle()
        {
            ShowFishs();

            for (int i = 0; i < _fish.Count; i++)
            {
                if (_fish[i].IsDead)
                {
                    Console.WriteLine($"{_fish[i].GetType().Name} Отправился на небеса. Возраст: {_fish[i].Age}");
                    _fish.Remove(_fish[i]);
                }
                else
                {
                    _fish[i].AddAge();
                }
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
                Create(count, _fish);
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
            Console.WriteLine($"{GetType().Name}, Возраст: {Age}, средняя продолжительность жизни: {LifeExpectancy} лет");
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
