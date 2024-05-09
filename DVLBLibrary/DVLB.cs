using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLBLibrary
{
    /// <summary>
    /// DVLB Format
    /// </summary>
    public class DVLB
    {
        public char[] DVLB_Header { get; set; }
        public int DVLE_DataCount { get; set; }
        public List<DVLEData> DVLE_List { get; set; }
        public class DVLEData
        {
            public int DVLE_Offset { get; set; }
            public DVLE DVLE_DATA { get; set; }

            public void Read_DVLEData(BinaryReader br, byte[] BOM, long Pos)
            {
                EndianConvert endianConvert = new EndianConvert(BOM);
                DVLE_Offset = BitConverter.ToInt32(endianConvert.Convert(br.ReadBytes(4)), 0);
                if (DVLE_Offset != 0)
                {
                    long CurrentPos = br.BaseStream.Position;

                    //Move DVLB Header
                    br.BaseStream.Position = Pos;

                    br.BaseStream.Seek(DVLE_Offset, SeekOrigin.Current);

                    DVLE_DATA.ReadDVLE(br, BOM);

                    //Leave Position
                    br.BaseStream.Position = CurrentPos;
                }
            }

            public DVLEData()
            {
                DVLE_Offset = 0;
                DVLE_DATA = new DVLE();
            }
        }

        public DVLP DVLP { get; set; }

        public Dictionary<string, DVLE> DVLE_Dictionary => GetDVLEDict();
        public Dictionary<string, DVLE> GetDVLEDict()
        {
            string[] strings = DVLP.NameData.GetDVLP_NameDataArray();
            Dictionary<string, DVLE> DVLEDataDict = new Dictionary<string, DVLE>();

            for (int i = 0; i < DVLE_List.Count; i++)
            {
                DVLEDataDict.Add(strings[i], DVLE_List[i].DVLE_DATA);
            }

            return DVLEDataDict;
        }

        public void ReadDVLB(BinaryReader br, byte[] BOM)
        {
            long DVLBPos = br.BaseStream.Position;

            DVLB_Header = br.ReadChars(4);
            if (new string(DVLB_Header) != "DVLB") throw new Exception("不明なフォーマットです");
            EndianConvert endianConvert = new EndianConvert(BOM);

            DVLE_DataCount = BitConverter.ToInt32(endianConvert.Convert(br.ReadBytes(4)), 0);

            for (int i = 0; i < DVLE_DataCount; i++)
            {
                DVLEData dVLEData = new DVLEData();
                dVLEData.Read_DVLEData(br, BOM, DVLBPos);
                DVLE_List.Add(dVLEData);
            }

            DVLP.ReadDVLP(br, BOM);
        }

        public DVLB()
        {
            DVLB_Header = "DVLB".ToCharArray();
            DVLE_DataCount = 0;
            DVLE_List = new List<DVLEData>();

            DVLP = new DVLP();
        }
    }
}
