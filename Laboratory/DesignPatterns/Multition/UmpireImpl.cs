using System;
using System.Collections.Generic;

namespace Laboratory.DesignPatterns.Multition
{
    public class UmpireImpl
    {
        private static readonly object locker = new object();
        private static List<Umpire> umpires = new List<Umpire>();

        static UmpireImpl()
        {
            umpires = new List<Umpire>(){
                new Umpire("裁判员1"),
                new Umpire("裁判员2"),
                new Umpire("裁判员3"),
                new Umpire("裁判员4"),
                new Umpire("裁判员5"),
                new Umpire("裁判员6")
            };
        }

        public static Umpire Instance(string name = "")
        {
            if (string.IsNullOrEmpty(name))
            {
                var rand = new Random();
                var randValue = rand.Next(0, umpires.Count);
                return umpires[randValue];
            }
            return umpires.Find((u) => u.Name == name);
        }
    }
}
