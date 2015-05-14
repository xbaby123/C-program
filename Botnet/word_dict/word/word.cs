using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace word_dict.word
{

    public class BotDictionnary
    {
        private Dictionary<String, Word> dictionary;
        private Dictionary<Char, UInt64> indexingChar;
        private Random ran;

        public Random Ran
        {
            get { return ran; }
            set { ran = value; }
        }

        internal Dictionary<String, Word> Dictionary
        {
            get { return dictionary; }
            set { dictionary = value; }
        }
        private Dictionary<String, String> testDic;
        private  String filePath = System.AppDomain.CurrentDomain.BaseDirectory;

        public Dictionary<String, String> TestDic
        {
            get { return testDic; }
            set { testDic = value; }
        }
        

        public String FilePath
        {
          get { return filePath; }
          set { filePath = value; }
        }

        public   BotDictionnary()
        {        
            String fileName = "dict_with_type.txt";


            List<String> keyWordList = GetKeyWordList();
            dictionary = new Dictionary<string, Word>();
            indexingChar = new Dictionary<Char, UInt64>();
            ran = new Random();
            try
            {
                if (filePath != null && filePath != "")
                {
                    String line;
                    using (StreamReader file = new StreamReader(filePath + fileName))
                    {

                        Int64 number = 0;
                        int tempNum = 0;
                    
                        UInt64  num = 0;
                    
                        String word = "";
                        String type = "";
                        while ((line = file.ReadLine()) != null)
                        {

                            number++;
                            String[] lineArray = line.Split(' ');
                            if (lineArray.Length >= 2)
                            {
                                word = lineArray[0];
                                type = lineArray[1];
                            }
                            bool isKey = keyWordList.IndexOf(word) > 0;

                            if (dictionary.ContainsKey(word))
                            {
                                tempNum++;
                                word = word + "_" + tempNum.ToString();
                            }

                         
                            dictionary.Add(word, new Word(type, isKey));
                        
                            num++;

                        }
                    }
               
                }
            }
            catch (Exception e)
            {
                //Console.WriteLine(e.Message);
                MessageBox.Show(e.Message, "Add to dictionary Error");
            }
        }


        public String EncodeCommand(String command)
        {
            int temp = 1;
            StringBuilder result = new StringBuilder();
            MySpecialChar specialChar = new MySpecialChar();
            foreach (char c in command)
            {
                result.Append(EncodeChar(c,temp,specialChar));
                result.Append(" ");
                temp = temp == 2 ? 1 : temp;
                temp++;
            }

            return result.ToString();
        }



        public String EncodeChar(char c, int part, MySpecialChar specialChar)
        {
            StringBuilder result = new StringBuilder();
            String temp ;
            string wordType ="";
            switch(part){
                case 1:
                    {
                        wordType = "n";
                        break;
                    }
                case 2:
                    {
                        wordType = "v";
                        break;
                    }
                default:
                    {
                        wordType = "v";
                        break;
                    }

            }


            if (('a' <= c && c <= 'z'))
            {
                var values = (from pv in Dictionary
                              where pv.Key.StartsWith(c.ToString())
                              select pv);
                int count = 0;
                count = values.Count();
                while (true)
                {
                    int index = ran.Next(count - 1);
                    KeyValuePair<string,Word> word =values.ElementAt(index);
                    if ('x' <= c && c <= 'z')
                    {
                        return this.RemoveSpecialChar(word.Key);
                    }
                    if(word.Value.IsKeyWord && !specialChar.EncodelList.ContainsValue(word.Key.Substring(0,2)) && word.Value.WordType == wordType)
                    {
                        return this.RemoveSpecialChar(word.Key);
                    }      


                }
                

            }
            else if(specialChar.EncodelList.ContainsKey(c.ToString()))
            {
                try
                {
                    String encode = specialChar.EncodelList[c.ToString()];
                    //  if(en
                    var values = (from pv in Dictionary
                                  where pv.Key.StartsWith(encode[0].ToString()) && pv.Key[1] == encode[1]
                                  select pv);
                    int count = 0;
                    count = values.Count();
                    while (true)
                    {
                        int index = ran.Next(count - 1);
                        KeyValuePair<string,Word> word =values.ElementAt(index);
                        if(word.Value.WordType == wordType)
                        {
                            return this.RemoveSpecialChar(word.Key);
                        }      


                    }


                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Encode Char error");
                }

            }

            return result.ToString();

        }





        public void code(string Url, List<String> wordList)
        {
            try
            {
                String line = "";
                bool getFlag = false;
                HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(Url);
                myRequest.Method = "GET";
                WebResponse myResponse = myRequest.GetResponse();
                StreamReader sr = new StreamReader(myResponse.GetResponseStream(), System.Text.Encoding.UTF8);
                //line = sr.ReadToEnd();
                while ((line = sr.ReadLine()) != null)
                {
                    if (line.CompareTo("\t\t\t\t\t\t\t\t<li>") == 0)
                    {
                        getFlag = true;
                        continue;
                    }

                    if (getFlag)
                    {
                        wordList.Add(RemoveSpecialChar(line));
                        getFlag = false;
                    }


                }
                sr.Close();
                myResponse.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        public String RemoveSpecialChar(String str)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in str)
            {
                if (('a' <= c && c <= 'z') || ('A' <= c && c <= 'Z'))
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }

        public List<String> GetKeyWordList()
        {
            List<String> keyWordList = new List<string>();
            String line = "";
            using (StreamReader file = new StreamReader(filePath + "3000Word.txt"))
            {
                while ((line = file.ReadLine()) != null)
                {
                    keyWordList.Add(line);
                }

            }
            return keyWordList;
        }

      



    }



    class Word
    {

        private String wordType;

        public String WordType
        {
            get { return wordType; }
            set { wordType = value; }
        }


        private bool isKeyWord = false;

        public bool IsKeyWord
        {
            get { return isKeyWord; }
            set { isKeyWord = value; }
        }

        public Word(String paramWordType, bool paramIsKeyWord)
        {
            wordType = paramWordType;
            isKeyWord = paramIsKeyWord;
        }
        


    }

    public enum WordPOS{
        Noun,
        Verb,
        Adverb,
        Pronoun,
        Adjective,
        Prepostion,
        Conjunction,
        Interjection,
        Article
    };


    public class MySpecialChar
    {
        const String Quotation = "\"";
        const String HashTag = "#";
        const String Percent = "%";
        const String Apos = "'";
        const String Left_Parenthesis = "(";
        const String Right_Parenthesis = ")";
        const String Minus = "-";
        const String Dot = ".";
        const String Slash = "\\";
        const String Comma = ",";
        const String Colon = ":";
        const String Space = " ";

        private Dictionary<String, String> encodelList;

        public Dictionary<String, String> EncodelList
        {
            get { return encodelList; }
            set { encodelList = value; }
        }

        public MySpecialChar()
        {
            encodelList = new Dictionary<string, string>();
            encodelList.Add(Quotation, "qu");
            encodelList.Add(HashTag, "ha");
            encodelList.Add(Percent, "pe");
            encodelList.Add(Apos, "ap");
            encodelList.Add(Left_Parenthesis, "sl");
            encodelList.Add(Right_Parenthesis, "sr");
            encodelList.Add(Minus, "mi");
            encodelList.Add(Dot, "do");
            encodelList.Add(Slash, "sh");
            encodelList.Add(Comma, "co");
            encodelList.Add(Colon, "cl");
            encodelList.Add(Space, "sp");

        }


    }

    /*encode system:
    " : Quotation mark      : Qu
    # : hashtag             :ha 
    % : percent             :pe
    ' : Apostrophe          :ap
    ( : Left parenthesis    :sl
    ) : Right parenthesis   :sr
    - : Hyphen-minus        :mi
    . : Dot                 :do
    / : Slash               :sh
    , : Comma               :co
    : : colon               :cl
     * 
     * */

}
