using FriendFinder.Core;
using FriendFinder.Core.Processor;
using FriendFinder.Entity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace FriendFinder
{
    class Program
    {
        static Timer t;
        static double interval;
        static int count;

        static string GPS_path;
        static string Friends_path;
        static void Main(string[] args)
        {
            Initialize();

            t = new Timer(interval);
            t.Elapsed += T_Elapsed;
            t.Start();
            T_Elapsed(null, null);
            Console.ReadKey();
        }

        static void Initialize()
        {
            interval = Convert.ToInt64(ConfigurationManager.AppSettings["interval"]);
            count = Convert.ToInt32(ConfigurationManager.AppSettings["count"]);
            GPS_path = ConfigurationManager.AppSettings["GPS_path"];
            Friends_path = ConfigurationManager.AppSettings["Friends_path"];
        }

        private static void T_Elapsed(object sender, ElapsedEventArgs e)
        {
            var friends = FileReader.ReadFiles<Friend>(@Friends_path, true);

            //
            // Find friends based on you (your position or a position reported by a GPS)
            //

            //var you = FileReader.ReadFiles<Point>(@Friends_path, false).LastOrDefault();

            //if (you != null)
            //{
            //    var nearests = Processor.FindNearestNeighbours(count, you, friends);
            //    Console.Clear();
            //    DisplayNearests(nearests, you);
            //}


            //TODO: Implement find nearest 3 for each friend.
            Console.Clear();
            foreach (var friend in friends)
            {
                var nearestForFriend = Processor.FindNearestNeighbours(count, friend.Location, friends);
                DisplayNearests(nearestForFriend, friend.Location, friend.Name);
                Console.WriteLine("");
            }
        }

        static void DisplayNearests(IEnumerable<KeyValuePair<Friend, double>> list, Point you, string he = null)
        {
            var actor = !String.IsNullOrEmpty(he) ? he + " is" : "You are";
            Console.WriteLine(actor + " at: " + you.Lat + " lat, " + you.Long + " long. Local time: " + DateTime.Now.ToString());
            
            Console.WriteLine("The " + count + " first nearest friends in asc order (if " + count + " or more):");
            foreach (var nearest in list)
            {
                Console.WriteLine(nearest.Key.Name + " is at: " + nearest.Key.Location.Lat + " lat and " + nearest.Key.Location.Long + " long. Distance: " + nearest.Value);
            }
            Console.WriteLine("");
            //Console.WriteLine("Refresh interval: " + (interval / 1000) + " seconds.");
            //Console.WriteLine("");
        }
    }
}
