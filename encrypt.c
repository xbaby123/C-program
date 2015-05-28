#include <stdio.h>
#include <math.h>
#include <stdlib.h>
int encrypt(long long n)
{

}
int main(void){

   char  input[] = "0123456789abcdef";
   char encrypt[] = "0123456789abcdef";
   char ip[] = "2004e3bcbcgbdcc`2004`27:2004`27:bcgbdcc`";
   int length = strlen(input);
   int ipnum = strlen(ip);
   int i;
   int j;
   for(i=0;i<length;i++)
   {
       char c = input[i];
       c = c ^ (c>>4);
       encrypt[i]= (char)(c);
       printf("%c",c);
   }
   printf("\n");

   for(i=0;i<ipnum;i++)
   {
       char k = ip[i];
       for(j=0;j<length;j++)
       {
           if(encrypt[j]==k)
                printf("%c",input[j]);
       }
   }

}
