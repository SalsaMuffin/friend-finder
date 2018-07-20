using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriendFinder.Entity
{
    public class Friend
    {
        public Friend()
        {

        }
        public Friend(string name, Point location)
        {
            this.Name = name;
            this.Location = location;
        }

        public string Name { get; set; }

        public Point Location { get; set; }
    }
}
