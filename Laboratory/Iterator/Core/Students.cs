using System;
using System.Collections;
using System.Collections.Generic;

namespace Laboratory.Iterator
{
    public class Students : IEnumerable<Student>
    {
        private int position;
        private Student[] students;

        public Students(int size)
        {
            this.position = 0;
            this.students = new Student[size];
        }

        /// <summary>
        /// 索引器
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Student this[int index]
        {
            get
            {
                if (index > -1 && index <= this.students.Length - 1)
                {
                    return this.students[index];
                }
                throw new IndexOutOfRangeException();
            }
            set
            {
                this.students[index] = value;
            }
        }

        public void Add(Student student)
        {
            this.students[position++] = student;
        }

        public IEnumerator<Student> GetEnumerator()
        {
            foreach (var student in students)
            {
                yield return student;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
