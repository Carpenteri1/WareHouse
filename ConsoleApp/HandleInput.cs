using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Warehouse;


namespace ConsoleApp
{
    class HandleInput
    {
        private static WareHouse warehouse;
        private static int index;
        public static string InputHandler(WantToSet n)
        {
            switch (n)
            {
                case WantToSet.id:
                    return SetInput("Enter product ID:> ",WantToSet.id);

                case WantToSet.description:
                    return SetInput("Enter a description:> ",WantToSet.description);

                case WantToSet.floorLevel:
                    return SetInput("Enter Storage storage level, 1 - 3 :> ",WantToSet.floorLevel);

                case WantToSet.floorLevelSecondTime:
                    return SetInput("Enter storage level, 1 - 3 :> ", WantToSet.floorLevelSecondTime);

                case WantToSet.boxPosition:
                    return SetInput("Enter box position, 1 - 100:> ",WantToSet.boxPosition);

                case WantToSet.productShape:
                   return SetInput("What shape is it ?" +
                       "\nPress 1 : Cube" +
                       "\nPress 2 : Blob" +
                       "\nPress 3 : Sphere" +
                       "\nPress 4 : Cubeoid" +
                       "\n:> ",WantToSet.productShape);

                case WantToSet.insurens:
                    return SetInput("Enter Insurens value: ",WantToSet.insurens);

                case WantToSet.weight:
                    return SetInput("Enter product weight:> ",WantToSet.weight);

                case WantToSet.size:
                    return SetSize();

                default:
                    Console.WriteLine("Something went wrong with the input handler");
                    Console.ReadKey();
                    break;
            }

            return null;
        }

        private static string SetInput(string consolePrompt,WantToSet type)
        {
            Console.Clear();
            Console.Write(consolePrompt);
            string input = CheckInput(Console.ReadLine(),type);
            return input;
        }

        private static string SetShape(string input)
        {

            if(input == "1")
            {
                return "cube";
            }
            if(input == "2")
            {
                return "blob";
            }
            if(input == "3")
            {
                return "sphere";
            }
            if(input == "4")
            {
                return "cubeoid";
            }

            return null;
        }

        private static string SetSize()
        {
            Console.Write("Enter size:> ");
            string setSize = Console.ReadLine();
            int size = -1;
            if (string.IsNullOrEmpty(setSize))
            {
                throw new ArgumentException("Cant be empty ");
            }
            if (Regex.IsMatch(setSize, @"^\d+$")) // is it numric 
            {
                size = int.Parse(setSize);

                if (size <= 0)
                {
                    throw new ArgumentException($"Size cant be {size}");
                }
                else
                { 
                    return setSize;
                }
            }
            else
            {
                throw new ArgumentException("Wrong input! ");
            }
            return setSize;
        }




        public static bool IsAFragileProduct()
        {
            Console.Write("Is the product fragile Y/N: ");
            string input = Console.ReadLine().ToUpper();
            if (input == "Y")
            {
                return true;
            }
            else if (input == "N")
            {
                return false;
            }
            else
            {
                Console.WriteLine("Wrong input!");
                Console.ReadKey();
                Console.Clear();
                IsAFragileProduct();
            }

            return false;
        }

        private static string CheckInput(string input,WantToSet type)
        {

            if (string.IsNullOrEmpty(input) && !type.Equals(WantToSet.floorLevel))
            {
                throw new ArgumentException("Input cant be empty");
            }
            if (type.Equals(WantToSet.floorLevel))
            {
                if (string.IsNullOrEmpty(input))
                {
                    return input;
                }
                else
                {
                    index = int.Parse(input);
                    if (index < 1 || index > 3)
                    {
                        throw new ArgumentException("Storage level input must be between 1 - 3");
                    }
                    else
                    {
                        return input;
                    }
                }
            }
           
            if (input.Length < 3 && type.Equals(WantToSet.id))
            {
                throw new ArgumentException("Id needs to be atleast 3 letters");
            }
            if (type.Equals(WantToSet.floorLevelSecondTime))
            {
                index = int.Parse(input);
                if (index < 1 || index > 3)
                {
                    throw new ArgumentException("Storage level input must be between 1 - 3");
                }
            }

            if (type.Equals(WantToSet.boxPosition))
            {
                index = int.Parse(input);
                if (index < 1 || index > 100)
                {
                    throw new ArgumentException("Box input value must be between 1 - 100");
                }        
            }
            if (type.Equals(WantToSet.productShape))
            {
                index = int.Parse(input);
                if (index < 1 || index > 4)
                {
                    Console.WriteLine("Wrong input!");
                    Console.ReadKey();
                    Console.Clear();
                    InputHandler(WantToSet.productShape);
                }
                else
                {
                    input = SetShape(input);
                }
            }
            if (type.Equals(WantToSet.weight))
            {
                if (Regex.IsMatch(input, @"^\d+$")) // is it numric
                {
                    index = int.Parse(input);
                    if(index <= 0)
                    {
                        throw new ArgumentException($"Weight input cant be {index} it needs to be positive");
                    }
                    return input;
                }
                else
                {
                    throw new ArgumentException($"Weight input {input} must be a number and cant be negative");
                }
                   
            }

          
            return input;
        }
    }
}
