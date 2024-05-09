using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLBLibrary
{
    /// <summary>
    /// DVLE
    /// </summary>
    public class DVLE
    {
        public char[] DVLE_Header { get; set; }
        public byte[] DVLE_UnknownByte1 { get; set; } //0x4
        public byte[] DVLE_UnknownByte4 { get; set; } //0x4
        public int DVLE_UnknownOffset0 { get; set; } //0x4
        public short DVLE_UnknownData6 { get; set; } //0x2
        public short DVLE_UnknownData7 { get; set; } //0x2
        public byte[] DVLE_UnknownByte8 { get; set; } //0x4

        public int DVLE_UnknownOffset1 { get; set; } //0x4, From : DVLE Header
        public int DVLE_UnknownDataCount1 { get; set; } //0x4
        public List<UnknownData1> UnknownData1_List { get; set; }
        public class UnknownData1
        {
            public short UnknownShortData0 { get; set; }
            public short ComponentCount { get; set; }

            public byte[] UnknownByteArray0 { get; set; } //0x4

            public UnknownArea UnknownAreaData { get; set; }
            public class UnknownArea
            {
                public short Data1 { get; set; }
                public short Data2 { get; set; }
                public short Data3 { get; set; }
                public short Data4 { get; set; }

                public void ReadUnknownArea(BinaryReader br, byte[] BOM)
                {
                    EndianConvert endianConvert = new EndianConvert(BOM);
                    Data1 = BitConverter.ToInt16(endianConvert.Convert(br.ReadBytes(2)), 0);
                    Data2 = BitConverter.ToInt16(endianConvert.Convert(br.ReadBytes(2)), 0);
                    Data3 = BitConverter.ToInt16(endianConvert.Convert(br.ReadBytes(2)), 0);
                    Data4 = BitConverter.ToInt16(endianConvert.Convert(br.ReadBytes(2)), 0);
                }

                public UnknownArea()
                {
                    Data1 = 0;
                    Data2 = 0;
                    Data3 = 0;
                    Data4 = 0;
                }
            }

            public byte UnknownByte1 { get; set; }
            public byte UnknownByte2 { get; set; }
            public short UnknownShortData1 { get; set; } //0x2, Count(?)

            public byte UnknownByte3 { get; set; }
            public byte UnknownByte4 { get; set; }
            public short UnknownShortData2 { get; set; } //0x2, Count(?)

            public byte[] UnknownByteArray1 { get; set; } //0x4

            public void ReadUnknownData0(BinaryReader br, byte[] BOM)
            {
                EndianConvert endianConvert = new EndianConvert(BOM);
                UnknownShortData0 = BitConverter.ToInt16(endianConvert.Convert(br.ReadBytes(2)), 0);
                ComponentCount = BitConverter.ToInt16(endianConvert.Convert(br.ReadBytes(2)), 0);

                UnknownByteArray0 = endianConvert.Convert(br.ReadBytes(4));
                UnknownAreaData.ReadUnknownArea(br, BOM);
                UnknownByte1 = br.ReadByte();
                UnknownByte2 = br.ReadByte();
                UnknownShortData1 = BitConverter.ToInt16(endianConvert.Convert(br.ReadBytes(2)), 0);

                UnknownByte3 = br.ReadByte();
                UnknownByte4 = br.ReadByte();
                UnknownShortData2 = BitConverter.ToInt16(endianConvert.Convert(br.ReadBytes(2)), 0);

                UnknownByteArray1 = endianConvert.Convert(br.ReadBytes(4));
            }

            public UnknownData1()
            {
                UnknownShortData0 = 0;
                ComponentCount = 0;

                UnknownByteArray0 = new byte[4];
                UnknownAreaData = new UnknownArea();
                UnknownByte1 = 0x00;
                UnknownByte2 = 0x00;
                UnknownShortData1 = 0;

                UnknownByte3 = 0x00;
                UnknownByte4 = 0x00;
                UnknownShortData2 = 0;

                UnknownByteArray1 = new byte[4];

            }
        }

        public int DVLE_ShaderEntryPointOffset { get; set; } //0x4, From : DVLE Header
        public int DVLE_ShaderEntryPointCount { get; set; } //0x4
        public List<ShaderEntryPoint> ShaderEntryPoint_List { get; set; }
        public class ShaderEntryPoint
        {
            public short ID { get; set; }
            public short UnknownData_1 { get; set; }
            public int UnknownData_2 { get; set; }
            public byte[] UnknownData_3 { get; set; } //0x4

            public int AttributeNameOffset { get; set; } //From : StringArray
            public List<char> AttributeNameCharAry { get; set; }
            public string AttributeName => new string(AttributeNameCharAry.ToArray());

            /// <summary>
            /// Get AttributeName (ShaderEntryPoint) 
            /// </summary>
            /// <param name="StringArray"></param>
            public void ReadAttributeName(char[] StringArray)
            {
                char[] cf = StringArray.Skip(AttributeNameOffset).Take(StringArray.Length - AttributeNameOffset).ToArray();

                foreach (var d in cf)
                {
                    if (d != '\0')
                    {
                        AttributeNameCharAry.Add(d);
                    }
                    else return;
                }
            }

            public void Read_ShaderEntryPoint(BinaryReader br, byte[] BOM)
            {
                EndianConvert endianConvert = new EndianConvert(BOM);
                ID = BitConverter.ToInt16(endianConvert.Convert(br.ReadBytes(2)), 0);
                UnknownData_1 = BitConverter.ToInt16(endianConvert.Convert(br.ReadBytes(2)), 0);
                UnknownData_2 = BitConverter.ToInt32(endianConvert.Convert(br.ReadBytes(4)), 0);
                UnknownData_3 = endianConvert.Convert(br.ReadBytes(4));
                AttributeNameOffset = BitConverter.ToInt32(endianConvert.Convert(br.ReadBytes(4)), 0);
            }

            public ShaderEntryPoint()
            {
                ID = 0;
                UnknownData_1 = 0;
                UnknownData_2 = 0;
                UnknownData_3 = new byte[4];
                AttributeNameOffset = 0;
                AttributeNameCharAry = new List<char>();
            }

            public override string ToString()
            {
                return AttributeName;
            }
        }

        public int DVLE_UnknownOffset_2 { get; set; } //0x4, From : DVLE Header
        public int DVLE_UnknownDataCount2 { get; set; } //0x4
        public List<UnknownData2> UnknownData2_List { get; set; }
        public class UnknownData2
        {
            public short UnknownShortData1 { get; set; }
            public short ID { get; set; }
            public int UnknownIntData1 { get; set; }

            public void ReadUnknownData2(BinaryReader br, byte[] BOM)
            {
                EndianConvert endianConvert = new EndianConvert(BOM);
                UnknownShortData1 = BitConverter.ToInt16(endianConvert.Convert(br.ReadBytes(2)), 0);
                ID = BitConverter.ToInt16(endianConvert.Convert(br.ReadBytes(2)), 0);
                UnknownIntData1 = BitConverter.ToInt32(endianConvert.Convert(br.ReadBytes(4)), 0);
            }

            public UnknownData2()
            {
                UnknownShortData1 = 0;
                ID = 0;
                UnknownIntData1 = 0;
            }
        }

        public int DVLE_UnknownOffset_3 { get; set; } //0x4, From : DVLE Header
        public int DVLE_UnknownDataCount3 { get; set; } //0x4
        public List<UnknownData3> UnknownData3_List { get; set; } //InputRegs (?)
        public class UnknownData3
        {
            public int AttributeNameOffset { get; set; } //From : StringArray
            //public char[] AttributeNameCharAry { get; set; }
            public List<char> AttributeNameCharAry { get; set; }
            public string AttributeName => new string(AttributeNameCharAry.ToArray());

            public short Data1 { get; set; }
            public short Data2 { get; set; }

            public void ReadUnknownData3(BinaryReader br, byte[] BOM)
            {
                EndianConvert endianConvert = new EndianConvert(BOM);
                AttributeNameOffset = BitConverter.ToInt32(endianConvert.Convert(br.ReadBytes(4)), 0);

                Data1 = BitConverter.ToInt16(endianConvert.Convert(br.ReadBytes(2)), 0);
                Data2 = BitConverter.ToInt16(endianConvert.Convert(br.ReadBytes(2)), 0);
            }

            public void ReadAttributeName(char[] StringArray)
            {
                char[] cf = StringArray.Skip(AttributeNameOffset).Take(StringArray.Length - AttributeNameOffset).ToArray();

                foreach (var d in cf)
                {
                    if (d != '\0')
                    {
                        AttributeNameCharAry.Add(d);
                    }
                    else return;
                }
            }

            public UnknownData3()
            {
                AttributeNameCharAry = new List<char>();
                AttributeNameOffset = 0;

                Data1 = 0;
                Data2 = 0;
            }

            public override string ToString()
            {
                return AttributeName;
            }
        }

        public char[] DVLE_PropertyNameDataCharArray { get; set; }
        public string DVLE_PropertyNameData => new string(DVLE_PropertyNameDataCharArray);
        public int DVLE_PropertyNameDataOffset { get; set; } //0x4, From : DVLE Header
        public int DVLE_PropertyNameDataLength { get; set; } //0x4


        public string[] DVLE_AttributeNameArray => GetDVLE_PropertyNameDataArray();
        public string[] GetDVLE_PropertyNameDataArray()
        {
            return DVLE_PropertyNameData.Replace('\0', ',').Split(',').ToArray();
        }

        public void ReadDVLE(BinaryReader br, byte[] BOM)
        {
            long DVLEPos = br.BaseStream.Position;

            DVLE_Header = br.ReadChars(4);
            if (new string(DVLE_Header) != "DVLE") throw new Exception("不明なフォーマットです");
            EndianConvert endianConvert = new EndianConvert(BOM);

            DVLE_UnknownByte1 = endianConvert.Convert(br.ReadBytes(4));
            DVLE_UnknownByte4 = endianConvert.Convert(br.ReadBytes(4));

            DVLE_UnknownOffset0 = BitConverter.ToInt32(endianConvert.Convert(br.ReadBytes(4)), 0);

            DVLE_UnknownData6 = BitConverter.ToInt16(endianConvert.Convert(br.ReadBytes(2)), 0);
            DVLE_UnknownData7 = BitConverter.ToInt16(endianConvert.Convert(br.ReadBytes(2)), 0);

            DVLE_UnknownByte8 = endianConvert.Convert(br.ReadBytes(4));

            DVLE_UnknownOffset1 = BitConverter.ToInt32(endianConvert.Convert(br.ReadBytes(4)), 0);
            DVLE_UnknownDataCount1 = BitConverter.ToInt32(endianConvert.Convert(br.ReadBytes(4)), 0);
            if (DVLE_UnknownOffset1 != 0)
            {
                long CurrentPos = br.BaseStream.Position;

                //Move DVLE Header
                br.BaseStream.Position = DVLEPos;

                br.BaseStream.Seek(DVLE_UnknownOffset1, SeekOrigin.Current);

                for (int i = 0; i < DVLE_UnknownDataCount1; i++)
                {
                    UnknownData1 unknownData0 = new UnknownData1();
                    unknownData0.ReadUnknownData0(br, BOM);
                    UnknownData1_List.Add(unknownData0);
                }

                //Leave Position
                br.BaseStream.Position = CurrentPos;
            }

            DVLE_ShaderEntryPointOffset = BitConverter.ToInt32(endianConvert.Convert(br.ReadBytes(4)), 0);
            DVLE_ShaderEntryPointCount = BitConverter.ToInt32(endianConvert.Convert(br.ReadBytes(4)), 0);
            if (DVLE_ShaderEntryPointOffset != 0)
            {
                long CurrentPos = br.BaseStream.Position;

                //Move DVLE Header
                br.BaseStream.Position = DVLEPos;

                br.BaseStream.Seek(DVLE_ShaderEntryPointOffset, SeekOrigin.Current);

                for (int i = 0; i < DVLE_ShaderEntryPointCount; i++)
                {
                    ShaderEntryPoint shaderEntryPoint = new ShaderEntryPoint();
                    shaderEntryPoint.Read_ShaderEntryPoint(br, BOM);
                    ShaderEntryPoint_List.Add(shaderEntryPoint);
                }

                //Leave Position
                br.BaseStream.Position = CurrentPos;
            }

            DVLE_UnknownOffset_2 = BitConverter.ToInt32(endianConvert.Convert(br.ReadBytes(4)), 0);
            DVLE_UnknownDataCount2 = BitConverter.ToInt32(endianConvert.Convert(br.ReadBytes(4)), 0);
            if (DVLE_UnknownOffset_2 != 0)
            {
                long CurrentPos = br.BaseStream.Position;

                //Move DVLE Header
                br.BaseStream.Position = DVLEPos;

                br.BaseStream.Seek(DVLE_UnknownOffset_2, SeekOrigin.Current);

                for (int i = 0; i < DVLE_UnknownDataCount2; i++)
                {
                    UnknownData2 unknownData2 = new UnknownData2();
                    unknownData2.ReadUnknownData2(br, BOM);
                    UnknownData2_List.Add(unknownData2);
                }

                //Leave Position
                br.BaseStream.Position = CurrentPos;
            }

            DVLE_UnknownOffset_3 = BitConverter.ToInt32(endianConvert.Convert(br.ReadBytes(4)), 0);
            DVLE_UnknownDataCount3 = BitConverter.ToInt32(endianConvert.Convert(br.ReadBytes(4)), 0);
            if (DVLE_UnknownOffset_3 != 0)
            {
                long CurrentPos = br.BaseStream.Position;

                //Move DVLE Header
                br.BaseStream.Position = DVLEPos;

                br.BaseStream.Seek(DVLE_UnknownOffset_3, SeekOrigin.Current);

                for (int i = 0; i < DVLE_UnknownDataCount3; i++)
                {
                    UnknownData3 unknownData3 = new UnknownData3();
                    unknownData3.ReadUnknownData3(br, BOM);
                    UnknownData3_List.Add(unknownData3);
                }

                //Leave Position
                br.BaseStream.Position = CurrentPos;
            }

            DVLE_PropertyNameDataOffset = BitConverter.ToInt32(endianConvert.Convert(br.ReadBytes(4)), 0);
            DVLE_PropertyNameDataLength = BitConverter.ToInt32(endianConvert.Convert(br.ReadBytes(4)), 0);
            if (DVLE_PropertyNameDataOffset != 0)
            {
                long CurrentPos = br.BaseStream.Position;

                //Move DVLE Header
                br.BaseStream.Position = DVLEPos;

                br.BaseStream.Seek(DVLE_PropertyNameDataOffset, SeekOrigin.Current);
                DVLE_PropertyNameDataCharArray = br.ReadChars(DVLE_PropertyNameDataLength);

                br.BaseStream.Position = CurrentPos;
            }


            foreach (var item in ShaderEntryPoint_List)
            {
                item.ReadAttributeName(DVLE_PropertyNameDataCharArray);
            }


            foreach (var item in UnknownData3_List)
            {
                item.ReadAttributeName(DVLE_PropertyNameDataCharArray);
            }
        }

        public DVLE()
        {
            DVLE_Header = "DVLE".ToCharArray();
            DVLE_UnknownByte1 = new byte[4];
            DVLE_UnknownByte4 = new byte[4];
            DVLE_UnknownOffset0 = 0;
            DVLE_UnknownData6 = 0;
            DVLE_UnknownData7 = 0;
            DVLE_UnknownByte8 = new byte[4];

            DVLE_UnknownOffset1 = 0;
            DVLE_UnknownDataCount1 = 0;
            UnknownData1_List = new List<UnknownData1>();

            DVLE_ShaderEntryPointOffset = 0;
            DVLE_ShaderEntryPointCount = 0;
            ShaderEntryPoint_List = new List<ShaderEntryPoint>();

            DVLE_UnknownOffset_2 = 0;
            DVLE_UnknownDataCount2 = 0;
            UnknownData2_List = new List<UnknownData2>();

            DVLE_UnknownOffset_3 = 0;
            DVLE_UnknownDataCount3 = 0;
            UnknownData3_List = new List<UnknownData3>();

            DVLE_PropertyNameDataCharArray = new char[0];
            DVLE_PropertyNameDataOffset = 0;
            DVLE_PropertyNameDataLength = 0; //FromStringData
        }
    }
}
