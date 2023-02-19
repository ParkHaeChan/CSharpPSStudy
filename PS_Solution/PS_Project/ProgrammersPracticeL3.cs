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
         *  결과 : 시간 초과 발생
         *  
         *  시도2)
         *  시간 초과가 발생하여 약간의 최적화를 적용해본다.
         *  110을 추출했을 때 다시 문자열의 처음으로 돌아가지 않고
         *  바로 앞의 인덱스에서 시작하도록 수정 110을 추출 했을 때 다음 경우를 생각해볼 수 있다.
         *  
         *  추출한 앞쪽이 00인 경우 -> 뒤에서만 더 찾아보면 된다
         *  추출한 앞쪽이 01인 경우 -> 앞에 1을 찾았다 치고 더 찾아 보면 된다.
         *  추출한 앞쪽이 10인 경우 -> 뒤에서만 더 찾아보면 된다
         *  추출한 앞쪽이 11인 경우 -> 앞에 11을 찾았다 치고 더 찾아 보면 된다.
         *  (110을 만들 수 있는지만 알면 되므로 앞을 더 볼 필요는 없음)
         *  
         *  시간 초과 발생
         *  
         *  시도3) C# indexOf 메서드 사용
         *  시간 초과 발생
         *  
         *  시도4) 
         *  result 만드는 부분 최적화
         *  
         *  시도5)
         *  110 추출 후 바로 이어붙인 새로운 문자열을 만드는데 시간이 오래 걸릴 것이므로 이부분을 더 최적화
         *  이어 붙이는 부분에서 110이 생길 수 있으므로 검사가 필요하다.
         *  110 추출한 인덱스가 i이면
         *  i-1이 1이면 뒷 문자열이 10으로 시작하는지 확인후 그 뒤에만 확인하고,
         *  i-1, i-2가 모두 1이면 뒷 문자열이 0으로 시작하는지 확인후 그 뒤에만 확인하면 된다.
         *  둘 다 아니면 합친 부위에서 110이 나올 확률은 없으므로 뒷 문자열 부터 다시 110을 찾는다.
         *  다시 합친 상태에서 index를 찾으면 느리므로 앞쪽부분은 모아두었다가 한번에 합친다.
         *  실패
         *  
         *  시도6)
         *  앞쪽을 모았다가 한번에 합치는 것이 아닌 뒤쪽에서 110을 못찾은 경우 바로 직전의 앞쪽과 합칠 때 110이 생길 수 있는지를 체크한다.
         *  직전의 앞쪽을 불러와서 합쳐서 110 체크를 다시 해야하므로
         *  앞쪽을 저장할 때는 stack 자료 구조를 사용한다.
         *  실패
         *  
         *  
        */

        public string[] solution(string[] s)
        {
            List<string> answer = new List<string>();
            Stack<string> parts = new Stack<string>();

            foreach(var bin_ary in s)
            {
                int cnt110 = 0; // 추출한 110 갯수
                string binStr = bin_ary;

                int idx110 = binStr.IndexOf("110");
                parts.Clear();

                // 110 없으면 바꿀게 없으므로 더 볼 필요 없음
                if (idx110 == -1)
                {
                    answer.Add(binStr);
                    continue;
                }

                // 추출 진행
                cnt110++;

                var frontStr = binStr.Substring(0, idx110);
                var leftStr = idx110 + 3 >= binStr.Length ? "" : binStr.Substring(idx110 + 3);

                // 추출 후 앞 뒤 부분 문자열
                parts.Push(frontStr);
                parts.Push(leftStr);

                // stack에 넣고 재접합시 110 처리하며 반복
                while (parts.Count >= 2)
                {
                    leftStr = parts.Pop();
                    frontStr = parts.Pop();

                    // 접합부에서 110이 발생 가능한 경우
                    if (frontStr.EndsWith("1") && leftStr.StartsWith("10"))
                    {
                        cnt110++;
                        parts.Push(frontStr.Substring(0, frontStr.Length - 1));
                        parts.Push(leftStr.Substring(2));
                    }
                    else if (frontStr.EndsWith("11") && leftStr.StartsWith("0"))
                    {
                        cnt110++;
                        parts.Push(frontStr.Substring(0, frontStr.Length - 2));
                        parts.Push(leftStr.Substring(1));
                    }
                    else
                    {   // 뒤쪽에서 110 나오는지 확인하여 그 부분으로 분리
                        idx110 = leftStr.IndexOf("110");
                        if (idx110 != -1)
                        {
                            var leftfrontStr = leftStr.Substring(0, idx110);
                            var leftbackStr = idx110 + 3 >= leftStr.Length ? "" : leftStr.Substring(idx110 + 3);

                            frontStr = frontStr + leftfrontStr;
                            leftStr = leftbackStr;

                            parts.Push(frontStr);
                            parts.Push(leftStr);
                        }
                        else
                            binStr = frontStr + leftStr;
                    }
                }

                // 추출한 110 이어 붙이기
                var middleStr = "";
                for (int i = 0; i < cnt110; ++i)
                    middleStr += "110";

                // 맨 끝에 0 찾기
                int pos = -1;
                for (int i = 0; i < binStr.Length; ++i)
                {
                    if (binStr[i] == '0')
                        pos = i;
                }

                // 나머지에서 0이 있으면
                if (pos != -1)
                {
                    // 끝0 뒤에 110 적용
                    frontStr = binStr.Substring(0, pos+1);
                    leftStr = binStr.Substring(pos + 1);
                    answer.Add(frontStr + middleStr + leftStr);
                }
                else
                {   // 1로만 구성되어 있으므로 맨 앞에 적용
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
