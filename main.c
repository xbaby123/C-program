///*
///* <-- Deadfish Interpreted Computer Language --> */
///* <-- Programmed by Jonathan Todd Skinner    --> */
//
///* <-- Include Header File --> */
///* Updated the Code to Remove Uneeded Header Includes - JTS */
//#include <stdio.h>
//
///* <-- Declare some variables --> */
//unsigned int x; /* make a positive integer and call it x */
//char usrinput; /* string to hold user input */
//
///* <-- Declare a function --> */
//void entercommand(void);
//
///* <-- Start Main Function --> */
//int main(void)
//{
//	/* At beginning of x is always 0 */
//	x = 0;
//	entercommand();
//}
//
///* <-- Enter Command --> */
//void entercommand(void)
//{
//	/* Accept User Input */
//	printf(">> "); /* output shell symbol */
//	scanf("%c",&usrinput); /* scan for user input that is char */
//	/* Check for commands and do action */
//	/* Make sure x is not greater then 256 */
//	if(x == 256) x = 0;
//	if(x == -1) x = 0;
//	if(usrinput == 'i')
//	{
//		x++;
//		entercommand();
//	}
//	else if(usrinput == 'd')
//	{
//		x--;
//		entercommand();
//	}
//	else if(usrinput == 'o')
//	{
//		printf("%d\n",x);
//		entercommand();
//	}
//	else if(usrinput == 's')
//	{
//		x=x*x;
//		entercommand();
//	}
//	else
//	{
//		printf("\n");
//		entercommand();
//	}
//
//}
