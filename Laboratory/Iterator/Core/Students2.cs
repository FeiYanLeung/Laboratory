using System;
using System.Collections;
using System.Collections.Generic;

namespace Laboratory.Iterator
{
    public class Students2 : IEnumerable<Student>
    {
        private int position;
        internal Student[] students;

        public Students2(int size)
        {
            this.position = 0;
            this.students = new Student[size];
        }

        public void Add(Student student)
        {
            this.students[this.position++] = student;
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

        public IEnumerator<Student> GetEnumerator()
        {
            return new StudentEnumartor(this);
        }

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }
}
