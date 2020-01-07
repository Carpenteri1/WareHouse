using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Warehouse
{

    [Serializable()]
    public class WareHouse
    {
        private WareHouseLocation[,] slots = new WareHouseLocation[4,101];
        private bool result, boxIsFragile;
        private int startindex = 1;

        public WareHouseLocation[,] Slot
        {
            get { return slots; }
            set { slots = value; }
        }


        public I3DStorageObject StoreAtFreeLocation(I3DStorageObject product)//calling this method if user didnt say where it wanted to store the object
        {
            for (int i = startindex; i < slots.GetLength(0);i++)
            {
                for(int  j = startindex;j < slots.GetLength(1);)
                {
                    if (slots[i, j] != null)
                    {
                        result = slots[i, j].StoreProduct(product,product.Weight,product.Volume,product.IsFragile,product.MaxDimension);
                        if(!result)
                        {
                            j++;
                        }
                        else
                        {
                            return slots[i, j].FoundByID(product.ID);
                        }
                    }
                    else if (slots[i, j] == null)
                    {
                        slots[i, j] = new WareHouseLocation(i, j);
                        slots[i, j].StoreProduct(product, product.Weight,product.Volume,product.IsFragile,product.MaxDimension);
                        return slots[i, j].FoundByID(product.ID);
                    }
                }
          
            }        
            return null;
        }


        public I3DStorageObject StoreAtIndexLocation(int slot,int floor,I3DStorageObject product,int oldFloorPosition, int oldBoxPosition)
        {
            if (slots[floor, slot] == null)
            {
                if(oldFloorPosition > 0 && oldBoxPosition > 0)
                {

                    slots[oldFloorPosition, oldBoxPosition].RemoveProduct(product.ID,product.Weight,product.Volume);
                    if(slots[oldFloorPosition, oldBoxPosition].ProductCount == 0)
                    {
                        slots[oldFloorPosition, oldBoxPosition] = null;
                    }
                    
                }
                slots[floor, slot] = new WareHouseLocation(floor, slot);
                slots[floor, slot].StoreProduct(product, product.Weight, product.Volume, product.IsFragile, product.MaxDimension);
                return slots[floor, slot].FoundByID(product.ID); 
            }
            if(slots[floor,slot] != null)
            {
                result = slots[floor, slot].StoreProduct(product, product.Weight,product.Volume,product.IsFragile,product.MaxDimension);
                if (result)
                {
                    if (oldFloorPosition > 0 && oldBoxPosition > 0)
                    {

                        slots[oldFloorPosition, oldBoxPosition].RemoveProduct(product.ID, product.Weight, product.Volume);
                        if (slots[oldFloorPosition, oldBoxPosition].ProductCount == 0)
                        {
                            slots[oldFloorPosition, oldBoxPosition] = null;
                        }
                    }
                    //slots[floor, slot].StoreProduct(product, product.Weight, product.Volume, product.IsFragile, product.MaxDimension);
                }
                else
                {   //removes the product from its old position
                    throw new ArgumentException($"Level {floor} at box positon {slot} is full"
                 + $" the product could not be added\n");
                    //adds the product to the new positon / "moves it to the new position box"
                  
                }
                return slots[floor, slot].FoundByID(product.ID);
            }
            return null;
        }

        public I3DStorageObject FoundByID(string id)//
        {
            for(int i = startindex; i < slots.GetLength(0); i++)
            {
                for(int j = startindex; j < slots.GetLength(1); j++)
                {
                    if(slots[i,j] != null)
                    {
                        if (slots[i, j].FoundByID(id) != null)
                        {
                            return slots[i, j].FoundByID(id);
                        }
                    }

                }
            }
            return null;
        }

        public WareHouseLocation LookAtBox(string id)
        {
            for (int i = startindex; i < slots.GetLength(0); i++)
            {
                for (int j = startindex; j < slots.GetLength(1); j++)
                {
                    if(Slot[i,j] != null)
                    {
                        if (slots[i, j].FoundByID(id) != null)
                        {
                            return slots[i, j];
                        }
                    }
                }
            }
            return null;
        }


        public WareHouseLocation Content(int level,int box)
        {

            if (Slot[level, box] != null)
            {
                return slots[level, box].Content();
            }
            return null;
        }


        public int ReturnBoxPosition(string id)
        {
            if(LookAtBox(id) != null)
            {
                return LookAtBox(id).BoxPosition;
            }

            return -1;
          
        }

        public int ReturnFloorPosition(string id)
        {
            if (LookAtBox(id) != null)
            {
                return LookAtBox(id).FloorLevel;
            }
            return -1;
        }


        public void DeletEveryThing()
        {
            foreach(var s in slots)
            {
                if(s != null)
                {
                    s.DeletEveryThing();
                }
            }
        }

        public I3DStorageObject RemoveProduct(string id,long weight,int volym)
        {

            for (int i = startindex; i < slots.GetLength(0); i++)
            {
                for (int j = startindex; j < slots.GetLength(1); j++)
                {
                    if (slots[i, j] != null)
                    {
                        if (slots[i, j].FoundByID(id) != null)
                        {
                            return slots[i, j].RemoveProduct(id,weight,volym);
                        }
                    }

                }
            }
            return null;
        }
    }
}

