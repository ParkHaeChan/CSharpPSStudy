using System;
using System.Collections.Generic;
using System.Text;

namespace PS_Project
{
    class PS_5 : PS
    {
        // 문제: https://school.programmers.co.kr/learn/courses/30/lessons/92335

        int n = 110011, k = 10;

        bool IsPrime(long num)
        {
            if (num <= 1)
                return false;

            for (long div = 2; div*div <= num; ++div)
            {
                if (num % div == 0)
                    return false;
            }
            return true;
        }

        public int solution(int n, int k)
        {
            int answer = 0;

            // n을 k진수로 변환 (k로 나눈 나머지를 stack에 넣었다 빼주면 구해짐)
            Stack<int> knum = new Stack<int>();
            while (n > 0)
            {
                int remain = n % k;
                knum.Push(remain);
                n = n / k;
            }

            // 0을 만날 때 까지 하나씩 추출
            StringBuilder snum = new StringBuilder();
            while (knum.Count > 0)
            {
                int top = knum.Pop();

                // 이번 top이 마지막인 경우 처리
                if(knum.Count == 0 && top != 0)
                {
                    snum.Append(top);
                    top = 0;
                }

                if (top == 0)
                {   // 이때까지 모인 수가 10진수로 소수인지 확인
                    if (snum.Length == 0)
                        continue;

                    long primeCheck = Convert.ToInt64(snum.ToString());
                    if (IsPrime(primeCheck))
                        answer++;

                    snum.Clear();
                }
                else
                    snum.Append(top);
            }

            return answer;
        }

        public override void Run()
        {
            int answer = solution(n, k);
            Console.WriteLine(answer);
        }
    }
}
