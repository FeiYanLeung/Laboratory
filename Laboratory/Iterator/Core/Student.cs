namespace Laboratory.Iterator
{
    public class Student
    {
        public Student() { }
        public Student(int id, string name, int age)
        {
            this.Id = id;
            this.Name = name;
            this.Age = age;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }

        public override string ToString()
        {
            return $"Id:{Id}, Name:{Name}, Age:{Age}";
        }
    }
}
