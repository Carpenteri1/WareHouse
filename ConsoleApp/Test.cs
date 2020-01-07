using System;
using System.Collections.Generic;
using System.Text;
using Warehouse;
namespace ConsoleApp
{
    class Test
    {
        private Orgenizer orgenizer = new Orgenizer();
        private const int Id_Dont_Excist_For_Product = -1;
        private WareHouse warehouse;
        private const int No_Value_Needed_Here = 0;
        public Test()
        {

        }

        public Test(WareHouse warehouse)
        {
            CreateDataValues(warehouse);
        }

        public void CreateDataValues(WareHouse warehouse)
        {
            int boxPositon;
            int levelPositon;


            {
            //creating test products no value needed here is only needed when we want to find the old positon if we move a box
            levelPositon = 2;
            boxPositon = 32;
            warehouse.StoreAtIndexLocation(boxPositon, levelPositon,new Cube("KKKK","A test box",25,false,5),No_Value_Needed_Here,No_Value_Needed_Here);

            levelPositon = 3;
            boxPositon = 6;
            warehouse.StoreAtIndexLocation(boxPositon, levelPositon, new Sphere("WWWW", "packeges from canada", 5, false, 35),No_Value_Needed_Here,No_Value_Needed_Here);
                warehouse.StoreAtFreeLocation(new Cube("AAAA", "Unkown, is it illigal ?", 890, false, 43));
                warehouse.StoreAtFreeLocation(new Cube("DDDD", "training gear", 1, true, 43));
            }

           {
                //creating sphere products       
                warehouse.StoreAtFreeLocation(new Sphere("VVVV", "bowling ball", 5, false, 29));
                warehouse.StoreAtFreeLocation(new Sphere("CCCC", "", 567, true, 67));

                levelPositon = 3;
                boxPositon = 76;
                //stored at level 3 box 76 use content or search method to find it
                warehouse.StoreAtIndexLocation(boxPositon, levelPositon, new Sphere("FYYYY", "packeges from canada", 5, true, 35),No_Value_Needed_Here,No_Value_Needed_Here);

            }

            {
                levelPositon = 2;
                boxPositon = 50;
                //stored at level 2 box 50 use content or search method to find it
                warehouse.StoreAtIndexLocation(boxPositon, levelPositon, new Blob("PPPP", "kitchen gear", 56, 3),No_Value_Needed_Here,No_Value_Needed_Here);
                warehouse.StoreAtFreeLocation(new Blob("OOOO", "Some slime.. we think", 3, 87));
                warehouse.StoreAtFreeLocation(new Blob("FFFF", "Did some one puke that?", 12, 32));
            }

            {
                levelPositon = 2;
                boxPositon = 32;
                //stored at level 3 box 100 use content or serach method to find it
                warehouse.StoreAtIndexLocation(boxPositon, levelPositon, new Cubeoid("XXXX", "We dont know", 7, false, 11, 22, 13),No_Value_Needed_Here,No_Value_Needed_Here);
                warehouse.StoreAtFreeLocation(new Cubeoid("CCCC", "maybe an xbox 10?", 12, false, 4, 7, 12));
                //stored at level 1 box 34 use content or serach method to find it
                warehouse.StoreAtIndexLocation(34, 1, new Cubeoid("TTTT", "Car parts", 34, true, 43, 19, 22),No_Value_Needed_Here,No_Value_Needed_Here);

            }

            {
         
                Console.WriteLine("Test file loaded");
                Console.ReadKey();
                
            }



        }
   

        private bool CheckIfIdExcist(string id,WareHouse warehouse)
        {

            //if id isnt found the return position will be -1 = Id_Dont_Excist
            if (warehouse.ReturnFloorPosition(id) != Id_Dont_Excist_For_Product || warehouse.ReturnBoxPosition(id) != Id_Dont_Excist_For_Product)
            {
                return true;
            }
            else
            {
                Console.WriteLine($"No product with ID: {id} was found!");
            }
            return false;

        }


    }
}
