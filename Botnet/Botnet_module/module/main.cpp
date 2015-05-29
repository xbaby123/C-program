#include <iostream>
#include <string>
#include <string.h>
#include <stdio.h>
#include <ctype.h>

using namespace std;

void decodeMessage(char* fdMessage, string  &decodedMessage);
void  str2chr(char [], string s);

int main()
{
    char *fbMessage = "child_1_n ask_1_v pleasure_1_n tie_1_v uncle_1_n read_1_v enemy_1_n speak_1_v standard_1_n catch_1_v ruler_1_n enjoy_1_v energy_1_n nab_2_v speed_1_n carry_1_v climate_1_n shoot_1_v image_1_n may_1_v author_1_n gain_1_v exchange_1_n start_1_v shirt_1_n agree_1_v basis_1_n cannot_1_v dollar_1_n join_1_v pain_1_n explain_1_v grandmother_1_n";
    string temp ="";
    char b[500];
    decodeMessage(fbMessage,temp);
    str2chr(b,temp);
    cout << temp << endl;
    cout <<b<<endl;
    cout << "Hello world!" << endl;
    return 0;
}
void decodeMessage(char* fbMessage, string  &temp)
{
    int len = strlen(fbMessage);
    int i,j;
    int newWord = 1;
    char c;
    char d;
    for(i = 0 ; i<len ; i++ )
    {
        c= fbMessage[i];
        if('a'<= c && c<='z' && newWord)
        {
            if(i<len-1){
                d=fbMessage[i+1];
            }else
            {
                continue;
            }
            switch(c)
            {
                case 'a':
                    {
                        if(d=='p')
                            temp.append("\'");
                        else
                            temp.append(string(1,c));
                        break;
                    }
                case 'q':
                    {
                       if(d=='u')
                           temp.append("\"");
                        else
                            temp.append(string(1,c));
                       break;

                    }
                 case 'h':
                    {
                       if(d=='a')
                           temp.append("#");
                        else
                            temp.append(string(1,c));
                       break;

                    }
                     case 'p':
                    {
                       if(d=='e')
                           temp.append("%");
                        else
                            temp.append(string(1,c));
                       break;

                    }
                     case 's':
                    {
                       if(d=='l')
                       {
                           temp.append("(");

                       }else if(d=='r')
                       {
                           temp.append(")");

                       }
                       else if(d=='h')
                       {
                        temp.append("/");

                       }else if(d=='p')
                       {
                        temp.append(" ");

                       }else
                            temp.append(string(1,c));

                        break;

                    }
                     case 'm':
                    {
                       if(d=='i')
                           temp.append("-");
                       else
                            temp.append(string(1,c));
                       break;

                    }
                     case 'd':
                    {
                       if(d=='o')
                           temp.append(".");
                       else
                            temp.append(string(1,c));
                       break;

                    }
                     case 'c':
                    {
                       if(d=='o')
                       {
                           temp.append(",");

                       }else if(d=='l')
                       {
                           temp.append(":");
                       }else
                            temp.append(string(1,c));
                        break;

                    }

                     default :
                        {
                            temp.append(string(1,c));
                            break;
                        }

            }
            newWord = 0;
            i++;
            continue;

        }else if(c==' ')
        {
            newWord = 1;
        }
    }


}

void str2chr(char b[],string s)
{
    int len = s.length();
    int i;
    for(i=0;i<len;i++){
        b[i]=s[i];
    }
    b[i]='\0';
}


//int Split(char *inStr, void *saveArray)
//{
//	int i,j,index=0;
//
//	char *lines[MAX_LINES];
//
//	memset(lines,0,sizeof(lines));
//
//	j=strlen(inStr);
//	if (j<1) return -1;
//
//	lines[index++]=inStr;
//	for (i=0;i < j;i++)
//		if ((inStr[i]=='\x0A') || (inStr[i]=='\x0D'))
//			inStr[i]='\x0';
//
//	//Now that all cr/lf have been converted to NULL, save the pointers...
//	for (i=0;i < j;i++) {
//		if ((inStr[i]=='\x0') && (inStr[i+1]!='\x0')) {
//			if (index < MAX_LINES)
//				lines[index++] = &inStr[i+1];
//			else
//				break;
//		}
//	}
//
//	if (saveArray!=0)
//		memcpy(saveArray,lines,sizeof(lines));
//
//	return index;
//}
