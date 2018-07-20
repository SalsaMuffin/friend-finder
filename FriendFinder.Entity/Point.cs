using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriendFinder.Entity
{
    public class Point
    {
        public Point()
        {

        }
        public Point(double lat, double lg)
        {
            this.Lat = lat;
            this.Long = lg;
        }
        public double Lat { get; set; }

        public double Long { get; set; }
    }
}
