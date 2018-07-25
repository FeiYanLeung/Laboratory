namespace Laboratory.DesignPatterns.Observer
{
    /// <summary>
    /// 委托
    /// </summary>
    /// <param name="count"></param>
    public delegate void NumberChangedEventHanlder(int count);

    /// <summary>
    /// 发布-订阅
    /// </summary>
    public class Publishser
    {
        private int count;

        /// <summary>
        /// 申明委托变量
        /// </summary>
        public event NumberChangedEventHanlder NumberChanged;

        /// <summary>
        /// 执行某些操作以及触发事件
        /// </summary>
        public void DoSomething()
        {
            #region 触发事件

            if (NumberChanged != null)
            {
                count++;
                NumberChanged(count);
            }

            #endregion
        }
    }
}
