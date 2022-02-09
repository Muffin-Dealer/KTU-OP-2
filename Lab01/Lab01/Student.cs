using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lab01
{
    /// <summary>
    /// Student Class Data Object that stores the name and connection
    /// </summary>
    public class Student
    {
        public string Name { get; set; }
        private List<string> Friends;
        public int FriendCount { get { return Friends.Count; } }

        /// <summary>
        /// Constructor
        /// </summary>
        public Student(string name, List<string> friends)
        {
            Name = name;
            Friends = friends;
        }

        /// <summary>
        /// Copies friends
        /// </summary>
        /// <returns>Deep copy of Friends List</returns>
        public List<string> GetFriends()
        {
            List<string> friendList = new List<string>();
            foreach (string friend in Friends)
                friendList.Add(friend);

            return friendList;
        }

        /// <summary>
        /// Transforms Friends list into a string seperated by spaces
        /// </summary>
        public string GetFriendsString() => String.Join(" ", Friends);

        /// <summary>
        /// ToString Override
        /// </summary>
        public override string ToString()
        {
            return $"{Name,-20}|{Friends.Count,20}|{GetFriendsString()}";
        }
    }
}