using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Facebook;
using word_dict.word;

namespace FB_Controller
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

      
        
        string accessToken = "CAAVUMKQz7ZB0BANtlRFjMPvzH2PQ2We0Xy6tyd8iAAnSMMHZCduQHQMGZAln4PWOQO6CPFnYT4LVpQJ2TBay7vIR268fuq3jVONE5Hteq05GQA8QlBapz3ZAWmQdmu43giQytttLwpCAphqZCcztAwrlLX3kpc2hm4tBCYmTn7xkRHPoEazE1Nb2XSXQAnYDRZCK6sNixcj7nDni2QN4am";
       
        

      

        private void btnSend_Click(object sender, EventArgs e)
        {
            string command = tbInput.Text;
            rtbStatus.Text += command + "\n";
            string encodeStr = botDic.EncodeCommand(command); 
            rtbStatus.Text += encodeStr + "\n";
            //Dictionary<String, Word> dict = botDic.Dictionary;

            dynamic messagePost = new ExpandoObject();
            messagePost.message = encodeStr;
            var fb = new FacebookClient(accessToken);
            fb.Post("me" + "/feed", messagePost);

        }

       
    }
}
