using System;
using System.Collections.Generic;
using System.Text;

namespace Warehouse 
{
    [Serializable()]
    public class Sphere : ICalculate,I3DStorageObject
    {

        private int radius;
        private readonly uint cubeSides = 6;
        private string id, description;
        private ProductShape shape;
        private long weight;
        private bool isFragile;
        private int insuranceValue;

        public Sphere(string id,string description,long weight,bool isFragile,int radius)
        {
            this.id = id;
            this.description = description;
            this.weight = weight;
            this.radius = radius;
            this.isFragile = isFragile;
            shape = ProductShape.Sphere;
        }

        public ProductShape Shape => shape;

        public string ID => id;

        public string Description => description;

        public long Weight => weight;

        public int Radius//you are asked to set the side size there for the radius is half of that
        {
            get => radius / 2;
        }

        public int Volume => CalculateVolume(radius);

        public long Area => CalculateArea(radius);

        public bool IsFragile => isFragile;

        public int MaxDimension => radius * 2;

        public int InsuranceValue
        {
            get => insuranceValue;
            set => insuranceValue = value;
        }

        public int CalculateVolume(int sides)
        {
            int volume = sides * sides * sides;
            return volume;
        }

        public long CalculateArea(int sides)
        {
            long area = (sides * sides) * cubeSides;
            return area;
        }

        public override string ToString()
        {
            return $"ID: {ID}\nShape: {shape}" +
                $"\nDescription: {Description}\nWeight: {Weight} kg" +
                $"\nRadius: {radius} cm" +
                $"\nArea: {Area} cm2 \nVolume: {Volume} cm2\nFragile product: {IsFragile}\nInsurance value: {InsuranceValue}$";
        }

        public object Clone()
        {
            Sphere sphere = new Sphere(ID,Description,Weight,IsFragile,Radius);
            return sphere;
        }
    }
}
