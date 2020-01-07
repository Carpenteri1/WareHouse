using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Warehouse;
using System.Linq;

namespace ConsoleApp
{
    class Orgenizer
    {
        private WareHouse warehouse;   
        private const I3DStorageObject Id_Dont_Excist = null;
        private const int Id_Dont_Excist_For_Product = -1;
        private const int Side_Needs_No_Value = 0;

        /// <summary>
        /// 
        /// </summary>
        public WareHouse Storage
        {
            get => warehouse;
            set => warehouse = value;
        }

        public void AddProduct()
        {
            string id = SendToUserInput(WantToSet.id).ToUpper();
            if (warehouse.FoundByID(id) == Id_Dont_Excist)
            {
                OrgenizeInput(id);
            }
            else
            {
                Console.Clear();
                Console.Write($"ID {id} already excist! ");
                Console.ReadKey();
            }
        }

        private void CreateProduct(string id, string shape, bool isFragile, string description, long weight, int xside,int yside,int zside)
        {
            string floorLevel = SendToUserInput(WantToSet.floorLevel);
            if (!string.IsNullOrEmpty(floorLevel))
            {
                ChooseAPosition(shape, id, description, weight, isFragile,xside,yside,zside,floorLevel);
                    Console.WriteLine($"{warehouse.FoundByID(id)}" +
                 $"\n\nProduct {id} is now added to Level: {warehouse.ReturnFloorPosition(id)} " +
                 $"|| Box: {warehouse.ReturnBoxPosition(id)}");
                
            }
            else
            {
                Console.WriteLine($"{warehouse.StoreAtFreeLocation(Warehouse.CreateProduct.HandleProductCreation(shape, id, description, weight, isFragile,xside,yside,zside))}" +
                     $"\n\nProduct {id} is now added to Level: {warehouse.ReturnFloorPosition(id)} " +
                     $"|| Box: {warehouse.ReturnBoxPosition(id)}");
                Console.ReadKey();
            }
        }

        /// <summary>
        /// Orgenize every input, and calls SendsToUserInput that calls handleInpup class. 
        /// </summary>
        /// <param name="id"></param>
        private void OrgenizeInput(string id)
        {
            string shape = SendToUserInput(WantToSet.productShape);
            bool isFragile = HandleIsFragile(shape);

            string description = SendToUserInput(WantToSet.description);
            long weight = long.Parse(SendToUserInput(WantToSet.weight));

            HandleSize(id,shape,isFragile,description,weight);
        }

        /// <summary>
        /// handles the size, if its a cubeoid we need to handle 3 sides. 
        /// Everything else needs to handle only 1 side
        /// </summary>
        /// <param name="id"></param>
        /// <param name="shape"></param>
        /// <param name="isFragile"></param>
        /// <param name="description"></param>
        /// <param name="weight"></param>
        private void HandleSize(string id,string shape,bool isFragile,string description,long weight)
        {
            if (shape == "cubeoid")
            {
                int xside = int.Parse(SendToUserInput(WantToSet.size));
                int yside = int.Parse(SendToUserInput(WantToSet.size));
                int zside = int.Parse(SendToUserInput(WantToSet.size));

                CreateProduct(id, shape, isFragile, description, weight,xside,yside,zside);
            }
            else
            {
                int xside = int.Parse(SendToUserInput(WantToSet.size));
                CreateProduct(id, shape, isFragile, description, weight, xside,Side_Needs_No_Value,Side_Needs_No_Value);
            }
        }

        private bool HandleIsFragile(string shape)
        {
            bool isFragile = false;
            if (shape == "blob")
            {
                isFragile = true;
            }
            else
            {
               isFragile = HandleInput.IsAFragileProduct();
            }

            return isFragile;
        }

        private void ChooseAPosition(string shape,string id, string description,long weight,bool isFragile,int xside,int yside,int zside,string floorLevel)
        {

                string boxPosition = SendToUserInput(WantToSet.boxPosition);
            if (!string.IsNullOrEmpty(boxPosition))
            {
                int convertFloor = int.Parse(floorLevel);
                int convertBox = int.Parse(boxPosition);
                try
                {
                        warehouse.StoreAtIndexLocation(convertBox, convertFloor, 
                        Warehouse.CreateProduct.HandleProductCreation(shape, id, description,weight,isFragile,xside,yside,zside),0,0);
                }
                catch (IndexOutOfRangeException e)//if position is full we catch it
                {
                    Console.WriteLine("Warehouse is full!");
                    Console.ReadKey();
                }catch(ArgumentException e)
                {
                    Console.WriteLine(e.Message);
                    Console.ReadKey();
                }
                finally
                {

                }
            }
        }

        /// <summary>
        /// Called when we want to move a product
        /// </summary>
        /// <param name="product"></param>
        /// <param name="floorLevel"></param>
        private void ChooseAPosition(I3DStorageObject product,string floorLevel,string id)
        {

            string boxPosition = SendToUserInput(WantToSet.boxPosition);
            if (!string.IsNullOrEmpty(boxPosition))
            {
                int convertFloor = int.Parse(floorLevel);
                int convertBox = int.Parse(boxPosition);
                int oldFloorPositon = warehouse.ReturnFloorPosition(product.ID);
                int oldBoxPositon = warehouse.ReturnBoxPosition(product.ID);

                try
                {
                    warehouse.StoreAtIndexLocation(convertBox, convertFloor,product,oldFloorPositon,oldBoxPositon);
                    Console.WriteLine($"{warehouse.FoundByID(id)}" +
                    $"\n\nProduct {id} is now added to Level: {warehouse.ReturnFloorPosition(id)} " +
                    $"|| Box: {warehouse.ReturnBoxPosition(id)}");
                    Console.ReadKey(); 
                }
                catch (IndexOutOfRangeException e)//if position is full we catch it
                {
                    Console.WriteLine("Warehouse is full!");
                    Console.ReadKey();
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine(e.Message);
                    Console.ReadKey();
                }
                finally
                {

                }
            }
        }

