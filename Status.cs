using System;
using System.Collections.Generic;
using System.Linq;

namespace WaterSortPuzzleSolver
{
    internal class Status
    {
        private List<Glass> glasses = new List<Glass>();

        private List<KeyValuePair<int, int>> lastActions = new List<KeyValuePair<int, int>>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        /// <param name="lastAction">经由哪一步行动至此节点</param>
        public Status(Status status, KeyValuePair<int, int> lastAction)
        {
            foreach (Glass glass in status.Glasses)
            {
                this.Glasses.Add(new Glass(glass));
            }
            this.LastActions = new List<KeyValuePair<int, int>>(status.LastActions);
            this.LastActions.Add(lastAction);
        }

        public Status(string status)
        {
            foreach (string glass in status.Split("\n"))
            {
                this.Glasses.Add(new Glass(glass));
            }
        }

        internal List<Glass> Glasses
        {
            get { return glasses; }
            set { glasses = value; }
        }

        /// <summary>
        /// 由根节点至当前节点进行过的行动
        /// </summary>
        internal List<KeyValuePair<int, int>> LastActions
        {
            get { return lastActions; }
            set { lastActions = value; }
        }

        /// <summary>
        /// 获得下一步可以进行的行动
        /// </summary>
        /// <returns></returns>
        public List<KeyValuePair<int, int>> GetCanActionList()
        {
            List<KeyValuePair<int, int>> canPourList = new List<KeyValuePair<int, int>>();
            for (int i = 0; i < Glasses.Count; i++)
            {
                //A倒入B与B倒入A结果不同，故从0开始
                for (int j = 0; j < Glasses.Count; j++)
                {
                    //不能倒给自己
                    if (i == j)
                    {
                        continue;
                    }
                    if (Glass.CanPour(Glasses[i], Glasses[j]))
                    {
                        canPourList.Add(new KeyValuePair<int, int>(i, j));
                    }
                }
            }
            return canPourList;
        }

        /// <summary>
        /// 是否已达到完成状态
        /// </summary>
        /// <remarks>即每一个杯子要么杯中水为同一颜色，要么没有水</remarks>
        /// <returns></returns>
        public bool IsCompleted()
        {
            foreach (Glass glass in Glasses)
            {
                if (!glass.IsCompleted())
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 获得当前节点经过可进行的活动后得到的节点列表
        /// </summary>
        /// <param name="actions">
        /// 由根节点起可以达到完成状态节点的行动列表
        /// <para>如果能够找到则返回一个列表，否则返回null</para>
        /// </param>
        /// <returns></returns>
        public List<Status> GetNextStatuses(out List<KeyValuePair<int, int>> actions)
        {
            actions = null;//TODO:是否可以out一个action或Status
            List<Status> result = new List<Status>();
            foreach (KeyValuePair<int, int> item in this.GetCanActionList())
            {
                Status status = new Status(this, item);
                Glass pouringGlass = status.Glasses[item.Key];
                int num = pouringGlass.GetTopWaterNumAndColor(out Color color);
                pouringGlass.Pouring();
                status.Glasses[item.Value].Poured(num, color);
                if (status.IsCompleted())
                {
                    actions = new List<KeyValuePair<int, int>>(status.LastActions);
                    //return result;
                }
                result.Add(status);
            }
            return result;
        }

        public bool IsEqual(Status status)
        {
            /* 1. 将Glass中的Colors(Color[])分别转化为int后连接成一个整数
             *      如：[Black,Black,DarkBlue,DarkBlue]转化成1122
             * 2. 将这些int类型存储为数组的形式
             * 3. 将数组排序，并进行比较
             */
            int[] statusIntArray1 = new int[this.Glasses.Count];
            for (int i = 0; i < this.Glasses.Count; i++)
            {
                string temp = "";
                foreach (Color color in this.Glasses[i].Colors)
                {
                    temp += ((int)color).ToString();
                }
                statusIntArray1[i] = int.Parse(temp);
            }

            int[] statusIntArray2 = new int[status.Glasses.Count];
            for (int i = 0; i < status.Glasses.Count; i++)
            {
                string temp = "";
                foreach (Color color in status.Glasses[i].Colors)
                {
                    temp += ((int)color).ToString();
                }
                statusIntArray2[i] = int.Parse(temp);
            }
            Array.Sort(statusIntArray1);
            Array.Sort(statusIntArray2);
            return Enumerable.SequenceEqual(statusIntArray1, statusIntArray2);
        }

        public override string ToString()
        {
            string result = "";
            foreach (Glass glass in Glasses)
            {
                result += glass.ToString() + "\n";
            }
            return result;
        }
    }
}
