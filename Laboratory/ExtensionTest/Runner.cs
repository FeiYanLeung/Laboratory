using System;

namespace Laboratory.ExtensionTest
{
    public class Runner : IRunner
    {
        public string Name
        {
            get { return "扩展方法"; }
        }

        public void Run()
        {
            Console.WriteLine("拼音扩展方法 测试：");
            var testChinese = "梵囧鑫";

            Console.WriteLine("笔画：");
            foreach (var item in testChinese.StrokeNumber())
            {
                Console.WriteLine("{0}->{1}", item.Key, item.Value);
            }

            Console.WriteLine("test target: {0}", testChinese);
            Console.WriteLine("拼音首字母：{0}", testChinese.PYFirstLetter());

            Console.WriteLine("全拼：{0}", string.Join(",", testChinese.PYFirstLetters()));

            Console.WriteLine(" html过滤 测试 ");
            Console.WriteLine("&#60;scr-->ipt>alert('bingo')&#60;/scr-->ipt>".DropHTML());
        }
    }
}
