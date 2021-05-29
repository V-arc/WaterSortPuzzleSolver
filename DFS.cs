using System.Collections.Generic;

namespace WaterSortPuzzleSolver
{
    internal class DFS
    {
        internal List<Status> HasDealed = new List<Status>();
        internal List<KeyValuePair<int, int>> Actions = null;

        public List<KeyValuePair<int, int>> Deal(Status status)
        {
            HasDealed.Add(status);
            List<Status> nextStatuses = status.GetNextStatuses(out Actions);
            #region 找到结果立刻返回
            if (Actions != null)
            {
                return Actions;
            }
            #endregion
            foreach (Status nextStatus in nextStatuses)
            {
                #region 不重复处理
                bool hasDealed = false;
                foreach (Status statusHasDealed in HasDealed)
                {
                    if (nextStatus.IsEqual(statusHasDealed))
                    {
                        hasDealed = true;
                        break;
                    }
                }
                if (hasDealed)
                {
                    continue;
                }
                #endregion
                Deal(nextStatus);
                #region 找到结果立刻返回
                if (Actions != null)
                {
                    return Actions;
                }
                #endregion
            }
            return Actions;
        }
    }
}
