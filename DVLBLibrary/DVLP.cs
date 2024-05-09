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
        public byte[] UnkByte1 { get; set; } //0x4
        public int UnknownDataOffset0 { get; set; } //From : DVLP Header
        public int DVLP_UnknownDataCount { get; set; }

        public List<UnknownData> UnknownDataList { get; set; }
        public class UnknownData
        {
            public byte Data0 { get; set; }
            public byte Data1 { get; set; }
            public byte Data2 { get; set; }
            public byte Data3 { get; set; }

            public Color ColorRGBA
            {
                get
                {
                    return GetColor();
                }
                set
                {
                    Data3 = value.A;
                    Data2 = value.R;
                    Data1 = value.G;
                    Data0 = value.B;
                }
            }
            public Color GetColor()
            {
                return Color.FromArgb(Data3, Data2, Data1, Data0);
            }

            public void ReadUnknowndata(BinaryReader br)
            {
                Data0 = br.ReadByte();
                Data1 = br.ReadByte();
                Data2 = br.ReadByte();
                Data3 = br.ReadByte();
            }

            public UnknownData(byte d0, byte d1, byte d2, byte d3)
            {
                Data0 = d0;
                Data1 = d1;
                Data2 = d2;
                Data3 = d3;
            }

            public UnknownData()
            {
                Data0 = 0;
                Data1 = 0;
                Data2 = 0;
                Data3 = 0;
            }
        }

        public int TableDataOffset { get; set; }
        public int TableDataCount { get; set; }
        public List<Table> Tables { get; set; }
        public class Table
        {
            public UnknownByteData Unknown_ByteData { get; set; }
            public class UnknownByteData
            {
                public byte Data0 { get; set; }
                public byte Data1 { get; set; }
                public byte Data2 { get; set; }
                public byte Data3 { get; set; }

                public void ReadUnknowndata(BinaryReader br)
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

            public int UnknownData0 { get; set; }

            public void ReadTableData(BinaryReader br, byte[] BOM)
            {
                EndianConvert endianConvert = new EndianConvert(BOM);
                Unknown_ByteData.ReadUnknowndata(br);
                UnknownData0 = BitConverter.ToInt32(endianConvert.Convert(br.ReadBytes(4)), 0);
            }

            public Table()
            {
                Unknown_ByteData = new UnknownByteData(0x00, 0x00, 0x00, 0x00);
                UnknownData0 = 0;
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
            UnkByte1 = endianConvert.Convert(br.ReadBytes(4));

            UnknownDataOffset0 = BitConverter.ToInt32(endianConvert.Convert(br.ReadBytes(4)), 0);
            DVLP_UnknownDataCount = BitConverter.ToInt32(endianConvert.Convert(br.ReadBytes(4)), 0);
            if (UnknownDataOffset0 != 0)
            {
                long CurrentPos = br.BaseStream.Position;

                //Move DVLP Header
                br.BaseStream.Position = DVLPPos;

                br.BaseStream.Seek(UnknownDataOffset0, SeekOrigin.Current);

                for (int i = 0; i < DVLP_UnknownDataCount; i++)
                {
                    UnknownData unknownData = new UnknownData();
                    unknownData.ReadUnknowndata(br);
                    UnknownDataList.Add(unknownData);
                }

                //Leave Position
                br.BaseStream.Position = CurrentPos;
            }

            TableDataOffset = BitConverter.ToInt32(endianConvert.Convert(br.ReadBytes(4)), 0);
            TableDataCount = BitConverter.ToInt32(endianConvert.Convert(br.ReadBytes(4)), 0);
            if (TableDataOffset != 0)
            {
                long CurrentPos = br.BaseStream.Position;

                //Move DVLP Header
                br.BaseStream.Position = DVLPPos;

                br.BaseStream.Seek(TableDataOffset, SeekOrigin.Current);

                for (int i = 0; i < TableDataCount; i++)
                {
                    Table table = new Table();
                    table.ReadTableData(br, BOM);
                    Tables.Add(table);
                }

                //Leave Position
                br.BaseStream.Position = CurrentPos;
            }

            NameData.Read_NameData(br, BOM, DVLPPos);
        }

        public void WriteDVLP(BinaryWriter bw, byte[] BOM)
        {
            bw.Write(DVLP_Header);
            bw.Write(UnkByte1);

            //...
        }

        public DVLP()
        {
            DVLP_Header = "DVLP".ToCharArray();
            UnkByte1 = new byte[4];
            UnknownDataOffset0 = 0;
            DVLP_UnknownDataCount = 0;
            UnknownDataList = new List<UnknownData>();

            TableDataOffset = 0;
            TableDataCount = 0;
            Tables = new List<Table>();

            NameData = new DVLP_NameData();
        }
    }

}
