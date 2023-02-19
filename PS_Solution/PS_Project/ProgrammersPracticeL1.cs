using System;
using System.Collections.Generic;
using System.Text;

namespace PS_Project
{
    class PS_3 : PS
    {
        // 문제: https://school.programmers.co.kr/learn/courses/30/lessons/118666

        // 예제 입력
        string[] survey = { "AN", "CF", "MJ", "RT", "NA" };
        int[] choices = { 5, 3, 2, 7, 5 };

        public string solution(string[] survey, int[] choices)
        {
            string answer = "";

            // 자료 구조 초기화
            Dictionary<char, int> factorDict = new Dictionary<char, int>();
            string factors = "RTCFJMAN";
            foreach(var c in factors)
            {
                factorDict[c] = 0;
            }

            // 점수 계산
            for(int i=0; i<survey.Length; ++i)
            {
                string fQuery = survey[i];
                if (choices[i] < 4)
                    factorDict[fQuery[0]] += 4-choices[i];
                else
                    factorDict[fQuery[1]] += choices[i]-4;  // 4는 0점(영향X)
            }

            // factor 별 총점으로 최종 계산
            for(int i=0; i<factors.Length; i+=2)
            {   // factor 점수 비교
                int fs1 = factorDict[factors[i]];
                int fs2 = factorDict[factors[i+1]];
                char factor;

                if (fs1 == fs2)
                {   // 점수 같은 경우 사전순 앞선 것 적용
                    factor = factors[i] < factors[i + 1] ? factors[i] : factors[i + 1];
                    answer += factor;
                }
                else
                {   // 점수 큰 것 적용
                    factor = fs1 > fs2 ? factors[i] : factors[i + 1];
                    answer += factor;
                }
            }

            return answer;
        }

        public override void Run()
        {
            var answer = solution(survey, choices);
            Console.WriteLine(answer);
        }
    }
}
