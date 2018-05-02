using System;
using System.Collections.Generic;
using System.Linq;

namespace Laboratory.ArithmeticTest
{
    public class Runner : IRunner
    {
        public string Name
        {
            get { return "算法"; }
        }

        /// <summary>
        /// 斐波拉契
        /// </summary>
        /// <typeparam name="TA1"></typeparam>
        /// <typeparam name="TA2"></typeparam>
        /// <typeparam name="TA3"></typeparam>
        /// <typeparam name="TR"></typeparam>
        /// <param name="f"></param>
        /// <returns></returns>
        private static Func<TA1, TA2, TA3, TR> fibonacci<TA1, TA2, TA3, TR>(Func<Func<TA1, TA2, TA3, TR>, Func<TA1, TA2, TA3, TR>> f)
        {
            return (x, y, z) => f(fibonacci(f))(x, y, z);
        }

        /// <summary>
        /// 填充100个随机数
        /// </summary>
        private static void fillRndNumber()
        {
            var lst = new List<int>();

            var rnd = new Random();
            do
            {
                var rndValue = rnd.Next(0, 100);
                if (!lst.Contains(rndValue))
                {
                    lst.Add(rndValue);
                }
            } while (lst.Count < 100);
            Console.WriteLine(string.Join(",", lst));

            lst.Clear();

            for (int i = 0; i < 100;)
            {
                var rndValue = rnd.Next(0, 100);
                if (!lst.Contains(rndValue))
                {
                    i++;
                    lst.Add(rndValue);
                }
            }
            Console.WriteLine(string.Join(",", lst));

            lst.Clear();

            while (lst.Count < 100)
            {
                var rndValue = rnd.Next(0, 100);
                if (!lst.Contains(rndValue))
                {
                    lst.Add(rndValue);
                }
            }

            Console.WriteLine(string.Join(",", lst));
        }

        /// <summary>
        /// 冒泡排序
        /// <remarks>
        /// 如果数组中没有元素或只有一个元素时最佳排序时间复杂度O(1)，否则为O(n)
        /// </remarks>
        /// </summary>
        /// <param name="origin_arr"></param>
        public void bubbleSort(int[] bubble_nums)
        {
            Console.WriteLine("冒泡排序：{0}", string.Join(" ", bubble_nums));

            int temp;
            bool swapped;

            for (int i = 0, len = bubble_nums.Length; i < len - 1; i++)
            {
                swapped = true;
                for (int j = 0; j < len - i - 1; j++)
                {
                    if (bubble_nums[j + 1] < bubble_nums[j])
                    {
                        temp = bubble_nums[j];
                        bubble_nums[j] = bubble_nums[j + 1];
                        bubble_nums[j + 1] = temp;
                        swapped = false;
                    }
                }
                Console.WriteLine("执行第{0}次：{1}", i + 1, string.Join(" ", bubble_nums));

                if (swapped) break;
            }

            Console.WriteLine("排序后的结果：{0}", string.Join(" ", bubble_nums));
        }

        /// <summary>
        /// 获取按枢轴值左右分流后枢轴的位置
        /// </summary>
        /// <param name="list"></param>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        private int division(List<int> list, int left, int right)
        {
            while (left < right)
            {
                int num = list[left]; //将首元素作为枢轴
                if (num > list[left + 1])
                {
                    list[left] = list[left + 1];
                    list[left + 1] = num;
                    left++;
                }
                else
                {
                    int temp = list[right];
                    list[right] = list[left + 1];
                    list[left + 1] = temp;
                    right--;
                }
                Console.WriteLine(string.Join(",", list));
            }
            Console.WriteLine("--------------\n");
            return left; //指向的此时枢轴的位置
        }
        private void quickSort(List<int> list, int left, int right)
        {
            if (left < right)
            {
                int i = division(list, left, right);
                //对枢轴的左边部分进行排序
                quickSort(list, i + 1, right);
                //对枢轴的右边部分进行排序
                quickSort(list, left, i - 1);
            }
        }

        /// <summary>
        /// 二分法查询
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="n"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public int binary_search(int[] nums, int target)
        {
            int low = 0;   //数组的首位置，即arr[0]处
            int height = nums.Length - 1;//数组的最后一个位置，即arr[n-1],数组大小为n
            while (low <= height)
            {
                int mid = low + ((height - low) / 2);//此处的mid的计算一定要放在while循环内部，否则mid无法正确更新;并且此处用移位代替除以2可以提高效率，而且可以防止溢出。
                if (nums[mid] > target)//数组中间的位置得数大于要查找的数，那么我们就在中间数的左区间找
                {
                    //循环条件一定要注意
                    height = mid - 1;
                }
                else if (nums[mid] < target)//数组中间的位置得数小于要查找的数，那么我们就在中间数的右区间找
                {
                    low = mid + 1;
                }
                else
                {
                    return mid;//中间数刚好是要查找的数字。
                }
            }

            //执行流如果走到此处说明没有找到要查找的数字。
            return -1;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Run()
        {
            #region 二分法，查找对象必须升序排列
            {
                int[] nums = new int[] { 1, 3, 5, 9, 10 };
                Console.WriteLine("输入查找的数：[{0}]", string.Join(",", nums));

                var input = Console.ReadLine();

                int target = 1;
                int.TryParse(input, out target);

                int ret1 = binary_search(nums, target);
                

                Console.WriteLine("二分法：{0}", ret1);
                return;
            }
            #endregion

            #region 快速排序
            {
                int[] _nArray = new int[] { 7, 6, 5, 4, 3, 2, 1, 0 };
                int _nLength = _nArray.Length;

                var list = _nArray.ToList();
                quickSort(list, 0, _nLength - 1);

                Console.WriteLine("快速排序：{0}", string.Join(" ", list));
            }

            #endregion

            #region 冒泡排序
            {
                return;
                bubbleSort(new int[] { 7, 6, 5, 4, 3, 2, 1, 0 });
            }
            #endregion

            #region 斐波那契
            {
                return;
                Console.WriteLine("打印斐波那契数");

                Func<int, int, int, int> FibBase = fibonacci<int, int, int, int>(f => (n, x, y) => n < 2 ? y : f(n - 1, y, x + y));

                for (int i = 0; i < 10; i++)
                {
                    Console.WriteLine(FibBase(i, 1, 1));
                }

            }
            #endregion

            Console.WriteLine("填充100个随机数");
            fillRndNumber();

        }
    }
}
