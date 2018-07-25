using System;

namespace Laboratory.DesignPatterns.Prototype
{
    public class Graphic : IPrototype<Graphic>
    {
        public Graphic() { }
        public Graphic(string name)
        {
            this.Name = name;
        }

        public Graphic Clone()
        {
            return (Graphic)base.MemberwiseClone();
        }

        public string Name { get; set; }

        public void Draw()
        {
            Console.WriteLine($"正在绘制图像{this.Name}");
        }
    }
}
