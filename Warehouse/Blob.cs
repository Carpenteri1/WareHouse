using System;
using System.Collections.Generic;
using System.Text;

namespace Warehouse
{
    [Serializable()]
    public class Blob : ICalculate,I3DStorageObject
    {
        private int sides;
        private readonly int cubeSides = 6;
        private string id, description;
        private long weight;
        private ProductShape shape;
        private int insuranceValue;
        


        public Blob(string id,string description,long weight,int sides)
        {
            this.id = id;
            this.description = description;
            this.weight = weight;
            this.sides = sides;
            shape = ProductShape.Blob;
        }

        public ProductShape Shape => shape;

        public string ID => id;

        public string Description => description;

        public long Weight => weight;

        public int Volume => CalculateVolume(sides);

        public long Area => CalculateArea(sides);

        public bool IsFragile => true;

        public int MaxDimension => Sides;

        public int Sides
        {
            get => sides;
            set => sides = value;
        }
        public int InsuranceValue
        {
            get => insuranceValue;
            set => insuranceValue = value;
        }

        public long CalculateArea(int sides)
        {
            long area = (sides * sides) * cubeSides;
            return area;
        }

        public int CalculateVolume(int sides)
        {
            int volume = sides * sides * sides;
            return volume;
        }

        public override string ToString()
        {
            return $"ID: {ID}\nShape: {shape}" +
                $"\nDescription: {Description}\nWeight: {Weight} kg" +
                $"\nSides: {sides} cm" +
                $"\nArea: {Area} cm2\nVolume: {Volume} cm2\nFragile product: {IsFragile}\nInsurance value: {InsuranceValue}$";
        }

        public object Clone()
        {
            Blob blob = new Blob(ID,Description,Weight,Sides);
            return blob;
        }
    }
}
