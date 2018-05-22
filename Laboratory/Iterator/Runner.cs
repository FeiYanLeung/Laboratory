using System;

namespace Laboratory.Iterator
{
    public class Runner : IRunner
    {
        public string Name => "迭代(IEnumerator、IEnumerable)";

        public void Run()
        {
            var students = new Students(2);
            students.Add(new Student(1, "Leung", 18));
            students.Add(new Student(2, "Jack", 22));

            foreach (var student in students)
            {
                Console.WriteLine(student.ToString());
            }

            Console.WriteLine(students[0].ToString());
            Console.WriteLine(students[1].ToString());

            Console.WriteLine("==== StudentEnumartor ====");

            var students2 = new Students2(2);
            students2.Add(new Student(3, "Leung", 18));
            students2.Add(new Student(4, "Jack", 22));

            foreach (var student in students2)
            {
                Console.WriteLine(student.ToString());
            }

            Console.WriteLine(students2[0].ToString());
            Console.WriteLine(students2[1].ToString());

            return;
        }
    }
}
