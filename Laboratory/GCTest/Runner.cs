using System;

namespace Laboratory.GCTest
{
    public class Runner : IRunner
    {
        public string Name => "GC";

        public void Run()
        {
            this.generation();
            this.reference();

            Console.Read();
        }

        /// <summary>
        /// 代
        /// </summary>
        private void generation()
        {
            var bag = new Bag();

            // 对象默认分配到第0代
            Console.WriteLine($"bag对象所处代：{GC.GetGeneration(bag)}");

            #region 垃圾回收，对象被分配到第1代

            // 回收对象，这时对象会被分配到第1代
            GC.Collect();
            Console.WriteLine($"bag对象所处代：{GC.GetGeneration(bag)}");

            #endregion

            #region 垃圾回收，对象被分配到第2代(最后一代)

            // 回收对象，这时对象会被分配到第2代
            GC.Collect();
            Console.WriteLine($"bag对象所处代：{GC.GetGeneration(bag)}");

            #endregion

            #region 垃圾回收时对象已经在第二代，所以对象依然在第二代

            // 回收对象，
            GC.Collect();

            Console.WriteLine($"bag对象所处代：{GC.GetGeneration(bag)}");

            #endregion

            // 设置对象引用
            bag = null;

            // 对象引用地址为空，被标记为回收并调用析构函数，如果此时再次查询对象所处代，会出现异常
            GC.Collect();
        }

        /// <summary>
        /// 强引用/若引用
        /// </summary>
        private void reference()
        {
            // 强引用
            var bag = new Bag();

            // 若引用
            WeakReference weak1 = new WeakReference(bag);

            // 若引用
            var weak2 = new WeakReference<Bag>(bag);

            // 由于存在强引用，所以不会被回收
            GC.Collect();

            // 此处也是一个强引用
            Bag bag1;

            Console.WriteLine($" weak1:{weak1.IsAlive}, weak2:{weak2.TryGetTarget(out bag1)} ");

            // 解除所有强引用

            bag = null;
            bag1 = null;

            // 由于已经不存在强引用，所以会被回收
            GC.Collect();

            Console.WriteLine($"weak1:{weak1.IsAlive}, weak2:{weak2.TryGetTarget(out bag1)} ");

            Console.ReadLine();
        }
    }
}
