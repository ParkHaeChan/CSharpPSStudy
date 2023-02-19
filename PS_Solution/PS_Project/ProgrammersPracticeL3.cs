using System;
using System.Collections.Generic;
using System.Text;

namespace PS_Project
{
    class PS_4 : PS
    {
        // 문제: https://school.programmers.co.kr/learn/courses/30/lessons/77886

        // 예제 입력
        string[] s = { "1110", "100111100", "0111111010" }; // result should be : "1101","100110110","0110110111"

        /*
         *  시도 1)
         *  110을 모두 찾아서 빼낸 상태에서 어디다 110을 넣어야 사전 순 앞으로 올 것인지를 생각해본다.
         *  
         *  110이 여러개 추출 되는 경우: 110, 110 두개를 추출한 경우 이 두개를 최소로 하여 붙여주는 경우는 다음과 같다.
         *  @위치에 110을 추가하는 경우를 보면
         *  1@10 , 11@0, 110@(== @110)이 있고 각 경우 111010, 111100, 110110으로 110을 그냥 연달아 붙였을 때가 제일 작다.
         *  (3개를 추출한 경우도 가장 앞에나타난 0뒤에 110이 들어가야 제일 작아지므로 110110110이 최소다)
         *  
         *  이제 110을 모두 추출하고 남은 숫자에 어디에 110들을 추가할 것인지를 결정해야한다.
         *  
         *  각 경우를 살펴보면서 예외적인 상황이 발생하는지 찾아야 한다.
         *  
         *  1개만 남은 경우
         *  0: 0뒤에 110을 붙여주어야 한다. 0^110 < 110^0
         *  1: 1 앞에 110을 붙여주어야 한다. 110^1 < 1^110
         *  
         *  2개 남은 경우
         *  00: 1개와 마찬가지로 0 뒤에 -> 00^110
         *  01: 01사이 vs 01뒤에를 비교 -> 0^1101 vs 01^110  -> win: 01사이 (0뒤에 1앞에)
         *  10: 10사이 vs 10뒤 vs 10앞 -> 1^1100 vs 10^110 vs 110^10 -> win: 10뒤 (0 뒤에)
         *  11: 11사이 vs 11뒤 vs 11앞 -> 1^1101 vs 11^110 vs 110^11 -> win: 11뒤
         *  
         *  3개 남은 경우
         *  000: (끝0뒤)
         *  001: 00^1101, 001^110 -> (끝0뒤)
         *  010: 0^11010, 01^1100, 010^110 --> (끝0뒤)
         *  011: 0^11011, 01^1101, 011^110 --> (끝0뒤)
         *  100: ^110100, 1^11000, 10^1100 100^110 --> (끝 0뒤)
         *  101: ^110101, 1^11001, 10^1101, 101^110 --> (끝 0뒤)
         *  110: 추출될 것이고 이경우는 110110
         *  111: 110111 --> 맨 앞
         *  
         *  4개 남은 경우 이상
         *  앞에 3에서 나온 경우에서 앞에 0이 붙은 경우와 1이 붙은 경우이다
         *  0이 붙은 경우는 차이가 없을 것이고, 1이 붙은 경우만 살펴보면
         *  1000: 끝0 뒤
         *  1001: 끝0 뒤 -> 1001101
         *  1010: 끝0 뒤 -> 1010110
         *  1011: 끝0 뒤 -> 1011011
         *  1100: 110 추출 후 -> 0
         *  1101: 110 추출 후 -> 1
         *  1110: 110 추출 후 -> 1
         *  1111: 1101111 -> 맨 앞
         *  
         *  110을 모든 추출하고
         *  남은 숫자에서 맨뒤에 나오는 0의 위치에 추출한 110을 연달아 써준다.
         *  0이 없을 경우 맨 앞에 추출한 110을 써주고 나머지를 써준다.
         * 
        */

        public string[] solution(string[] s)
        {
            List<string> answer = new List<string>();

            foreach(var bin_ary in s)
            {
                // 110을 추출
                int cnt110 = 0;
                string binStr = bin_ary;
                while(binStr.Contains("110"))
                {
                    for(int i=0; i<binStr.Length-2; ++i)
                    {
                        if(binStr[i] == '1')
                        {
                            if(binStr[i+1] == '1')
                            {
                                if(binStr[i+2] == '0')
                                {
                                    var frontStr = binStr.Substring(0, i);
                                    var leftStr = i + 3 >= binStr.Length ? "": binStr.Substring(i + 3);
                                    binStr = frontStr + leftStr;
                                    cnt110++;
                                }
                            }
                        }
                    }
                }

                // 추출한 110 이어 붙이기
                var middleStr = "";
                for (int i = 0; i < cnt110; ++i)
                    middleStr += "110";

                // 나머지에서 0이 있으면
                if (binStr.Contains("0"))
                {   // 맨 끝에 0 찾기
                    int pos = -1;
                    for(int i=0; i<binStr.Length; ++i)
                    {
                        if(binStr[i] == '0')
                            pos = i;
                    }

                    // 끝0 뒤에 110 적용
                    var frontStr = binStr.Substring(0, pos+1);
                    var leftStr = binStr.Substring(pos + 1);
                    answer.Add(frontStr + middleStr + leftStr);
                }
                else
                {   // 1만 있으므로 맨 앞에 적용
                    answer.Add(middleStr + binStr);
                }
            }

            return answer.ToArray();
        }

        public override void Run()
        {
            var answer = solution(s);
            Console.WriteLine(string.Join(", ", answer));
        }
    }
}
