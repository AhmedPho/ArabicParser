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
        List<TagOp> listTag;
        List<string> TagsOfSentence;
        List<string> GrammersOfSentence;
        List<string> GrammersPossible;
        List<Way> BadWays;
        Way MyWay;
        DataSet1TableAdapters.TagsTableAdapter MyTagsTableAdapter;
        DataSet1TableAdapters.WordsTableAdapter MyWordsTableAdapter;
        DataSet1TableAdapters.GrammersTableAdapter MyGrammersTableAdapter;
        DataSet1.WordsDataTable MyWordsDataTable;
        //DataSet1.TagsDataTable MyTagsDataTable;
        public Form1()
        {

            listGrammers = new List<GrammerOp>();
            listTag = new List<TagOp>();
            
            MyTagsTableAdapter = new DataSet1TableAdapters.TagsTableAdapter();
            MyWordsTableAdapter = new DataSet1TableAdapters.WordsTableAdapter();
            MyGrammersTableAdapter = new DataSet1TableAdapters.GrammersTableAdapter();
            MyGrammersTableAdapter = new DataSet1TableAdapters.GrammersTableAdapter();
            DataSet1.GrammersDataTable MyGrammersDataTable = MyGrammersTableAdapter.GetData();
            DataSet1.TagsDataTable MyTagsDataTable = MyTagsTableAdapter.GetData();
            int IDsOfGrammers = 0;
            foreach (DataSet1.GrammersRow dr in MyGrammersDataTable.Rows)
            {
                listGrammers.Add(new GrammerOp(dr.Grammer, dr.IsMaster, IDsOfGrammers));
                IDsOfGrammers++;
            }
            foreach (DataSet1.TagsRow dr in MyTagsDataTable)
            {
                listTag.Add(new TagOp(dr.Tag,dr.IsTerminal));
            }

            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TagsOfSentence = new List<string>();
            GrammersOfSentence = new List<string>();
            GrammersPossible = new List<string>();
            BadWays = new List<Way>();
            MyWay = new Way();
            string[] Tags = textBox1.Text.Split(' ');
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
            //int index;
            Boolean IsBadWay = false;
            GrammersPossible = TagsOfSentence.ToList();
            for (int indexGrammers = 0; indexGrammers < listGrammers.Count; indexGrammers++)
            {

                if (listGrammers[indexGrammers].MyTags.Count <= GrammersPossible.Count)
                {
                    continue;
                }
                //index = 0;
                //this Grammer same "Grammers Possible" ?
                for (int indexTags = 0; indexTags < GrammersPossible.Count; indexTags++)
                {
                    if (GrammersPossible[indexTags] != listGrammers[indexGrammers].MyTags[indexTags])
                    {
                        break;
                    }

                    if (indexTags == listGrammers[indexGrammers].MyTags.Count - 1)
                    {
                        foreach (Way way in BadWays)
                        {
                            if(this.MyWay.IsEqual(way))
                            {
                                IsBadWay = true;
                            }
                        }
                        if (IsBadWay )
                        {
                            continue;
                        }
                        else // good way
                        {
                            List<string> copy = new List<string>();
                            for (int j = 0; j < indexTags - (listGrammers[indexGrammers].MyTags.Count - 1); j++)
                            {
                                copy.Add(GrammersPossible[j]);
                            }
                            copy.Add(listGrammers[indexGrammers].LeftSide);
                            for (int j = indexTags + 1; j < GrammersPossible.Count; j++)
                            {
                                copy.Add(GrammersPossible[j]);
                            }
                            GrammersPossible = new List<string>();
                            GrammersPossible = copy.ToList();
                            MyWay.AddStep(listGrammers[indexGrammers].ID);
                            GrammersOfSentence.Add(listGrammers[indexGrammers].grammer);
                            indexGrammers = 0;
                            continue;
                        }
                    }
                }
                if (listGrammers.Count - 1 == indexGrammers && GrammersPossible.Count != 1 && MyWay.Steps.Count !=0)
                {
                    GrammersPossible = GrammersOfSentence.ToList();
                    BadWays.Add(MyWay);
                    MyWay = new Way();
                    indexGrammers = 0;
                    if (BadWays.Count == 50)
                    {
                        int m = 0;
                    }
                //    foreach (GrammerOp gr in listGrammers)
                //    {
                //        if (gr.IsMaster == true)
                //        {
                //            if (gr.LeftSide != GrammersPossible.First())
                //            {
                //                GrammersPossible = GrammersOfSentence.ToList();
                //                BadWays.Add(MyWay);
                //                MyWay = new Way();
                //                break;
                //            }
                //       }
                //    }
                }
            }
            //TagsOfSentence
            foreach (string gra in GrammersOfSentence)
            {
                textBox2.Text = textBox2.Text + gra + "\r\n";
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string[] grammers = textBox3.Text.Split(',');
            DataSet1TableAdapters.WordsTableAdapter WordsTableAdapter1 = new DataSet1TableAdapters.WordsTableAdapter();
            for (int i = 0; i < grammers.Length; i++)
            {
                if (grammers[i] != ",")
                {
                    WordsTableAdapter1.InsertQuery1(grammers[i]);
                }
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dataSet1.Tags' table. You can move, or remove it, as needed.
            this.tagsTableAdapter.Fill(this.dataSet1.Tags);

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
