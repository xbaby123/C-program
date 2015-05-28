//#include <stdio.h>
//#include <math.h>
//int CheckPerfectSquare(long long n)
//{
//    long long square = sqrt(n);
//    if(square*square==n)
//        return 1;
//    else
//        return 0;
//}
//int main(void){
//    long long input = 9999999997;
//    long long result = 0;
//    long long i ;
//    long long j;
//    for( i =1;i<=input;i++)
//    {
//        for(j=i;j<=input;j++)
//        {
//            if(j==i)
//            {
//                result+=i;
//            }else if(j==i+1)
//            {
//                result+=i;
//            }
//            long long diagonal = i*i+j*j;
//            if(CheckPerfectSquare(diagonal))
//            {
//                result+=i;
//                printf("Result: %ll\n",result);
//            }
//
//        }
//    }
//    printf("ok\n");
//    printf("%ll",result);
//
//}
