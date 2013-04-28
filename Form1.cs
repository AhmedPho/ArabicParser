using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArabicParser
{
    public partial class Form1 : Form
    {
        List<GrammerOp> listGrammers;
        List<string> TagsOfSentence;
        DataSet1TableAdapters.TagsTableAdapter MyTagsTableAdapter;
        DataSet1TableAdapters.WordsTableAdapter MyWordsTableAdapter;
        DataSet1TableAdapters.GrammersTableAdapter MyGrammersTableAdapter;
        DataSet1.WordsDataTable MyWordsDataTable;
        public Form1()
        {

            listGrammers = new List<GrammerOp>();
            TagsOfSentence = new List<string>();
            MyTagsTableAdapter = new DataSet1TableAdapters.TagsTableAdapter();
            MyWordsTableAdapter = new DataSet1TableAdapters.WordsTableAdapter();
            MyGrammersTableAdapter = new DataSet1TableAdapters.GrammersTableAdapter();
            MyGrammersTableAdapter = new DataSet1TableAdapters.GrammersTableAdapter();
            DataSet1.GrammersDataTable MyGrammersDataTable = MyGrammersTableAdapter.GetData();
            foreach (DataSet1.GrammersRow dr in MyGrammersDataTable.Rows)
            {
                listGrammers.Add(new GrammerOp(dr.Grammer,dr.IsMaster));
            }
            
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //GrammerOp a = new GrammerOp(textBox1.Text,true);
            //label1.Text = "LeftSide = " + a.LeftSide;
            //label2.Text = "RightSide = " + a.RightSide;
            string[] Tags = textBox1.Text.Split(' ');
            //label1.Text = " ";
            foreach (string tag in Tags)
            {
                MyWordsDataTable = MyWordsTableAdapter.GetTag(tag);
                string temp="";
                Boolean WordInDB = false;
                
                foreach (DataSet1.WordsRow row in MyWordsDataTable)
                {
                    WordInDB = true;
                    temp = row.Tag;
                }
                if (WordInDB)
                {
                    label1.Text = label1.Text + temp + " ";
                    TagsOfSentence.Add(temp);
                }
            }
        }
    }
}
