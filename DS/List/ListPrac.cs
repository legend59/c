using System;
using System.Collections.Generic;
using System.IO;

namespace ListPrac
{
    class ListPrac
    {
        static void Main(string[] args)
        {
            List<Grade> myAL = new List<Grade>();

            string line;
            StreamReader file = new StreamReader("List_Sample.txt");
            while ((line = file.ReadLine()) != null)
            {
                string[] words = line.Split('\t');
                Grade userGrade = new Grade(words[0], Convert.ToInt32(words[1]), Convert.ToInt32(words[2]), Convert.ToInt32(words[3]));

                myAL.Add(userGrade);
            }
            file.Close();

            while (true)
            {
                string strLine = Console.ReadLine();

                switch(strLine)
                {
                    case "PRINT": // 이름 순 출력
                        myAL.Sort((Grade x, Grade y) => x.Name.CompareTo(y.Name));
                        break;
                    case "KOREAN": // 국어 성적 순 출력
                        // Lambda식
                        myAL.Sort((Grade x, Grade y) => x.Korean == y.Korean ? x.Name.CompareTo(y.Name) : y.Korean - x.Korean);
                        break;
                    case "ENGLISH": // 영어 성적 순 출력
                        // delegate
                        myAL.Sort(delegate (Grade x, Grade y)
                        {
                            if (y.English == x.English)
                            {
                                return x.Name.CompareTo(y.Name);
                            }
                            else
                            {
                                return y.English.CompareTo(x.English);
                            }
                        });
                        break;
                    case "MATH": // 수학 성적 순 출력
                        // comparer
                        myAL.Sort(compare);
                        break;
                    case "QUIT": // 종료
                        return;
                    default:
                        break;
                }

                foreach (Grade obj in myAL)
                {
                    Console.WriteLine("{0}\t{1}\t{2}\t{3}", obj.Name, obj.Korean, obj.English, obj.Math);
                }
            }
        }

        static int compare(Grade x, Grade y)
        {
            if (x.Math == y.Math)
                return x.Name.CompareTo(y.Name);
            else
                return y.Math - x.Math;
        }
    }

    public class Grade
    {
        private String name;
        private int korean;
        private int english;
        private int math;

        public Grade(string str, int k, int e, int m)
        {
            Name = str;
            Korean = k;
            English = e;
            Math = m;
        }

        public string Name { get => name; set => name = value; }
        public int Korean { get => korean; set => korean = value; }
        public int English { get => english; set => english = value; }
        public int Math { get => math; set => math = value; }
    }
}