using System.Collections;
using System.Collections.Generic;

namespace Laboratory.Iterator
{
    public class StudentEnumartor : IEnumerator<Student>
    {
        private Students2 students;
        private int position;
        public StudentEnumartor(Students2 students)
        {
            this.position = -1;
            this.students = students;
        }

        public Student Current
        {
            get
            {
                return this.students.students[this.position];
            }
        }

        object IEnumerator.Current => this.Current;


        public bool MoveNext()
        {
            if (this.position < this.students.students.Length - 1)
            {
                this.position++;
                return true;
            }
            return false;
        }

        public void Reset()
        {
            this.position = 0;
        }
        public void Dispose()
        {
            this.students = null;
        }
    }
}
