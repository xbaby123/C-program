using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using word_dict.word;

namespace word_dict
{
    public partial class Form1 : Form
    {
        private BotDictionnary botDic;

        public Form1()
        {
            InitializeComponent();
            
            String filePath = System.AppDomain.CurrentDomain.BaseDirectory;
           // MessageBox.Show(filePath);
            botDic = new BotDictionnary();
        }
        private void button2_Click(object sender, EventArgs e)
        {
           // rtbStatus.Text = "";

            string command = tbInput.Text;
            rtbStatus.Text += command + "\n";
            rtbStatus.Text += botDic.EncodeCommand(command)+"\n";
            //Dictionary<String, Word> dict = botDic.Dictionary;

      
            

        }
    }
}
