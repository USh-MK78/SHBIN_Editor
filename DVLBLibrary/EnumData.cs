using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLBLibrary
{
    public class EnumData
    {
        public class DVLE
        {
            public enum ShaderType : byte
            {
                Vertex = 0x00,
                Geometry = 0x01,
                Unknown
            }

            /// <summary>
            /// ConstantTable -> ConstantType
            /// </summary>
            public enum ConstantType : short
            {
                Boolean = 0,
                IVec4 = 1,
                Vec4 = 2
            }

            /// <summary>
            /// OutputTable -> 
            /// </summary>
            public enum OutputTable_OutputType : short
            {
                Result_Position = 0x00,
                Result_Normalquat = 0x01,
                Result_Color = 0x02,
                Result_TexCoord0 = 0x03,
                Result_TexCoord0w = 0x04,
                Result_TexCoord1 = 0x05,
                Result_TexCoord2 = 0x06,
                Result_Unknown = 0x07,
                Result_View = 0x08
            }

            /// <summary>
            /// UniformTable -> RegisterSpaceType
            /// </summary>
            public enum RegisterSpaceType : byte
            {
                #region v0 - v15
                v0 = 0x00,
                v1 = 0x01,
                v2 = 0x02,
                v3 = 0x03,
                v4 = 0x04,
                v5 = 0x05,
                v6 = 0x06,
                v7 = 0x07,
                v8 = 0x08,
                v9 = 0x09,
                v10 = 0x0A,
                v11 = 0x0B,
                v12 = 0x0C,
                v13 = 0x0D,
                v14 = 0x0E,
                v15 = 0x0F,
                #endregion

                #region c0 - c95
                c0 = 0x10,
                c1 = 0x11,
                c2 = 0x12,
                c3 = 0x13,
                c4 = 0x14,
                c5 = 0x15,
                c6 = 0x16,
                c7 = 0x17,
                c8 = 0x18,
                c9 = 0x19,
                c10 = 0x1A,
                c11 = 0x1B,
                c12 = 0x1C,
                c13 = 0x1D,
                c14 = 0x1E,
                c15 = 0x1F,

                c16 = 0x20,
                c17 = 0x21,
                c18 = 0x22,
                c19 = 0x23,
                c20 = 0x24,
                c21 = 0x25,
                c22 = 0x26,
                c23 = 0x27,
                c24 = 0x28,
                c25 = 0x29,
                c26 = 0x2A,
                c27 = 0x2B,
                c28 = 0x2C,
                c29 = 0x2D,
                c30 = 0x2E,
                c31 = 0x2F,

                c32 = 0x30,
                c33 = 0x31,
                c34 = 0x32,
                c35 = 0x33,
                c36 = 0x34,
                c37 = 0x35,
                c38 = 0x36,
                c39 = 0x37,
                c40 = 0x38,
                c41 = 0x39,
                c42 = 0x3A,
                c43 = 0x3B,
                c44 = 0x3C,
                c45 = 0x3D,
                c46 = 0x3E,
                c47 = 0x3F,

                c48 = 0x40,
                c49 = 0x41,
                c50 = 0x42,
                c51 = 0x43,
                c52 = 0x44,
                c53 = 0x45,
                c54 = 0x46,
                c55 = 0x47,
                c56 = 0x48,
                c57 = 0x49,
                c58 = 0x4A,
                c59 = 0x4B,
                c60 = 0x4C,
                c61 = 0x4D,
                c62 = 0x4E,
                c63 = 0x4F,

                c64 = 0x50,
                c65 = 0x51,
                c66 = 0x52,
                c67 = 0x53,
                c68 = 0x54,
                c69 = 0x55,
                c70 = 0x56,
                c71 = 0x57,
                c72 = 0x58,
                c73 = 0x59,
                c74 = 0x5A,
                c75 = 0x5B,
                c76 = 0x5C,
                c77 = 0x5D,
                c78 = 0x5E,
                c79 = 0x5F,

                c80 = 0x60,
                c81 = 0x61,
                c82 = 0x62,
                c83 = 0x63,
                c84 = 0x64,
                c85 = 0x65,
                c86 = 0x66,
                c87 = 0x67,
                c88 = 0x68,
                c89 = 0x69,
                c90 = 0x6A,
                c91 = 0x6B,
                c92 = 0x6C,
                c93 = 0x6D,
                c94 = 0x6E,
                c95 = 0x6F,
                #endregion

                #region i0 - i3
                i0 = 0x70,
                i1 = 0x71,
                i2 = 0x72,
                i3 = 0x73,
                #endregion

                #region b0 - b15
                b0 = 0x78,
                b1 = 0x79,
                b2 = 0x7A,
                b3 = 0x7B,
                b4 = 0x7C,
                b5 = 0x7D,
                b6 = 0x7E,
                b7 = 0x7F,
                b8 = 0x80,
                b9 = 0x81,
                b10 = 0x82,
                b11 = 0x83,
                b12 = 0x84,
                b13 = 0x85,
                b14 = 0x86,
                b15 = 0x87,
                #endregion
            }
        }
    }
}
