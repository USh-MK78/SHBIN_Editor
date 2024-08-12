using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLBLibrary;

namespace SHBIN_Editor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public DVLB DVLB_Format { get; set; } = new DVLB();
        public List<List<string>> DVLEPropDataList = new List<List<string>>();

        //public Dictionary<string, DVLE> DVLBDataDict { get; set; } = new Dictionary<string, DVLE>();

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Open_DVLB(object sender, EventArgs e)
        {
            OpenFileDialog OFD_DVLB = new OpenFileDialog()
            {
                Title = "binファイルを開く",
                InitialDirectory = Environment.CurrentDirectory,
                Filter = "bin file|*.bin|Shader binary file|*.shbin"
            };
            if (OFD_DVLB.ShowDialog() == DialogResult.OK)
            {


                FileStream fs = new FileStream(OFD_DVLB.FileName, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);

                DVLB_Format.ReadDVLB(br, new byte[] { 0xFF, 0xFE }); //LE Mode

                br.Close();
                fs.Close();

                int Count1 = 0;
                int Count2 = 0;
                int Count3 = 0;
                int Count4 = 0;

                foreach (var item in DVLB_Format.DVLP.OperandDescriptorTables)
                {
                    if (item.UnknownData0 == 3) Count1++;
                    else if (item.UnknownData0 == 6) Count2++;
                    else if (item.UnknownData0 == 7) Count3++;
                    else if (item.UnknownData0 == 15) Count4++;
                }

                for (int i = 0; i < DVLB_Format.DVLP.CompiledShaderBinaryBlobList.Count; i++)
                {
                    CompiledShaderBinaryBlobListBox.Items.Add("CompiledShaderBinaryBlob " + i);
                }

                string[] dataAry = DVLB_Format.DVLP.NameData.GetDVLP_NameDataArray();
                DVLE_ListBox.Items.AddRange(dataAry);


                for (int TableDataCount = 0; TableDataCount < DVLB_Format.DVLP.OperandDescriptorTableCount; TableDataCount++)
                {
                    OperandDescriptorListBox.Items.Add("OperandDescriptorTable " + TableDataCount);
                }

                OperandDescriptorTable_PropertyGrid.SelectedObject = DVLB_Format.DVLP.OperandDescriptorTables[0];


                foreach (var item in DVLB_Format.DVLE_List)
                {
                    List<string> Data = new List<string>();

                    foreach (var d in item.DVLE_DATA.DVLE_AttributeNameArray)
                    {
                        Data.Add(d);
                    }

                    DVLEPropDataList.Add(Data);
                }
            }
            else return;
        }


        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            OperandDescriptorTable_PropertyGrid.SelectedObject = DVLB_Format.DVLP.OperandDescriptorTables[OperandDescriptorListBox.SelectedIndex];
        }

        private void DVLE_LIstBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            DVLE_Main_PropertyGrid.SelectedObject = DVLB_Format.DVLE_Dictionary[DVLE_ListBox.Text];
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            CompiledShaderBinaryBlobPropertyGrid.SelectedObject = DVLB_Format.DVLP.CompiledShaderBinaryBlobList[CompiledShaderBinaryBlobListBox.SelectedIndex];
        }
    }
}
