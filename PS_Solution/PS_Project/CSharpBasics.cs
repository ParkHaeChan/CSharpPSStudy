using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PS_Project
{
    class PS_Basic0 : PS
    {
        public override void Run()
        {
            Console.WriteLine("Hello World");
        }
    }

    class PS_Basic1 : PS
    {
        // 정렬: using System.Linq; 사용

        class CustomElem
        {
            public string _name;
            public int _age;

            public CustomElem(string name, int age)
            {
                _name = name;   // this.name이라 쓰면 객체 이름에 접근하게 된다. (그리고 읽기 전용이라 무시된다)
                _age = age;
            }

            public override string ToString()
            {
                return "Name: " + _name + ", Age: " + _age;
            }
        }

        public override void Run()
        {
            List<int> ls = new List<int>() {1,3,2,5,4 };

            // 원본
            Console.WriteLine(string.Join(", ", ls));
            
            // 오름차순 정렬 (자료형이 int라 i=>i 그대로 씀)
            Console.WriteLine(string.Join(", ", ls.OrderBy(i => i).ToList()));

            // 내림차순 정렬
            Console.WriteLine(string.Join(", ", ls.OrderByDescending(i => i).ToList()));

            // Custom 객체
            List<CustomElem> cls = new List<CustomElem>();
            cls.Add(new CustomElem("tom", 33));
            cls.Add(new CustomElem("adam", 33));
            cls.Add(new CustomElem("blue", 33));
            cls.Add(new CustomElem("may", 22));
            cls.Add(new CustomElem("zolly", 22));

            // 이름 오름 차순
            Console.WriteLine(string.Join(", ", cls.OrderBy(e => e._name).ToList()));
            // 이름 내림 차순
            Console.WriteLine(string.Join(", ", cls.OrderByDescending(e => e._name).ToList()));

            // 이름만 모아서
            var temp = new List<string>();
            cls.ForEach(a => temp.Add(a._name));
            Console.WriteLine(string.Join(", ", temp.OrderBy(a=>a)));
        }
    }

    class PS_Basic2 : PS
    {
        // BFS 구현
        // 문제: https://school.programmers.co.kr/learn/courses/30/lessons/1844?language=csharp

        // 입력: 2차원 배열 그래프
        int[,] maps = { { 1, 0, 1, 1, 1 },{1, 0, 1, 0, 1},{1, 0, 1, 1, 1},{1, 1, 1, 0, 1},{0, 0, 0, 0, 1}};     // 11
        //int[,] maps = { { 1, 0, 1, 1, 1 },{1, 0, 1, 0, 1},{1, 0, 1, 1, 1},{1, 1, 1, 0, 0},{0, 0, 0, 0, 1}};   // -1

        public bool InRange(Tuple<int, int> pos, int R, int C)
        {
            if (0 <= pos.Item1 && pos.Item1 < R && 0 <= pos.Item2 && pos.Item2 < C)
                return true;
            return false;
        }

        public int solution(int[,] maps)
        {
            // c++의 pair 대신 C#에서는 KeyValuePair 나 Tuple을 사용하는데, 할당 속도를 제외하고는 Tuple이 성능이 더 좋다.
            // 그래프 순회에서는 할당도 자주하지만,  접근도 자주하므로 일단 tuple을 사용해본다.
            Queue<Tuple<Tuple<int,int>, int>> bfsQ = new Queue<Tuple<Tuple<int,int>, int>>();

            int R = maps.GetLength(0);
            int C = maps.GetLength(1);

            bool[,] visited = new bool[R, C];

            var startPos = new Tuple<int, int>(0, 0);
            var endPos = new Tuple<int, int>(R-1, C-1);

            Tuple<int, int>[] dirs = { new Tuple<int,int>(0, -1), new Tuple<int, int>(0, 1), new Tuple<int, int>(-1, 0), new Tuple<int, int>(1, 0)};    // LRUD

            bfsQ.Enqueue(new Tuple<Tuple<int,int>, int>(startPos, 1));
            visited[0,0] = true;

            while(bfsQ.Count > 0)
            {
                var cur = bfsQ.Dequeue();

                if( cur.Item1.Item1 == endPos.Item1 && cur.Item1.Item2 == endPos.Item2)
                    return cur.Item2;

                foreach(var dir in dirs)
                {
                    var next = new Tuple<int, int>(cur.Item1.Item1 + dir.Item1, cur.Item1.Item2 + dir.Item2);
                    if(InRange(next, R, C) && !visited[next.Item1, next.Item2])
                    {
                        visited[next.Item1, next.Item2] = true;
                        if (maps[next.Item1, next.Item2] == 0)   // 벽
                            continue;
                        bfsQ.Enqueue(new Tuple<Tuple<int, int>, int>(next, cur.Item2+1));
                    }
                }
            }

            return -1;
        }

        public override void Run()
        {
            var answer = solution(maps);
            Console.WriteLine("최단 거리: " + answer);
        }
    }
}
