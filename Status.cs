using System;
using System.Collections.Generic;
using System.Linq;

namespace WaterSortPuzzleSolver
{
    internal class Status
    {
        private List<Glass> glasses = new List<Glass>();

        private List<KeyValuePair<int, int>> lastActions = new List<KeyValuePair<int, int>>();

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

        internal List<KeyValuePair<int, int>> LastActions
        {
            get { return lastActions; }
            set { lastActions = value; }
        }

        public List<KeyValuePair<int, int>> GetCanActionList()
        {
            List<KeyValuePair<int, int>> canPourList = new List<KeyValuePair<int, int>>();
            for (int i = 0; i < Glasses.Count; i++)
            {
                for (int j = 0; j < Glasses.Count; j++)
                {
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

        public List<Status> GetNextStatuses(out List<KeyValuePair<int, int>> actions)
        {
            actions = null;
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
    }
}
