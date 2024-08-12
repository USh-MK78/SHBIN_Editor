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
        public byte[] Version { get; set; } //0x2

        public byte ShaderTypeValue { get; set; }
        public EnumData.DVLE.ShaderType Shader_Type
        {
            get
            {
                return (EnumData.DVLE.ShaderType)Enum.ToObject(typeof(EnumData.DVLE.ShaderType), ShaderTypeValue);
            }
        }

        public bool IsMergeVertexAndGeometryShaderOutmap { get; set; } //0x1

        public int ExecutableFileBinaryBlobMainOffset { get; set; } //0x4
        public int ExecutableProgramBinaryBlobEndMainOffset { get; set; } //0x4
        public short DVLE_InputRegisterBitmask { get; set; } //0x2
        public short DVLE_OutputRegisterBitmask { get; set; } //0x2

        public byte GeometryShaderTypeValue { get; set; }
        public byte NumOfFixedSizePrimitiveVertexShaderArrayStartIndex { get; set; }
        public byte NumOfFullDefinedVariableSizePrimitiveVertexShaderArray { get; set; }
        public byte NumOfVerticesFixedSizePrimitiveVertexShaderArray { get; set; }

        public int ConstantTableOffset { get; set; } //0x4, From : DVLE Header
        public int ConstantTableCount { get; set; } //0x4
        public List<ConstantTable> ConstantTable_List { get; set; }
        public class ConstantTable
        {
            public short ConstantTypeValue { get; set; }
            public EnumData.DVLE.ConstantType ConstantType
            {
                get
                {
                    return (EnumData.DVLE.ConstantType)Enum.ToObject(typeof(EnumData.DVLE.ConstantType), ConstantTypeValue);
                }
            }

            public short ConstantRegisterID { get; set; }

            public bool Boolean { get; set; }
            public StructData.DVLE.IntVector4 ivec4 { get; set; }
            public StructData.DVLE.FloatVector4 vec4 { get; set; }

            public void ReadUnknownData0(BinaryReader br, byte[] BOM)
            {
                EndianConvert endianConvert = new EndianConvert(BOM);
                ConstantTypeValue = BitConverter.ToInt16(endianConvert.Convert(br.ReadBytes(2)), 0);
                ConstantRegisterID = BitConverter.ToInt16(endianConvert.Convert(br.ReadBytes(2)), 0);

                if (ConstantType == EnumData.DVLE.ConstantType.Boolean)
                {
                    Boolean = Convert.ToBoolean(BitConverter.ToInt32(endianConvert.Convert(br.ReadBytes(4)), 0));
                }
                else if (ConstantType == EnumData.DVLE.ConstantType.IVec4)
                {
                    ivec4 = new StructData.DVLE.IntVector4(br.ReadByte(), br.ReadByte(), br.ReadByte(), br.ReadByte());
                }
                else if (ConstantType == EnumData.DVLE.ConstantType.Vec4)
                {
                    float X = BitConverter.ToSingle(endianConvert.Convert(br.ReadBytes(4)), 0);
                    float Y = BitConverter.ToSingle(endianConvert.Convert(br.ReadBytes(4)), 0);
                    float Z = BitConverter.ToSingle(endianConvert.Convert(br.ReadBytes(4)), 0);
                    float W = BitConverter.ToSingle(endianConvert.Convert(br.ReadBytes(4)), 0);

                    vec4 = new StructData.DVLE.FloatVector4(X, Y, Z, W);
                }
            }

            public ConstantTable()
            {
                ConstantTypeValue = 0;
                ConstantRegisterID = 0;

                Boolean = false;
                ivec4 = new StructData.DVLE.IntVector4();
                vec4 = new StructData.DVLE.FloatVector4();
            }

            public override string ToString()
            {
                return "ConstantTable : [ Register ID : " + ConstantRegisterID + "] [Type : " + ConstantType.ToString() + "]";
            }
        }

        public int LabelTableOffset { get; set; } //0x4, From : DVLE Header
        public int LabelTableCount { get; set; } //0x4
        public List<LabelTable> LabelTable_List { get; set; }
        public class LabelTable
        {
            public short ID { get; set; }
            public short UnknownData_1 { get; set; }
            public uint LabelLocationOffset { get; set; }
            public uint LabelLocationSize { get; set; } //0x4, 0xFFFFFFFF = NoSize

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

            public void Read_LabelTable(BinaryReader br, byte[] BOM)
            {
                EndianConvert endianConvert = new EndianConvert(BOM);
                ID = BitConverter.ToInt16(endianConvert.Convert(br.ReadBytes(2)), 0);
                UnknownData_1 = BitConverter.ToInt16(endianConvert.Convert(br.ReadBytes(2)), 0);
                LabelLocationOffset = BitConverter.ToUInt32(endianConvert.Convert(br.ReadBytes(4)), 0);
                LabelLocationSize = BitConverter.ToUInt32(endianConvert.Convert(br.ReadBytes(4)), 0);
                AttributeNameOffset = BitConverter.ToInt32(endianConvert.Convert(br.ReadBytes(4)), 0);
            }

            public LabelTable()
            {
                ID = 0;
                UnknownData_1 = 0;
                LabelLocationOffset = 0;
                LabelLocationSize = 0xFFFFFFFF;
                AttributeNameOffset = 0;
                AttributeNameCharAry = new List<char>();
            }

            public override string ToString()
            {
                return "LabelTable : [ Register ID : " + ID + "] [Name : " + AttributeName + "]";
            }
        }

        public int DVLE_OutputRegisterTableOffset { get; set; } //0x4, From : DVLE Header
        public int DVLE_OutputRegisterTableCount { get; set; } //0x4
        public List<OutputRegisterTable> OutputRegisterTable_List { get; set; }
        public class OutputRegisterTable
        {
            public short OutputTypeValue { get; set; }
            public EnumData.DVLE.OutputTable_OutputType OutputType
            {
                get
                {
                    return (EnumData.DVLE.OutputTable_OutputType)Enum.ToObject(typeof(EnumData.DVLE.OutputTable_OutputType), OutputTypeValue);
                }
            }

            public short RegisterID { get; set; }
            public short OutputAttributeComponentMask { get; set; }
            public short UnknownData0 { get; set; }

            public void ReadOutputRegisterTable(BinaryReader br, byte[] BOM)
            {
                EndianConvert endianConvert = new EndianConvert(BOM);
                OutputTypeValue = BitConverter.ToInt16(endianConvert.Convert(br.ReadBytes(2)), 0);
                RegisterID = BitConverter.ToInt16(endianConvert.Convert(br.ReadBytes(2)), 0);
                OutputAttributeComponentMask = BitConverter.ToInt16(endianConvert.Convert(br.ReadBytes(2)), 0);
                UnknownData0 = BitConverter.ToInt16(endianConvert.Convert(br.ReadBytes(2)), 0);
            }

            public OutputRegisterTable()
            {
                OutputTypeValue = 0;
                RegisterID = 0;
                OutputAttributeComponentMask = 0;
                UnknownData0 = 0;
            }

            public override string ToString()
            {
                return "OutputRegisterTable : [ Register ID : " + RegisterID + "] [OutputType : " + OutputType.ToString() + "]";
            }
        }

        public int DVLE_UniformTableOffset { get; set; } //0x4, From : DVLE Header
        public int DVLE_UniformTableCount { get; set; } //0x4
        public List<UniformTable> UniformTable_List { get; set; } //InputRegs (?)
        public class UniformTable
        {
            public int AttributeNameOffset { get; set; } //From : StringArray
            public List<char> AttributeNameCharAry { get; set; }
            public string AttributeName => new string(AttributeNameCharAry.ToArray());

            public short StartOfTheUniformRegisterIndex { get; set; }
            public EnumData.DVLE.RegisterSpaceType StartOfTheUniformRegisterSpaceType
            {
                get
                {
                    return (EnumData.DVLE.RegisterSpaceType)Enum.ToObject(typeof(EnumData.DVLE.RegisterSpaceType), StartOfTheUniformRegisterIndex);
                }
            }

            public short EndOfTheUniformRegisterIndex { get; set; }
            public EnumData.DVLE.RegisterSpaceType EndOfTheUniformRegisterSpaceType
            {
                get
                {
                    return (EnumData.DVLE.RegisterSpaceType)Enum.ToObject(typeof(EnumData.DVLE.RegisterSpaceType), EndOfTheUniformRegisterIndex);
                }
            }

            public void ReadUniformTable(BinaryReader br, byte[] BOM)
            {
                EndianConvert endianConvert = new EndianConvert(BOM);
                AttributeNameOffset = BitConverter.ToInt32(endianConvert.Convert(br.ReadBytes(4)), 0);

                StartOfTheUniformRegisterIndex = BitConverter.ToInt16(endianConvert.Convert(br.ReadBytes(2)), 0);
                EndOfTheUniformRegisterIndex = BitConverter.ToInt16(endianConvert.Convert(br.ReadBytes(2)), 0);
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

            public UniformTable()
            {
                AttributeNameCharAry = new List<char>();
                AttributeNameOffset = 0;

                StartOfTheUniformRegisterIndex = 0;
                EndOfTheUniformRegisterIndex = 0;
            }

            public override string ToString()
            {
                return "UniformTable : [Name : " + AttributeName + "]";
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

            Version = endianConvert.Convert(br.ReadBytes(2));

            ShaderTypeValue = br.ReadByte();
            IsMergeVertexAndGeometryShaderOutmap = Convert.ToBoolean(br.ReadByte());

            ExecutableFileBinaryBlobMainOffset = BitConverter.ToInt32(endianConvert.Convert(br.ReadBytes(4)), 0);

            ExecutableProgramBinaryBlobEndMainOffset = BitConverter.ToInt32(endianConvert.Convert(br.ReadBytes(4)), 0);

            DVLE_InputRegisterBitmask = BitConverter.ToInt16(endianConvert.Convert(br.ReadBytes(2)), 0);
            DVLE_OutputRegisterBitmask = BitConverter.ToInt16(endianConvert.Convert(br.ReadBytes(2)), 0);

            GeometryShaderTypeValue = br.ReadByte();
            NumOfFixedSizePrimitiveVertexShaderArrayStartIndex = br.ReadByte();
            NumOfFullDefinedVariableSizePrimitiveVertexShaderArray = br.ReadByte();
            NumOfVerticesFixedSizePrimitiveVertexShaderArray = br.ReadByte();

            ConstantTableOffset = BitConverter.ToInt32(endianConvert.Convert(br.ReadBytes(4)), 0);
            ConstantTableCount = BitConverter.ToInt32(endianConvert.Convert(br.ReadBytes(4)), 0);
            if (ConstantTableOffset != 0)
            {
                long CurrentPos = br.BaseStream.Position;

                //Move DVLE Header
                br.BaseStream.Position = DVLEPos;

                br.BaseStream.Seek(ConstantTableOffset, SeekOrigin.Current);

                for (int i = 0; i < ConstantTableCount; i++)
                {
                    ConstantTable unknownData0 = new ConstantTable();
                    unknownData0.ReadUnknownData0(br, BOM);
                    ConstantTable_List.Add(unknownData0);
                }

                //Leave Position
                br.BaseStream.Position = CurrentPos;
            }

            LabelTableOffset = BitConverter.ToInt32(endianConvert.Convert(br.ReadBytes(4)), 0);
            LabelTableCount = BitConverter.ToInt32(endianConvert.Convert(br.ReadBytes(4)), 0);
            if (LabelTableOffset != 0)
            {
                long CurrentPos = br.BaseStream.Position;

                //Move DVLE Header
                br.BaseStream.Position = DVLEPos;

                br.BaseStream.Seek(LabelTableOffset, SeekOrigin.Current);

                for (int i = 0; i < LabelTableCount; i++)
                {
                    LabelTable shaderEntryPoint = new LabelTable();
                    shaderEntryPoint.Read_LabelTable(br, BOM);
                    LabelTable_List.Add(shaderEntryPoint);
                }

                //Leave Position
                br.BaseStream.Position = CurrentPos;
            }

            DVLE_OutputRegisterTableOffset = BitConverter.ToInt32(endianConvert.Convert(br.ReadBytes(4)), 0);
            DVLE_OutputRegisterTableCount = BitConverter.ToInt32(endianConvert.Convert(br.ReadBytes(4)), 0);
            if (DVLE_OutputRegisterTableOffset != 0)
            {
                long CurrentPos = br.BaseStream.Position;

                //Move DVLE Header
                br.BaseStream.Position = DVLEPos;

                br.BaseStream.Seek(DVLE_OutputRegisterTableOffset, SeekOrigin.Current);

                for (int i = 0; i < DVLE_OutputRegisterTableCount; i++)
                {
                    OutputRegisterTable unknownData2 = new OutputRegisterTable();
                    unknownData2.ReadOutputRegisterTable(br, BOM);
                    OutputRegisterTable_List.Add(unknownData2);
                }

                //Leave Position
                br.BaseStream.Position = CurrentPos;
            }

            DVLE_UniformTableOffset = BitConverter.ToInt32(endianConvert.Convert(br.ReadBytes(4)), 0);
            DVLE_UniformTableCount = BitConverter.ToInt32(endianConvert.Convert(br.ReadBytes(4)), 0);
            if (DVLE_UniformTableOffset != 0)
            {
                long CurrentPos = br.BaseStream.Position;

                //Move DVLE Header
                br.BaseStream.Position = DVLEPos;

                br.BaseStream.Seek(DVLE_UniformTableOffset, SeekOrigin.Current);

                for (int i = 0; i < DVLE_UniformTableCount; i++)
                {
                    UniformTable unknownData3 = new UniformTable();
                    unknownData3.ReadUniformTable(br, BOM);
                    UniformTable_List.Add(unknownData3);
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


            foreach (var item in LabelTable_List)
            {
                item.ReadAttributeName(DVLE_PropertyNameDataCharArray);
            }


            foreach (var item in UniformTable_List)
            {
                item.ReadAttributeName(DVLE_PropertyNameDataCharArray);
            }
        }

        public DVLE()
        {
            DVLE_Header = "DVLE".ToCharArray();
            Version = new byte[2];
            ShaderTypeValue = 0;
            IsMergeVertexAndGeometryShaderOutmap = false;
            ExecutableFileBinaryBlobMainOffset = 0;
            ExecutableProgramBinaryBlobEndMainOffset = 0;
            DVLE_InputRegisterBitmask = 0;
            DVLE_OutputRegisterBitmask = 0;
            GeometryShaderTypeValue = 0;
            NumOfFixedSizePrimitiveVertexShaderArrayStartIndex = 0;
            NumOfFullDefinedVariableSizePrimitiveVertexShaderArray = 0;
            NumOfVerticesFixedSizePrimitiveVertexShaderArray = 0;

            ConstantTableOffset = 0;
            ConstantTableCount = 0;
            ConstantTable_List = new List<ConstantTable>();

            LabelTableOffset = 0;
            LabelTableCount = 0;
            LabelTable_List = new List<LabelTable>();

            DVLE_OutputRegisterTableOffset = 0;
            DVLE_OutputRegisterTableCount = 0;
            OutputRegisterTable_List = new List<OutputRegisterTable>();

            DVLE_UniformTableOffset = 0;
            DVLE_UniformTableCount = 0;
            UniformTable_List = new List<UniformTable>();

            DVLE_PropertyNameDataCharArray = new char[0];
            DVLE_PropertyNameDataOffset = 0;
            DVLE_PropertyNameDataLength = 0; //FromStringData
        }
    }
}
