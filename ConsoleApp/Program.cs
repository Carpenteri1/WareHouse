using System;
using Warehouse;
/*
 *Author: Niclas Timle
 *Date: 01-08-2020
 */
namespace ConsoleApp
{
    class Program
    {
        private string input;
        private Orgenizer orgenizer = new Orgenizer();
        private bool IsRunning = true;
        private bool backUpLoaded = true;      

        public Program()
        {
            orgenizer.Storage = BackUp.LoadDataFromBin();
            while (IsRunning)
            {
                Console.Clear();
                Console.Write("Warehouse Database\n" +
                    "\nPress 0 : Add new product " +
                    "\nPress 1 : Search for a product" +
                    "\nPress 2 : Remove a product" +//checks out a product
                    "\nPress 3 : MoveProduct" +
                    "\nPress 4 : Set insurance for product" +
                    "\nPress 5 : Show Content in a box" +//checks whats inside a level and slot positon
                    "\nPress 6 : Show All Content" +
                    "\nPress 7 : Load test file" +
                    "\nPress 8 : Load backup file" +
                    "\nPress 9 : Close program" +
                    "\nEnter value:> ");
                input = Console.ReadLine();
                switch (input)
                {
                    case "0":
                        orgenizer.AddProduct();
                        orgenizer.SaveData(backUpLoaded);
                        break;
                    case "1":
                        orgenizer.SearchForProduct();
                        orgenizer.SaveData(backUpLoaded);
                        break;
                    case "2":
                        orgenizer.RemoveProduct();
                        orgenizer.SaveData(backUpLoaded);
                        break;
                    case "3":
                        orgenizer.MoveProduct();
                        orgenizer.SaveData(backUpLoaded);
                        break;
                    case "4":
                        orgenizer.SetInsuresValue();
                        orgenizer.SaveData(backUpLoaded);
                        break;
                    case "5":
                        orgenizer.GetContent();
                        break;
                    case "6":
                        Console.Clear();
                        orgenizer.GetAllContent();
                        break;
                    case "7":
                        orgenizer.LoadTestData();
                        backUpLoaded = false;
                        break;
                    case "8":
                        orgenizer.LoadDataFromBackUp();
                        backUpLoaded = true;
                        break;
                    case "9":
                        IsRunning = false;
                        break;
                    default:
                        Console.Write("Wrong input!!");
                        Console.ReadKey();
                        break;
                }
            }
            Console.Clear();
            Console.WriteLine("Closing program....");
        }

        static void Main(string[] args)
        {
            new Program();
        }
    }
}
