using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;//

namespace Warehouse
{
    [Serializable()]
    public class Cubeoid : I3DStorageObject
    {
        private int xSide, ySide, zSide;
        private long weight;
        private readonly uint cubeSides = 6;
        private string id, description;
        private ProductShape shape;
        private bool isFragile;
        private int insuranceValue;
        private int[] findMaxValue = new int[3];

        public Cubeoid(string id, string description, long weight, bool isFragile, int xSide, int ySide, int zSide)
        {
            this.id = id;
            this.description = description;
            this.weight = weight;
            this.isFragile = isFragile;
            this.xSide = xSide;
            this.ySide = ySide;
            this.zSide = zSide;
            shape = ProductShape.Cubeoid;
        }

        public string Shape => shape.ToString();

        public int XSide => xSide;

        public int YSide => ySide;

        public int ZSide => zSide;

        public string ID => id;

        public string Description => description;

        public long Weight => weight;

        public int Volume => CalculateVolume(zSide, xSide, ySide);

        public long Area => CalculateArea(xSide, ySide, zSide);//TODO might need an overhaul, math could be wrong

        public bool IsFragile => isFragile;

        public int MaxDimension
        {
            get {
                findMaxValue[0] = XSide;
                findMaxValue[1] = YSide;
                findMaxValue[2] = ZSide;
                int maxNumber = findMaxValue.Max();
                return maxNumber;
                }
        }
        public int InsuranceValue
        {
            get => insuranceValue;
            set => insuranceValue = value;
        }

        private long CalculateArea(int xSides,int ySides,int zSides)//TODO might need a overhaul, math could be wrong
        {
            long area = 2 * ((zSide * cubeSides) + (xSide * cubeSides) + (ySide * cubeSides));
            return area;
        }

        private int CalculateVolume(int zSide,int xSide,int ySide)
        {
            int volume = zSide * xSide * zSide;
            return volume;
        }

        public override string ToString()
        {
            return $"ID: {ID}\nShape: {shape}" +
                $"\nDescription: {Description}\nWeight: {Weight} kg" +
                $"\nSides: Xside {XSide} : Yside {ySide} : Zside {zSide}" +
                $"\nArea: {Area} cm2\nVolume: {Volume} cm2\nFragile product: {IsFragile}\nInsurance value: {InsuranceValue}$";
        }

        public object Clone()
        {
            Cubeoid cubeoid = new Cubeoid(ID, Description, Weight, IsFragile,XSide,YSide,ZSide);
            return cubeoid;
        }
    }
}