        /// <summary>
        /// checks if the id is already added return true or false
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool IDExcist(string id)
        {

            if(warehouse != null)
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

            }
            return false;
        }

        public void SearchForProduct()
        {
            string id = SendToUserInput(WantToSet.id).ToUpper();
            if (IDExcist(id))
            {
               
                Console.WriteLine($"{warehouse.FoundByID(id)}" +
                $"\nStored at: Level: {warehouse.ReturnFloorPosition(id)} || Box: {warehouse.ReturnBoxPosition(id)}");
            }
            Console.ReadKey();
        }

        /// <summary>
        /// removes a product by its id
        /// </summary>
        public void RemoveProduct()
        {
            string id = SendToUserInput(WantToSet.id).ToUpper();
            if (IDExcist(id))
            {
                Console.Write($"\n{warehouse.FoundByID(id)}" +
             $"\nStored at: Level: {warehouse.ReturnFloorPosition(id)} || Box: {warehouse.ReturnBoxPosition(id)}" +
             $"\nPress enter to remove:> Product {id}");
                warehouse.RemoveProduct(warehouse.FoundByID(id).ID,warehouse.FoundByID(id).Weight,warehouse.FoundByID(id).Volume);
            }
            Console.ReadKey();
        }
 
        /// <summary>
        /// moves a product by its id
        /// </summary>
        public void MoveProduct()
        {
            string id = SendToUserInput(WantToSet.id).ToUpper();
            I3DStorageObject saveData;
            if (IDExcist(id))
            {
                try
                {
                    saveData = warehouse.FoundByID(id);
                    string floorLevel = SendToUserInput(WantToSet.floorLevelSecondTime);//second time for moving object handled in another way
                    ChooseAPosition(saveData,floorLevel,saveData.ID);
                }catch(ArgumentException e)
                {
                    Console.WriteLine(e.Message);
                    Console.ReadKey();
                }
                finally
                {

                }
          
            }

        }


          /// <summary>
          /// handles every input the user do, and send it to the handle input class
          /// </summary>
          /// <param name="n"></param>
          /// <returns></returns>
        private static string SendToUserInput(WantToSet n)
        {
            string input = null;
            try
            {
                Console.Clear();
                return input = HandleInput.InputHandler(n);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();
                new Program();

            }
            catch(FormatException e)
            {
                Console.WriteLine(e.Message +" Must be a number!");
                Console.ReadKey();
                new Program();
            }
            finally
            {

            }
            return input;
        }

        /// <summary>
        /// Prints out the a specific storage box content
        /// </summary>
        public void GetContent()
        {
            int floor = int.Parse(SendToUserInput(WantToSet.floorLevelSecondTime));
            int box = int.Parse(SendToUserInput(WantToSet.boxPosition));

            if(warehouse.Content(floor,box) != null)
            {
                foreach (I3DStorageObject boxes in warehouse.Content(floor, box))
                {
                    if (boxes != null)
                    {
                        Console.WriteLine(boxes.ToString());
                    }
                }
            }
            else
            {
                Console.WriteLine("Box is empty");
            }
        
            Console.ReadKey();
        }

        public void GetAllContent()
        {
           
            for(int i = 1; i < 4; i++)
            {
                for(int j = 1; j < 101; j++)
                {
                    if(warehouse.Content(i,j)!= null)
                    {
                        foreach (I3DStorageObject boxes in warehouse.Content(i, j))
                        {
                            Console.WriteLine($"Floor: {i} Slot: {j} {boxes.ToString()}\n\n");
                        }
                    }
                   
                }
            }
            Console.ReadKey();
        }

        public void SaveData(bool BackUpLoaded)
        {
            if (BackUpLoaded)
            {
                BackUp.SaveDataToBin(warehouse);
            }
        }

        public void LoadDataFromBackUp()
        {
            Console.Clear();
            warehouse.DeletEveryThing();//remove test file data
            
            warehouse = BackUp.LoadDataFromBin();
            Console.WriteLine("Loaded from backup file!");
            Console.ReadKey();
        }


        public void LoadTestData()
        {
            Console.Clear();
            warehouse.DeletEveryThing();//remove backup file data (not the file)
            
            new Test(warehouse);
        }

        public void SetInsuresValue()
        {
            string id = SendToUserInput(WantToSet.id).ToUpper();
   

            if (IDExcist(id))
            {
                try
                {
                    int value = int.Parse(SendToUserInput(WantToSet.insurens));
                    warehouse.FoundByID(id).InsuranceValue = value;
                    Console.WriteLine($"Insurance value for {id} is set to {warehouse.FoundByID(id).InsuranceValue}$");
           
                }catch(FormatException e)
                {
                    Console.WriteLine(e.Message + " needs to be number");
                }
                finally
                {
                    Console.ReadKey();
                }
              
            }
        }

    }
}
