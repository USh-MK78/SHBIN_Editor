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
                InitialDirectory = @"C:\Users\User\Desktop",
                Filter = "bin file|*.bin|Shader binary file|*.shbin"
            };

            if (OFD_DVLB.ShowDialog() != DialogResult.OK) return;


            FileStream fs = new FileStream(OFD_DVLB.FileName, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);

            DVLB_Format.ReadDVLB(br, new byte[] { 0xFF, 0xFE }); //LE Mode

            br.Close();
            fs.Close();


            int Count1 = 0;
            int Count2 = 0;
            int Count3 = 0;
            int Count4 = 0;

            foreach (var item in DVLB_Format.DVLP.Tables)
            {
                if (item.UnknownData0 == 3) Count1++;
                else if (item.UnknownData0 == 6) Count2++;
                else if (item.UnknownData0 == 7) Count3++;
                else if (item.UnknownData0 == 15) Count4++;
            }



            //foreach (var f in DVLB_Format.DVLP.UnknownDataList)
            //{
            //    listBox1.Items.Add(f);
            //}

            for (int i = 0; i < DVLB_Format.DVLP.UnknownDataList.Count; i++)
            {
                listBox1.Items.Add("UnknownData " + i);
            }


            string[] dataAry = DVLB_Format.DVLP.NameData.GetDVLP_NameDataArray();
            DVLE_LIstBox.Items.AddRange(dataAry);

            //char[][] chars = DVLB_Format.DVLP.NameData.GetDVLP_NameDataCharArrays();

            for (int TableDataCount = 0; TableDataCount < DVLB_Format.DVLP.TableDataCount; TableDataCount++)
            {
                listBox2.Items.Add("Table" + TableDataCount);
            }

            propertyGrid1.SelectedObject = DVLB_Format.DVLP.Tables[0];

            
            foreach (var item in DVLB_Format.DVLE_List)
            {
                List<string> Data = new List<string>();

                foreach (var d in item.DVLE_DATA.DVLE_AttributeNameArray)
                {
                    Data.Add(d);
                }

                DVLEPropDataList.Add(Data);
            }

            ////PropertyName
            //for (int i = 0; i < DVLEPropDataList[0].Count; i++)
            //{
            //    textBox1.Text += DVLEPropDataList[0][i] + "\r\n";
            //}
        }


        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {


            propertyGrid1.SelectedObject = DVLB_Format.DVLP.Tables[listBox2.SelectedIndex];
        }

        //private void DVLB_LIstBox_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    //listBox2.Items.Clear();
        //    //for (int TableDataCount = 0; TableDataCount < DVLB_Format.DVLP.TableDataCount; TableDataCount++)
        //    //{
        //    //    listBox2.Items.Add("Table" + TableDataCount);
        //    //}

        //    textBox1.Text = "";
        //    for (int i = 0; i < DVLEPropDataList[DVLE_LIstBox.SelectedIndex].Count; i++)
        //    {
        //        textBox1.Text += DVLEPropDataList[DVLE_LIstBox.SelectedIndex][i] + "\r\n";
        //    }

        //    DVLE_Main_PropertyGrid.SelectedObject = DVLB_Format.DVLE_Dictionary[DVLE_LIstBox.Text];
        //}

        private void DVLE_LIstBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //listBox2.Items.Clear();
            //for (int TableDataCount = 0; TableDataCount < DVLB_Format.DVLP.TableDataCount; TableDataCount++)
            //{
            //    listBox2.Items.Add("Table" + TableDataCount);
            //}

            //textBox1.Text = "";
            //for (int i = 0; i < DVLEPropDataList[DVLE_LIstBox.SelectedIndex].Count; i++)
            //{
            //    textBox1.Text += DVLEPropDataList[DVLE_LIstBox.SelectedIndex][i] + "\r\n";
            //}

            DVLE_Main_PropertyGrid.SelectedObject = DVLB_Format.DVLE_Dictionary[DVLE_LIstBox.Text];
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            propertyGrid2.SelectedObject = DVLB_Format.DVLP.UnknownDataList[listBox1.SelectedIndex];
        }
    }
}
