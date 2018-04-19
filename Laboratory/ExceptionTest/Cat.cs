using System;

namespace Laboratory.ExceptionTest
{
    public class Cat
    {

        public void Sleep()
        {
            Console.WriteLine("Cat go to bed !");
            throw new Exception("No !");
        }

    }
}
