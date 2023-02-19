using System;
using System.Collections.Generic;

namespace PS_Project
{
    public class PS
    {   // Problem Solving 클래스
        // PS를 상속하여 Run 함수를 구현해 Main에서 실행(한 프로젝트에서 여러 소스 코드 파일을 실행 Test가능 하도록 구성)
        static Dictionary<string, PS> testDict = new Dictionary<string, PS>();

        public PS()
        {
            Enroll();
        }

        public virtual void Run() { }

        public static PS GetTestProgram(int index)
        {
            string key = "PS_Project.PS_" + index;
            if (!testDict.ContainsKey(key))
            {
                Console.WriteLine("해당하는 key값이 없습니다");
                return null;
            }
            return testDict[key];
        }

        void Enroll()
        {
            // 클래스 명을 key로 사용하기 때문에 key가 중복될 일은 없다(컴파일 에러 날 것임)
            testDict[this.ToString()] = this;
        }
    }

    class Program
    {
        const int TESTINDEX = 4;    // 0번: HelloWorld 출력 테스트
        const int TESTCOUNT = 1000;

        static void Main(string[] args)
        {
            // 테스트 할 class 세팅 (많아지면 인덱스 범위 조절 필요함)
            for(int i=0; i<=TESTCOUNT; ++i)
            {
                var type = Type.GetType("PS_Project.PS_" + i);
                if (type != null)
                {
                    var instance = Activator.CreateInstance(type) as PS;
                }
            }

            // 테스트 수행할 Class index를 입력
            var test = PS.GetTestProgram(TESTINDEX);

            test.Run();
        }
    }
}
