﻿using System;
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
        DataSet1.TagsDataTable MyTagsDataTable;
        public Form1()
        {

            listGrammers = new List<GrammerOp>();
            listTag = new List<TagOp>();
            TagsOfSentence = new List<string>();
            GrammersOfSentence = new List<string>();
            GrammersPossible = new List<string>();
            BadWays = new List<Way>();
            MyWay = new Way();
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
            //int index;
            Boolean IsBadWay = false;
            GrammersPossible = TagsOfSentence.ToList();
            for(int i = 0 ; i<listGrammers.Count ; i++)
            {

                if (listGrammers[i].MyTags.Count > GrammersPossible.Count)
                {
                    continue;
                }
                //index = 0;
                //this Grammer same "Grammers Possible" ?
                for (int index = 0; index < listGrammers[i].MyTags.Count; index++)
                {

                    if (GrammersPossible[index] != listGrammers[i].MyTags[index])
                    {
                        break;
                    }

                    if (index == listGrammers[i].MyTags.Count - 1)
                    {
                        foreach (Way way in BadWays)
                        {
                            if(this.MyWay.IsEqual(way))
                            {
                                IsBadWay = true;
                            }
                        }
                        if (IsBadWay)
                        {
                            continue;
                        }
                        else // good way
                        {
                            List<string> copy = new List<string>();
                            for (int j = 0; j < index - (listGrammers[i].MyTags.Count -1); j++)
                            {
                                copy.Add(GrammersPossible[j]);
                            }
                            copy.Add(listGrammers[i].LeftSide);
                            for (int j = index + 1; j < GrammersPossible.Count /*- (listGrammers[i].MyTags.Count - 1)*/; j++)
                            {
                                copy.Add(GrammersPossible[j]);
                            }
                            GrammersPossible = new List<string>();
                            GrammersPossible = copy.ToList();
                            MyWay.AddStep(listGrammers[i].ID);
                            GrammersOfSentence.Add(listGrammers[i].grammer);
                            continue;
                        }
                    }
                    //index++;
                }
                if (listGrammers.Count - 1 == i)
                {
                    //Boolean GrammersPossibleIsMaster = false;
                    foreach (GrammerOp gr in listGrammers)
                    {
                        if (gr.IsMaster == true)
                        {
                            if (gr.LeftSide != GrammersPossible.First())
                            {
                                i = 0;
                                BadWays.Add(MyWay);
                            }
                            //else
                            //{
                            //    foreach (string gra in GrammersOfSentence)
                            //    {
                            //        textBox2.Text = textBox2.Text + gra + "\r\n";
                            //    }
                                     
                            //}
                            /*
                            if (gr.MyTags.Count != GrammersPossible.Count)
                            {
                                continue;
                            }
                            int k = 0;
                            Boolean GrammersPossibleIsMaster = true;
                            foreach (string tag in GrammersPossible)
                            {
                                if (tag != gr.MyTags[k])
                                {
                                    GrammersPossibleIsMaster = false;
                                    break;
                                }
                                k++;
                            }
                            if (GrammersPossibleIsMaster)
                            {
                                GrammersOfSentence.Add(gr.grammer);
                                MyWay.AddStep(gr.ID);
                            }
                            else
                            {
                                i = 0;
                                BadWays.Add(MyWay);
                            }
                        */}
                    }
                }
            }
            //TagsOfSentence
            foreach (string gra in GrammersOfSentence)
            {
                textBox2.Text = textBox2.Text + gra + "\r\n";
            }
        }
    }
}
