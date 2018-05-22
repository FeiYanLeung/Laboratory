using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Laboratory.ArithmeticTest.BinaryTree
{
    public class BTIteration
    {
        private BTree bTree;
        public BTIteration()
        {
            bTree = new BTree("a")
            {
                LeftTree = new BTree("b")
                {
                    RightTree = new BTree("c")
                    {
                        LeftTree = new BTree("d")
                    }
                },
                RightTree = new BTree("e")
                {
                    RightTree = new BTree("f")
                    {
                        LeftTree = new BTree("g")
                        {
                            LeftTree = new BTree("h"),
                            RightTree = new BTree("k")
                        }
                    }
                }
            };
        }

        public void Run()
        {
            var builder = new StringBuilder();
            this.iteration(bTree, ref builder);
            Console.WriteLine(builder.ToString());
            builder.Clear();


            this.LevelOrder(bTree);
            builder.Clear();
        }

        /// <summary>
        /// 先序遍历
        /// </summary>
        /// <param name="bTree"></param>
        /// <param name="builder"></param>
        void iteration(BTree bTree, ref StringBuilder builder)
        {
            if (bTree == null) return;
            iteration(bTree.LeftTree, ref builder);
            builder.Append(bTree.Name);
            iteration(bTree.RightTree, ref builder);
        }

        void LevelOrder(BTree tree)
        {
            if (tree == null)
                return;

            List<BTree> destTree = new List<BTree>() {
                tree
            };

            while (destTree.Count > 0)
            {
                var _destTree = destTree[0];
                destTree.RemoveAt(0);

                Console.WriteLine(_destTree.Name);
                if (_destTree.LeftTree != null)
                {
                    destTree.Add(_destTree.LeftTree);
                }
                if (_destTree.RightTree != null)
                {
                    destTree.Add(_destTree.RightTree);
                }
            }
            return;

            var queue = new Queue<BTree>();
            queue.Enqueue(tree);

            while (queue.Any())
            {
                var item = queue.Dequeue();
                System.Console.Write(item.Name);

                if (item.LeftTree != null) queue.Enqueue(item.LeftTree);

                if (item.RightTree != null) queue.Enqueue(item.RightTree);
            }
        }
    }
}
