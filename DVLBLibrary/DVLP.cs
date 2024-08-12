using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace DVLBLibrary
{
    /// <summary>
    /// DVLP
    /// </summary>
    public class DVLP
    {
        public char[] DVLP_Header { get; set; } //char(0x4)
        public byte[] Version { get; set; } //0x4
        public int CompiledShaderBinaryBlobOffset { get; set; } //From : DVLP Header
        public int CompiledShaderBinaryBlobCount { get; set; }
        public List<CompiledShaderBinaryBlob> CompiledShaderBinaryBlobList { get; set; }
        public class CompiledShaderBinaryBlob
        {
            public byte Data0 { get; set; }
            public byte Data1 { get; set; }
            public byte Data2 { get; set; }
            public byte Data3 { get; set; }

            public void ReadCompiledShaderBinaryBlob(BinaryReader br)
            {
                Data0 = br.ReadByte();
                Data1 = br.ReadByte();
                Data2 = br.ReadByte();
                Data3 = br.ReadByte();
            }

            public CompiledShaderBinaryBlob(byte d0, byte d1, byte d2, byte d3)
            {
                Data0 = d0;
                Data1 = d1;
                Data2 = d2;
                Data3 = d3;
            }

            public CompiledShaderBinaryBlob()
            {
                Data0 = 0;
                Data1 = 0;
                Data2 = 0;
                Data3 = 0;
            }

            public override string ToString()
            {
                return "CompiledShaderBinaryBlob";
            }
        }

        public int OperandDescriptorTableOffset { get; set; }
        public int OperandDescriptorTableCount { get; set; }
        public List<OperandDescriptorTable> OperandDescriptorTables { get; set; }
        public class OperandDescriptorTable
        {
            public UnknownByteData Unknown_ByteData { get; set; }
            public class UnknownByteData
            {
                public byte Data0 { get; set; }
                public byte Data1 { get; set; }
                public byte Data2 { get; set; }
                public byte Data3 { get; set; }

                public void ReadUnknownData(BinaryReader br)
                {
                    Data0 = br.ReadByte();
                    Data1 = br.ReadByte();
                    Data2 = br.ReadByte();
                    Data3 = br.ReadByte();
                }

                public UnknownByteData(byte d0, byte d1, byte d2, byte d3)
                {
                    Data0 = d0;
                    Data1 = d1;
                    Data2 = d2;
                    Data3 = d3;
                }
            }

            public int UnknownData0 { get; set; } //BitData (?)

            public void ReadTableData(BinaryReader br, byte[] BOM)
            {
                EndianConvert endianConvert = new EndianConvert(BOM);
                Unknown_ByteData.ReadUnknownData(br);
                UnknownData0 = BitConverter.ToInt32(endianConvert.Convert(br.ReadBytes(4)), 0);
            }

            public OperandDescriptorTable()
            {
                Unknown_ByteData = new UnknownByteData(0x00, 0x00, 0x00, 0x00);
                UnknownData0 = 0;
            }

            public override string ToString()
            {
                return "OperandDescriptorTable";
            }
        }

        public DVLP_NameData NameData { get; set; }
        public class DVLP_NameData
        {
            public char[] NameCharArray { get; set; }

            public Start StringStart { get; set; }
            public class Start
            {
                public int Offset { get; set; } //From : DVLP Header, 
                public int NameStartOffset { get; set; }

                public void Read_StartData(BinaryReader br, byte[] BOM)
                {
                    EndianConvert endianConvert = new EndianConvert(BOM);
                    Offset = BitConverter.ToInt32(endianConvert.Convert(br.ReadBytes(4)), 0);
                    NameStartOffset = BitConverter.ToInt32(endianConvert.Convert(br.ReadBytes(4)), 0);
                }

                /// <summary>
                /// 
                /// </summary>
                /// <param name="Offset">Endと同じオフセットを使用する</param>
                /// <param name="Length">Default : 0</param>
                public Start(int Offset, int Length)
                {
                    this.Offset = Offset;
                    NameStartOffset = Length;
                }
            }

            public End StringEnd { get; set; }
            public class End
            {
                public int Offset { get; set; } //From : DVLP Header, 
                public int NameEndOffset { get; set; }

                public void Read_EndData(BinaryReader br, byte[] BOM)
                {
                    EndianConvert endianConvert = new EndianConvert(BOM);
                    Offset = BitConverter.ToInt32(endianConvert.Convert(br.ReadBytes(4)), 0);
                    NameEndOffset = BitConverter.ToInt32(endianConvert.Convert(br.ReadBytes(4)), 0);
                }

                /// <summary>
                /// 
                /// </summary>
                /// <param name="Offset">Startと同じオフセットを使用する</param>
                /// <param name="Length">StringLength</param>
                public End(int Offset, int Length)
                {
                    this.Offset = Offset;
                    NameEndOffset = Length;
                }
            }

            /// <summary>
            /// Delete the terminating string (\0, NULL) that fills in the excess string data.
            /// </summary>
            /// <returns></returns>
            public char[] FixedNameCharArray()
            {
                return NameCharArray.Take(StringEnd.NameEndOffset - 1).ToArray();
            }

            public int CalculateNULLPadding()
            {
                return NameCharArray.Length % 4;
            }

            public char[] CreateNULLPaddingCharArray()
            {
                return Enumerable.Repeat('\0', CalculateNULLPadding()).ToArray();
            }


            public string[] GetDVLP_NameDataArray()
            {
                string NoSplitedString = new string(FixedNameCharArray());
                string[] SplitedStringArray = NoSplitedString.Split('\0').ToArray();

                return SplitedStringArray;
            }

            public char[][] GetDVLP_NameDataCharArrays()
            {
                string NoSplitedString = new string(FixedNameCharArray());
                string[] SplitedStringArray = NoSplitedString.Split('\0').ToArray();

                List<char[]> NameCharArrayList = SplitedStringArray.ToList().Select(x => x.ToCharArray()).ToList();

                return NameCharArrayList.ToArray();
            }

            public void Read_NameData(BinaryReader br, byte[] BOM, long DVLPPos)
            {
                StringStart.Read_StartData(br, BOM);
                StringEnd.Read_EndData(br, BOM);

                if ((StringStart.Offset != 0 && StringEnd.Offset != 0) == true)
                {
                    #region Start
                    long CurrentPos0 = br.BaseStream.Position;

                    //Move DVLP Header
                    br.BaseStream.Position = DVLPPos;

                    br.BaseStream.Seek(StringStart.Offset, SeekOrigin.Current);

                    long StartPos = br.BaseStream.Position;

                    br.BaseStream.Position = CurrentPos0;
                    #endregion

                    #region End
                    long CurrentPos1 = br.BaseStream.Position;

                    //Move DVLP Header
                    br.BaseStream.Position = DVLPPos;

                    br.BaseStream.Seek(StringEnd.Offset, SeekOrigin.Current);

                    long EndPos = br.BaseStream.Position + StringEnd.NameEndOffset;

                    br.BaseStream.Position = CurrentPos1;
                    #endregion

                    int charArrayLength = (int)(EndPos - StartPos);

                    #region Read
                    long CurrentPos2 = br.BaseStream.Position;

                    //Move DVLP Header
                    br.BaseStream.Position = DVLPPos;

                    br.BaseStream.Seek(StringStart.Offset, SeekOrigin.Current);

                    NameCharArray = br.ReadChars(charArrayLength);

                    br.BaseStream.Position = CurrentPos2;
                    #endregion
                }
            }

            public DVLP_NameData(int NameOffset, int StartPos, int EndPos, string str)
            {
                NameCharArray = str.ToArray();
                StringStart = new Start(NameOffset, StartPos);
                StringStart = new Start(NameOffset, EndPos);
            }

            public DVLP_NameData()
            {
                NameCharArray = new List<char>().ToArray();
                StringStart = new Start(0, 0);
                StringEnd = new End(0, 0);
            }
        }

        public void ReadDVLP(BinaryReader br, byte[] BOM)
        {
            long DVLPPos = br.BaseStream.Position;

            DVLP_Header = br.ReadChars(4);
            if (new string(DVLP_Header) != "DVLP") throw new Exception("不明なフォーマットです");
            EndianConvert endianConvert = new EndianConvert(BOM);
            Version = endianConvert.Convert(br.ReadBytes(4));

            CompiledShaderBinaryBlobOffset = BitConverter.ToInt32(endianConvert.Convert(br.ReadBytes(4)), 0);
            CompiledShaderBinaryBlobCount = BitConverter.ToInt32(endianConvert.Convert(br.ReadBytes(4)), 0);
            if (CompiledShaderBinaryBlobOffset != 0)
            {
                long CurrentPos = br.BaseStream.Position;

                //Move DVLP Header
                br.BaseStream.Position = DVLPPos;

                br.BaseStream.Seek(CompiledShaderBinaryBlobOffset, SeekOrigin.Current);

                for (int i = 0; i < CompiledShaderBinaryBlobCount; i++)
                {
                    CompiledShaderBinaryBlob compiledShaderBinaryBlob = new CompiledShaderBinaryBlob();
                    compiledShaderBinaryBlob.ReadCompiledShaderBinaryBlob(br);
                    CompiledShaderBinaryBlobList.Add(compiledShaderBinaryBlob);
                }

                //Leave Position
                br.BaseStream.Position = CurrentPos;
            }

            OperandDescriptorTableOffset = BitConverter.ToInt32(endianConvert.Convert(br.ReadBytes(4)), 0);
            OperandDescriptorTableCount = BitConverter.ToInt32(endianConvert.Convert(br.ReadBytes(4)), 0);
            if (OperandDescriptorTableOffset != 0)
            {
                long CurrentPos = br.BaseStream.Position;

                //Move DVLP Header
                br.BaseStream.Position = DVLPPos;

                br.BaseStream.Seek(OperandDescriptorTableOffset, SeekOrigin.Current);

                for (int i = 0; i < OperandDescriptorTableCount; i++)
                {
                    OperandDescriptorTable operandDescriptorTable = new OperandDescriptorTable();
                    operandDescriptorTable.ReadTableData(br, BOM);
                    OperandDescriptorTables.Add(operandDescriptorTable);
                }

                //Leave Position
                br.BaseStream.Position = CurrentPos;
            }

            NameData.Read_NameData(br, BOM, DVLPPos);
        }

        public void WriteDVLP(BinaryWriter bw, byte[] BOM)
        {
            bw.Write(DVLP_Header);
            bw.Write(Version);

            //...
        }

        public DVLP()
        {
            DVLP_Header = "DVLP".ToCharArray();
            Version = new byte[4];
            CompiledShaderBinaryBlobOffset = 0;
            CompiledShaderBinaryBlobCount = 0;
            CompiledShaderBinaryBlobList = new List<CompiledShaderBinaryBlob>();

            OperandDescriptorTableOffset = 0;
            OperandDescriptorTableCount = 0;
            OperandDescriptorTables = new List<OperandDescriptorTable>();

            NameData = new DVLP_NameData();
        }
    }

}
