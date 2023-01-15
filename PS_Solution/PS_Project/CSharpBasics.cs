using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PS_Project
{
    class PS_0 : PS
    {
        public override void Run()
        {
            Console.WriteLine("Hello World");
        }
    }

    class PS_1 : PS
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
}
