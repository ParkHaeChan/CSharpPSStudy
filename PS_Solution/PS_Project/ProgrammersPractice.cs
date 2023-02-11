using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PS_Project
{
    class PS_2 : PS
    {
        // 문제: https://school.programmers.co.kr/learn/courses/30/lessons/92334

        // 예제 입력
        string[] id_list = { "muzi", "frodo", "apeach", "neo" };
        string[] report = { "muzi frodo", "apeach frodo", "frodo neo", "muzi neo", "apeach muzi" };
        int k = 2;

        public override void Run()
        {
            var answer = solution2(id_list, report, k);
            Console.WriteLine(string.Join(", ", answer));
        }

        public int[] solution1(string[] id_list, string[] report, int k)
        {
            List<int> answer = new List<int>();

            // Dictionary에 신고자 id와 신고당한 id들의 집합 저장
            Dictionary<string, HashSet<string>> reportDictionary = new Dictionary<string, HashSet<string>>();
            
            // id별 신고 당한 횟수 저장
            Dictionary<string, int> sueDictionary = new Dictionary<string, int>();

            foreach (var id in id_list)
            {   // Dictionary 초기화
                reportDictionary[id] = new HashSet<string>();
                sueDictionary[id] = 0;
            }

            // report 형식 (신고자, 신고할 사람) 묶음
            foreach (var e in report)
            {
                var string_pair = e.Split(" ");     // 구분자: space
                var reporter = string_pair[0];
                var reportee = string_pair[1];

                var set = reportDictionary[reporter];
                set.Add(reportee);
            }

            foreach (var id in id_list)
            {
                var set = reportDictionary[id];
                foreach(var sued in set)
                {   // 신고 당한 횟수 저장
                    sueDictionary[sued]++;
                }
            }

            // id별 처리 결과 메일 받을 횟수
            foreach(var id in id_list)
            {
                var set = reportDictionary[id]; // id가 신고한 사람 명단
                int mailcnt = 0;
                foreach(var sued in set)
                {
                    if (sueDictionary[sued] >= k)
                        mailcnt++;
                }
                answer.Add(mailcnt);
            }

            return answer.ToArray();
        }

        // 다른 사람 풀이 중 Linq 사용한 것
        // GroupBy 예시 블로그 글: https://developer-talk.tistory.com/607
        public int[] solution2(string[] id_list, string[] report, int k)
        {
            var 중복제거 = report.Distinct();
            Console.WriteLine("중복 신고 제외: " + string.Join(", ", 중복제거));
            var 데이터수정= 중복제거.Select(s => s.Split(' '));
            foreach(var data in 데이터수정)
                Console.WriteLine(data[0] + "가 " + data[1] + "을 신고함");
            var 그룹화 = 데이터수정.GroupBy(g => g[1]); // 신고 당한 id 기준으로 그룹화 ==> 그룹화 결과: {신고 당한 id[key] , (신고자 id, 신고 당한 id)}의 집합
            foreach(var g in 그룹화)
                Console.WriteLine( g.Key + "가 신고 당한 횟수: " + string.Join(", ", g.Count()));
            var 조건처리 = 그룹화.Where(w => w.Count() >= k);
            Console.WriteLine("정지 처리된 id: " + string.Join(", ", 조건처리.Select(a => a.Key).ToList()));
            // SelectMany: 각 원소가 컬랙션일 때 펴서 하나의 리스트로 만들어 줌(flatten)
            var ls = 조건처리.SelectMany(sm => sm.Select(s => s[0]/* 위 그룹화 상태 참고: 0이면 신고자 id */)).ToList();
            Console.WriteLine("처리 결과 메일 받을 사람(신고 한 사람중 정지 여럿 있으면 메일 여러번 받기 가능하므로 중복 가능): " + string.Join(", ", ls));

            // id_list 순서로 ls의 원소와 같은지에 대한 갯수를 적용
            return id_list.Select(a => ls.Count(c => c == a)).ToArray();
        }
    }
}
