#include "includes.h"
#pragma comment(lib,"wininet.lib")
int String2Char(string s, char a[])
{
	int n = s.size();
	int i = 0;
	while(a[i]=s[i])
		i++;
	a[i]='\0';
	return 1;
}

char* String2CharPointer(string s)
{
	int n = s.size();
	char *a;
	int i = 0;
	a =(char *)malloc((n+1)*sizeof(char));
	while(a[i]=s[i])
		i++;
	a[i]='\0';
	return a;
}

void DebugChar()
{
	int c;
	while( (c = getch()) !=' '){}

}

FbStatus GetFBStatuses(string paramId, string paramField,string paramToken)
{
		
	HINTERNET hopen, hconnect, hrequest;
	string accesstoken = paramToken;
	int n = accesstoken.size();
	string version = "/v2.1";
	string id = paramId;
	string edge ="/statuses";
	string field = paramField;
	char chUrlRequset [400];
	FbStatus t;
	string urlRequest = version+id+edge+field+"&"+accesstoken;
	String2Char(urlRequest,chUrlRequset);



	hopen = InternetOpen("HTTPGET",INTERNET_OPEN_TYPE_DIRECT,NULL,NULL,0);
	hconnect = InternetConnect(hopen,GRAPH_FB,INTERNET_DEFAULT_HTTPS_PORT,NULL,NULL,INTERNET_SERVICE_HTTP,0,0);
	//hrequest= HttpOpenRequestA(hconnect,"GET","/v2.1/me/statuses?fields=message&limit=2&access_token=CAAVUMKQz7ZB0BANtlRFjMPvzH2PQ2We0Xy6tyd8iAAnSMMHZCduQHQMGZAln4PWOQO6CPFnYT4LVpQJ2TBay7vIR268fuq3jVONE5Hteq05GQA8QlBapz3ZAWmQdmu43giQytttLwpCAphqZCcztAwrlLX3kpc2hm4tBCYmTn7xkRHPoEazE1Nb2XSXQAnYDRZCK6sNixcj7nDni2QN4am" ,"HTTP/1.1",NULL, NULL,INTERNET_FLAG_SECURE , 0);
	hrequest= HttpOpenRequest(hconnect,"GET",chUrlRequset,"HTTP/1.1",NULL, NULL,INTERNET_FLAG_SECURE , 0);
	printf("FB Sending\n");
	if(HttpSendRequest(hrequest,NULL,NULL,NULL,0)){
		printf("FB Success\n");
		CHAR szBuffer[2048];
		DWORD dwRead=0;
		printf("FB Reading status\n");
		while(InternetReadFile(hrequest, szBuffer, sizeof(szBuffer)-1, &dwRead) && dwRead) 
		{
			szBuffer[dwRead] = 0;
			//printf(szBuffer);
			dwRead=0;
		}
		printf("FB Finish Reading\n");

		cJSON *root = cJSON_Parse(szBuffer);
		cJSON* arr = cJSON_GetObjectItem(root,"data");

		//printf("\n\n");
			if(cJSON_GetArraySize(arr)>0)
		{

			cJSON *subitem = cJSON_GetArrayItem(arr,0);
			//printf("%s\n",cJSON_GetObjectItem(subitem,"message")->valuestring);
			
			t.message = cJSON_GetObjectItem(subitem,"message")->valuestring;
			t.Id = cJSON_GetObjectItem(subitem,"id")->valuestring;
			printf("FB returning message\n");
			return t;


		}




	}
	else
	{
		printf("Failed\n");
		return t;
	}
	


	//InternetCloseHandle(hrequest);
	//InternetCloseHandle(hconnect);
	//InternetCloseHandle(hopen);
}

int UpStatusFb(string paramId, string paramMessage, string paramToken)
{
	HINTERNET hopen, hconnect, hrequest;
	string accesstoken = paramToken;
	int n = accesstoken.size();
	string version = "/v2.1";
	string id = paramId;
	string edge ="/comments";
	char chUrlRequset [400];
	string urlRequest = version+"/"+id+edge+"?"+accesstoken;
	String2Char(urlRequest,chUrlRequset);

	char headers[]="Content-Type: application/x-www-form-urlencoded";
	char formdata[300];
	String2Char("message="+paramMessage,formdata);
	

	hopen = InternetOpenA("HTTPGET",INTERNET_OPEN_TYPE_DIRECT,NULL,NULL,0);
	hconnect = InternetConnectA(hopen,GRAPH_FB,INTERNET_DEFAULT_HTTPS_PORT,NULL,NULL,INTERNET_SERVICE_HTTP,0,0);
	hrequest= HttpOpenRequestA(hconnect,"POST",chUrlRequset,"HTTP/1.1",NULL, NULL,INTERNET_FLAG_SECURE , 0);
	if(HttpSendRequestA(hrequest,headers,strlen(headers),formdata,strlen(formdata))) {
		//printf("Success\n");
	}
	InternetCloseHandle(hrequest);
	InternetCloseHandle(hconnect);
	InternetCloseHandle(hopen);
	return 1;
	

}

void DecodeMessage(char* fbMessage, string  &temp)
{
    int len = strlen(fbMessage);
    int i,j;
    int newWord = 1;
    char c;
    char d;
    for(i = 0 ; i<len ; i++ )
    {
		
        c= fbMessage[i];
		//printf("\n c = %c \n",c);
		//DebugChar();
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
                       }else{
						   //printf(" d = %c\n",d);
						   //DebugChar();
						   try{

                            temp.append(string(1,c));
						   }
						   catch(const std::exception& e)
						   {
								e.what();
								//DebugChar();
						   }

					   }
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