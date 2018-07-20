using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FriendFinder.Core.Processor
{
    public static class Processor
    {
        public static IEnumerable<KeyValuePair<T,double>> FindNearestNeighbours<T>(int count, object position, List<T> friends)
        {
            var posValues = position.GetType().GetRuntimeProperties().Select(_ => (double)_.GetValue(position));
            Dictionary<T, double> dists = new Dictionary<T, double>();
            foreach (var friend in friends)
            {
                PropertyInfo[] pis = friend.GetType().GetProperties();
                var nm = pis[0].GetValue(friend);
                var lc = pis[1].GetValue(friend);
                var lcValues = lc.GetType().GetProperties().Select(_ => (double)_.GetValue(lc));
                
                //Test to see if we don´t have two people at the same place
                if(lcValues.ToArray()[0] != posValues.ToArray()[0] && lcValues.ToArray()[1] != posValues.ToArray()[1])
                {
                    dists.Add((T)friend, CalculateDist(posValues, lcValues));
                }
            }
            return dists.OrderBy(_ => _.Value).Take(count);
        }

        static double CalculateDist(IEnumerable<double> pos1, IEnumerable<double> pos2)
        {
            var pos1Arr = pos1.ToArray();
            var pos2Arr = pos2.ToArray();

            var deltaX = pos2Arr[0] - pos1Arr[0];
            var deltaY = pos2Arr[1] - pos1Arr[1];

            return Math.Sqrt(Math.Pow(deltaX,2) + Math.Pow(deltaY, 2));
        }
    }
}
