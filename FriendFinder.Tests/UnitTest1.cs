using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FriendFinder.Core.Processor;
using FriendFinder.Entity;
using System.Collections.Generic;
using FriendFinder.Core;
using System.Linq;

namespace FriendFinder.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void FriendFinder_Is_Finding()
        {
            List<Friend> friends = new List<Friend>();

            friends.Add(new Friend("Giulia", new Point(1, 1)));
            friends.Add(new Friend("Alber", new Point(33.765, 25.55665)));
            friends.Add(new Friend("Fabio", new Point(15.434534, 5.34555)));
            friends.Add(new Friend("Lourdes", new Point(-3, -7)));
            friends.Add(new Friend("Moacyr", new Point(-8.23335, 2.44334)));

            var nearFriends = Processor.FindNearestNeighbours<Friend>(3, new Point(0, 0), friends);
            Assert.IsTrue(nearFriends.Count() == 3);
        }

        [TestMethod]
        public void FileReader_Is_Reading_Files()
        {
            var friends = FileReader.ReadFiles<Friend>(@"C:\Users\ACastellani\Documents\visual studio 2015\Projects\FriendFinder\FriendFinder\JSON_input_files\", true);
            var gpsData = FileReader.ReadFiles<Point>(@"C:\Users\ACastellani\Documents\visual studio 2015\Projects\FriendFinder\FriendFinder\GPS_data\", false);
        }
    }
}
