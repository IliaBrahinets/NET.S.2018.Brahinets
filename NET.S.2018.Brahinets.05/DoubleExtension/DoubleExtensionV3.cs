using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;


public static class DoubleExtensionV3
{
    [StructLayout(LayoutKind.Explicit)]
    private struct DoubleToLongStruct
    {
        [FieldOffset(0)]
        private readonly long long64Bits;

        [FieldOffset(0)]
        private double double64Bits;

        public long GetLongFromDouble(double value)
        {
            double64Bits = value;
            return this.long64Bits;
        }

    }

    public static string BitsRepresentationAsStringV3(this double value)
    {
        DoubleToLongStruct converter = new DoubleToLongStruct();

        long asLong = converter.GetLongFromDouble(value);

        return asLong.LongToBitsString();
    }

    
}

