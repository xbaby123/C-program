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
using Facebook;

namespace word_dict
{
    public partial class Form1 : Form
    {
        private BotDictionnary botDic;
        string _accessToken = "CAAVUMKQz7ZB0BANtlRFjMPvzH2PQ2We0Xy6tyd8iAAnSMMHZCduQHQMGZAln4PWOQO6CPFnYT4LVpQJ2TBay7vIR268fuq3jVONE5Hteq05GQA8QlBapz3ZAWmQdmu43giQytttLwpCAphqZCcztAwrlLX3kpc2hm4tBCYmTn7xkRHPoEazE1Nb2XSXQAnYDRZCK6sNixcj7nDni2QN4am";

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


            dynamic messagePost = new ExpandoObject();
            messagePost.message = tbInput.Text;
            var fb = new FacebookClient(_accessToken);
            fb.Post("me" + "/feed", messagePost);
            

        }
    }
}
