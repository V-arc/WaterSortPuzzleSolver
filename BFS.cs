using System;
using System.Collections.Generic;

namespace WaterSortPuzzleSolver
{
    /// <summary>
    /// DFS
    /// </summary>
    /// <remarks>有错误，暂时弃用<remarks>
    [Obsolete]
    internal class BFS
    {
        private bool IsGo = true;
        private List<KeyValuePair<int, int>> Actions = null;

        public List<KeyValuePair<int, int>> method(List<Status> currentStatuses)
        {
            Console.WriteLine(currentStatuses.Count.ToString());
            if (!IsGo)
            {
                return Actions;
            }
            List<Status> statuses = new List<Status>();
            foreach (Status currentStatus in currentStatuses)
            {
                statuses.AddRange(currentStatus.GetNextStatuses(out Actions));
                //快速剪枝
                if (Actions != null)
                {
                    IsGo = false;
                    return Actions;
                }
            }
            return method(statuses);
        }
    }
}
