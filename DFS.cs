using System.Collections.Generic;

namespace WaterSortPuzzleSolver
{
    internal class DFS
    {
        internal bool IsGo = true;
        internal List<Status> HasDealed = new List<Status>();
        internal List<KeyValuePair<int, int>> Actions = null;

        public List<KeyValuePair<int, int>> Deal(Status status)
        {
            HasDealed.Add(status);
            List<Status> nextStatuses = status.GetNextStatuses(out Actions);
            if (Actions != null)
            {
                return Actions;
            }
            foreach (Status nextStatus in nextStatuses)
            {
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
                Deal(nextStatus);
                if (Actions != null)
                {
                    return Actions;
                }
            }
            return Actions;
        }
    }
}
