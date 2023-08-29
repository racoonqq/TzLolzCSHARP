using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TzConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Database.DbManager MyDB = new Database.DbManager("Games.db");
            MyDB.Init();
            DBMethods.Methods DbMethod = new DBMethods.Methods();

            Console.Write("Выберете действия из списка : \n");
            Console.WriteLine(" 1.Добавить игру");
            Console.WriteLine(" 2.Поиск игры");
            Console.WriteLine(" 3.Редактирование игры");
            Console.WriteLine(" 4.Удаление игры");
            Console.WriteLine(" 5.Все игры");
            int Variant = Convert.ToInt16(Console.ReadLine());

            switch (Variant)
            {
                case 1:
                    Console.WriteLine("Напишите через пробел данные (Имя, издатель, дата/год) от игры :");
                    string Data = Console.ReadLine();

                    DbMethod.ActionGame(Data + " I");
                    break;
                case 2:
                    Console.WriteLine("Напишите любой параметр по которому будет поиск");
                    string Param = Console.ReadLine();
                    DbMethod.FindGame(Param);

                    break;
                case 3:
                    Console.WriteLine("Напишите айди игры, которую нужно изменить : ");
                    int ID = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Напишите через пробел новые данные от игры : ");
                    string NewData = Console.ReadLine();
                    DbMethod.ActionGame(NewData + " " + ID);

                    break;
                case 4:
                    Console.WriteLine("Напишите айди игры которую нужно удалить :");
                    int Ajdi = Convert.ToInt32(Console.ReadLine());
                    DbMethod.DeleteGame(Ajdi);

                    break;
                case 5:
                    DbMethod.AllGames();

                    break;

            }
        }
    }
}
