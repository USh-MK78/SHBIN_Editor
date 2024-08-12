using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLBLibrary
{
    public class StructData
    {
        public class DVLE
        {
            /// <summary>
            /// Integer Vector 4
            /// </summary>
            public struct IntVector4
            {
                public byte X { get; set; }
                public byte Y { get; set; }
                public byte Z { get; set; }
                public byte W { get; set; }

                public static IntVector4 Default()
                {
                    return new IntVector4(0, 0, 0, 1);
                } 

                public IntVector4(byte X, byte Y, byte Z, byte W)
                {
                    this.X = X;
                    this.Y = Y;
                    this.Z = Z;
                    this.W = W;
                }

                public override string ToString()
                {
                    return "[IVec4] X : " + X + " | Y : " + Y + " | Z : " + Z + " | W : " + W;
                }
            }

            /// <summary>
            /// Float Vector4 (MaxValue : 0x00FFFFFF)
            /// </summary>
            public struct FloatVector4
            {
                //TODO : Create Float24 (3byte float)
                public float X { get; set; }
                public float Y { get; set; }
                public float Z { get; set; }
                public float W { get; set; }

                public FloatVector4(float X, float Y, float Z, float W)
                {
                    this.X = X;
                    this.Y = Y;
                    this.Z = Z;
                    this.W = W;
                }

                public override string ToString()
                {
                    return "[Vec4] X : " + X + " | Y : " + Y + " | Z : " + Z + " | W : " + W;
                }
            }
        }
    }
}
