using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Warehouse
{

    [Serializable()]
    public class WareHouseLocation 
    {
        private List<I3DStorageObject> wareHouse = new List<I3DStorageObject>();
        private const int Set_ID = 1;
        private const int Set_Description = 2;
        private long combinedProductWeight;
        private int combinedProductVolym;
        private int volym;
        private int[] findMaxValue = new int[3];
        internal WareHouseLocation(int floorLevel, int boxPosition)
        {
            this.BoxPosition = boxPosition;
            this.FloorLevel = floorLevel;
            this.Height = 220;
            this.Width = 200;
            this.Depth = 140;
            this.MaxVolume = Height * Width * Depth;
            MaxWeight = 1000;
            findMaxValue[0] = Height;
            findMaxValue[1] = Width;
            findMaxValue[2] = Depth;
            MaxDimension = findMaxValue.Max();
        }

        internal WareHouseLocation()
        {

        }

        internal int BoxPosition{get;}
        internal int FloorLevel {get;} 
        internal int Height{get;}
        internal int Width {get;}
        internal int Depth {get;}
        internal long MaxWeight{get;}
        internal int MaxVolume {get;}
        internal int MaxDimension{get;}

        internal bool StoreProduct(I3DStorageObject newProduct, long weigth,int volym, bool isFragile,int maxDimension)
        {
            if (!BoxIsFull(newProduct,weigth,volym,isFragile,maxDimension))
            {
                wareHouse.Add(newProduct);
                return true;
            }

            return false;
        }

        internal bool BoxIsFull(I3DStorageObject newProduct,long weight,int volym,bool isFragile ,int maxDimension)
        {

            if (!isFragile && !FragileProductsAlreadyAdded())
            {
                if (CalculateVolume(volym) < MaxVolume)
                {
                    if(maxDimension < this.MaxDimension)
                    {
                        if (CalculateWeigh(weight) < this.MaxWeight)
                        {
                            this.combinedProductVolym += volym;
                            this.combinedProductWeight += weight;
                            return false;//box isnt full
                        }
                    }
                }
                return true;
            }
            else if(isFragile)
            {
                if(wareHouse.Count < 1)
                {
                    return false;//box isnt full
                }
            }
         
            return true;
        }

        internal bool FragileProductsAlreadyAdded()
        {
            foreach (var s in wareHouse)
            {
                if (s != null)
                {
                    if(s.IsFragile == true)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        internal int ProductCount
        {
            get => this.wareHouse.Count;
        }

        internal I3DStorageObject RemoveProduct(string id,long weight, int volyme)
        {
            I3DStorageObject productInfo = FoundByID(id);
            combinedProductVolym -= volym;
            combinedProductWeight -=combinedProductWeight;
            wareHouse.Remove(FoundByID(id));

            return productInfo;
        }

        internal I3DStorageObject RemoveProduct(string id)
        {
            I3DStorageObject productInfo = FoundByID(id);
            wareHouse.Remove(FoundByID(id));
            return productInfo;
        }


        internal I3DStorageObject FoundByID(string id)
        {
            foreach (var s in wareHouse)
            {
                if (s.ToString().Contains(id))
                {
                    return s;
                }
            }
            return null;
        }


        internal void DeletEveryThing()
        {
            wareHouse.Clear();
        }

        internal long CalculateWeigh(long weight)
        {
            long copyCombinedProductWeigth = this.combinedProductWeight;
            copyCombinedProductWeigth += weight;
            return copyCombinedProductWeigth;
        }

        internal int CalculateVolume(int volym)
        {
            int copyCombinedProductVolym = this.combinedProductVolym;
            copyCombinedProductVolym += volym;
            return copyCombinedProductVolym;

        }

        internal WareHouseLocation Content()
        {
            WareHouseLocation cloneWarehouse = new WareHouseLocation();
            foreach(var boxes in wareHouse)
            {
                I3DStorageObject cloneBox = boxes.Clone() as I3DStorageObject;
                cloneWarehouse.wareHouse.Add(cloneBox);
            }
            return cloneWarehouse;
        }

        public IEnumerator GetEnumerator()
        {
            return wareHouse.GetEnumerator();
        }
    }
}