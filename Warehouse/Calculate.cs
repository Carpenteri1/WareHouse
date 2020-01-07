using System;
using System.Collections.Generic;
using System.Text;

namespace Warehouse
{
    interface ICalculate
    { 
        int CalculateVolume(int sides);
        long CalculateArea(int sides);
    }
}
