using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Warehouse
{
    public class CreateProduct
    {
       
        private static object input;
        private static bool isFragile;
        private static int xside, yside, zside,size;
        /// <summary>
        /// Calls create new product and creates a new product depending on its shape
        /// </summary>
        /// <param name="shape"></param>
        /// <param name="id"></param>
        /// <param name="description"></param>
        /// <param name="weight"></param>
        /// <param name="isFragile"></param>
        /// <param name="xside"></param>
        /// <param name="yside"></param>
        /// <param name="zside"></param>
        /// <returns></returns>
        public static I3DStorageObject HandleProductCreation(string shape, string id, string description, long weight,bool isFragile,int xside,int yside,int zside)
        {
            switch (shape)
            {
                case "cube":
                    return CreatingNewProduct(ProductShape.Cube, id, description,weight,isFragile,xside,yside,zside);
                case "blob":
                    return CreatingNewProduct(ProductShape.Blob, id, description, weight,isFragile,xside,yside,zside);
                case "sphere":
                    return CreatingNewProduct(ProductShape.Sphere, id, description, weight,isFragile,xside,yside,zside);
                case "cubeoid":
                    return CreatingNewProduct(ProductShape.Cubeoid, id, description, weight,isFragile,xside,yside,zside);

            }

            return null;
        }

        public static I3DStorageObject CreatingNewProduct(ProductShape shape, string id, string description, long weight,bool isFragile,int xside,int yside,int zside)
        {


            if (shape.Equals(ProductShape.Cube))
            { 
                return new Cube(id, description, weight, isFragile, xside);
            }
            if (shape.Equals(ProductShape.Sphere))
            {
                return new Sphere(id, description, weight, isFragile, xside);
            }
            if (shape.Equals(ProductShape.Cubeoid))
            {
                return new Cubeoid(id,description,weight,isFragile,xside,yside,zside);

            }
            if (shape.Equals(ProductShape.Blob))
            {
                return new Blob(id, description, weight, xside);
            }

            return null;
        }

    }
}
