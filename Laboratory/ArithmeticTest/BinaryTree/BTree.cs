namespace Laboratory.ArithmeticTest.BinaryTree
{
    public class BTree
    {
        public BTree(string name)
        {
            this.Name = name;
        }

        public string Name { get; private set; }
        public BTree LeftTree { get; set; }
        public BTree RightTree { get; set; }
    }
}
