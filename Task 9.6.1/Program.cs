using System;

namespace Task_9._6._1
{
    static class Program
    {
        public static int WriteRead(string message)
        {
            Console.WriteLine(message);
            return Convert.ToInt32(Console.ReadLine());
        }
        static void Main(string[] args)
        {
            Random rnd = new Random();
            Exception[] exceptions = new Exception[5];
            exceptions[0] = new NameExeption();
            exceptions[1] = new FormatException();
            exceptions[2] = new ArgumentOutOfRangeException();
            exceptions[3] = new OverflowException();
            exceptions[4] = new ArgumentException();
            Character cha;
            
            try
            {
                Console.WriteLine("Введите ваше имя");
                string name = Console.ReadLine();
                Console.WriteLine($"Привет {name}! Выбери расу\n0 — человек\n1 — эльф\n2 - орк");
                int race = Convert.ToInt32(Console.ReadLine());
                if (race > 2 || race < 0)
                    throw new FormatException("Такой расы нет");

                cha = new Character(name, race);
                Character enemy = new Character("enemy", rnd.Next(0, 3));
                while (enemy.HP > 0 && cha.HP > 0)
                {
                    cha.Attack(enemy, Program.WriteRead("Введите место атаки\n0 — голова\n1 — пах\n2 - ноги"), rnd.Next(0,3));
                    enemy.Attack(cha, rnd.Next(0, 3), Program.WriteRead("Введите место защиты\n0 — голова\n1 — пах\n2 — ноги"));
                }

                if (cha.HP > 0)
                   Console.WriteLine($"Молодец {name}, ты победил!!\nВ качестве награды вот тебе массив с исключениями:");
                

                else Console.WriteLine($"{name} тебя охерачили!\nНо не расстрывайся. Посмотри лучше на массив с исключениями:");

                foreach (Exception ex in exceptions)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            catch (NameExeption e)
            {

                Console.WriteLine(e.Message);
                Console.WriteLine($" Ошибка произошла по вине введеного имени, смените пожалуйста: {e.Value}");
            }
            catch (FormatException e)
            {

                Console.WriteLine(e.Message);
                Console.WriteLine($"Не верный формат данных");
            }
            catch (ArgumentOutOfRangeException e)
            {

                Console.WriteLine(e.Message);
                Console.WriteLine($"Вы ничего не написали");
            }
            catch (OverflowException e)
            {

                Console.WriteLine(e.Message);
                Console.WriteLine($"Вы ввели слишком большое число");
            }
            catch (ArgumentException e)
            {

                Console.WriteLine(e.Message);
                Console.WriteLine($"Непустой аргумент, передаваемый в метод, является недопустимым");
            }
        }
    }
    

    class NameExeption: ArgumentException
    {
        public string Value { get; }
        public NameExeption(string message, string val)
            : base(message)
        {
            Value = val;
        }
        public NameExeption()
        {

        }
    }
    public class Character
    {
        string Name { get; set; }
        string Race { get;  }
        int Level { get; }
        int Experience { get; }
        int DamageMin { get;  }
        int DamageMax { get;  }
        int Defense { get;  }
        public int HP { get; set; }
        string[] Purpose = new string[3];
        public Character (string name, int race)
        {
            switch (race)
            {
                case 0: Race = "Человек";
                    DamageMax = 4;
                    DamageMin = 1;
                    Defense = 1;
                    HP = 30;
                    break;
                case 1: Race = "Эльф";
                    DamageMax = 3;
                    DamageMin = 3;
                    Defense = 2;
                    HP = 25;
                    break;
                case 2:
                    Race = "Орк";
                    DamageMax = 5;
                    DamageMin = 0;
                    Defense = 0;
                    HP = 50;
                    break;
                default:
                    break;
            }
            Level = 1;
            Experience = 0;
            if (name.Length > 20)
                throw new NameExeption("Длина имени должна быть менее 20", name);
            Name = name;
            Purpose = new string[] { "Голову", "Пах", "Ноги"};
        }
        
        public void Attack(Character enemy, int mark, int def)
        {
            Random rnd = new Random();
            if (mark > 2)
                mark = 2;
            if (def > 2)
                def = 2;
            if (mark == def)
            {
                Console.WriteLine($"{enemy.Name} защитил атаку в {Purpose[mark]}");
            }
            else
            {
                int dam = rnd.Next(DamageMin, DamageMax + 1);
                enemy.HP -= dam - enemy.Defense;
                Console.WriteLine($"{Name} атаковал {enemy.Name} в {Purpose[mark]} и нанес {dam - enemy.Defense} урона");
            }
            Console.WriteLine($"{enemy.Name} НР: {enemy.HP}\n{Name} HP: {HP}");
        }
    }
}
