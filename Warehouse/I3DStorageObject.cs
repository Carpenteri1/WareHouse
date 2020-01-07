using System;
using System.Collections.Generic;
using System.Text;

namespace Warehouse
{
    public interface I3DStorageObject : ICloneable
    {
        string ID
        {
            get;
        }
        string Description
        {
            get;
        }

        long Weight
        {
            get;
        }

        int Volume
        {
            get;
        }

        long Area
        {
            get;
        }

        bool IsFragile
        {
            get;
        }

        int MaxDimension
        {
            get;
        }
        int InsuranceValue
        {
            get;
            set;
        }


    }
}
