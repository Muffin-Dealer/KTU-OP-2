using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Lab01
{
    public static class TaskUtils
    {
        /// <summary>
        /// Creates a name to Student class object relation dictionary
        /// </summary>
        /// <param name="path">Path to the the text file containing the data</param>
        /// <returns>Dictionary(key -> string, value -> Student class object) </returns>
        public static Dictionary<string,Student> ReadStudents(string path)
        {
            Dictionary<string, Student> students = new Dictionary<string, Student>();
            using (StreamReader sr = new StreamReader(path))
            {
                string line;
                while((line = sr.ReadLine()) != null)
                {
                    string[] elements = line.Split(' ');
                    string name = elements[0];
                    List<string> friends = new List<string>();
                    for (int i = 2; i < elements.Length; i++)
                        friends.Add(elements[i]);

                    students.Add(name, new Student(name, friends));
                }
            }
            return students;
        }

        /// <summary>
        /// Gets the connections of students
        /// </summary>
        /// <param name="path">.txt file to the input</param>
        /// <returns>List of Tupples(string, string)</returns>
        public static List<Tuple<string, string>> ReadConnections(string path)
        {
            List<Tuple<string, string>> conncetions = new List<Tuple<string, string>>();
            using (StreamReader sr = new StreamReader(path))
            {
                string line;
                while((line = sr.ReadLine()) != null)
                {
                    string[] elements = line.Split(' ');
                    conncetions.Add(new Tuple<string, string>(elements[0], elements[1]));
                }
            }

            return conncetions;
        }

        /// <summary>
        /// Recursive implementation of DFS
        /// </summary>
        /// <param name="start">Start of the person</param>
        /// <param name="end">End of the person</param>
        /// <param name="path">path to current position from initial start</param>
        /// <param name="students">Dictionary, key: string (name of the student), value Student class object</param>
        /// <returns>List of strings, that create a path from student a to b</returns>
        public static List<string> FindConnection(string start, string end, List<string> path, Dictionary<string, Student> students)
        {
            Student curr = students[start];
            List<string> outputPath = null;
            foreach(string next in curr.GetFriends())
            {
                if (next == end)
                    return path;

                else if (path.Contains(next)) // Checks if the current node has been visited, so it does not loop
                    continue;

                Student nextStudent = students[next];
                List<string> pathCopy = path.CopyPath();
                pathCopy.Add(next);
                
                List<String> pathToEnd = FindConnection(next, end, pathCopy, students); // Recursion Call

                if(outputPath == null || (pathToEnd != null && pathToEnd.Count < outputPath.Count))
                    outputPath = pathToEnd;

            }

            return outputPath; // Did not found the path
        }

        /// <summary>
        /// Deep copies a string list
        /// </summary>
        /// <param name="path">string list</param>
        /// <returns>string list</returns>
        private static List<string> CopyPath(this List<string> path)
        {
            List<string> copy = new List<string>();
            foreach (string s in path)
                copy.Add(s);

            return copy;
        }

        public static string CreatePathText(List<string> path)
        {
            if (path == null)
                return "negali susipažinti";
            else if (path.Count == 1)
                return "jau pažįstami";
            else
            {
                path.RemoveAt(0);
                return $"bendri pažįstami: {String.Join(" ", path)}";
            }
        }

        /// <summary>
        /// Creates a new empty file, ready for appending data
        /// </summary>
        /// <param name="path"></param>
        public static void CreateFile(string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.CreateNew))
                new StreamWriter(fs, encoding: System.Text.Encoding.UTF8).Close();
        }
       

        /// <summary>
        /// appends students to TXT file
        /// </summary>
        public static void AppendInitialStudentData(List<Student> students, string path)
        {
            using (StreamWriter sr = new StreamWriter(path, append:true))
            {
                sr.WriteLine("Studentai ir jų draugai");
                sr.WriteLine($"{"Studentas",-20}|{"Draugų kiekis",-20}|{"Draugai:"}");
                foreach (Student student in students)
                    sr.WriteLine(student);
                sr.WriteLine();
            }
        }

        public static void AppendInnitialConnectionData(List<Tuple<string, string>> connections, string path)
        {
            using (StreamWriter sr = new StreamWriter(path, append:true))
            {
                sr.WriteLine("Studentai ir jų ieškomi draugai:");
                sr.WriteLine($"{"Studentas",-20} {"Ieškomas draugas",-20}");
                foreach (Tuple<string, string> connection in connections)
                    sr.WriteLine($"{connection.Item1,-20} {connection.Item2,-20}");
                sr.WriteLine();
            }
        }

        public static void AppendConnectionResults(Dictionary<string, Student> students, List<Tuple<string, string>> connections, string outputPath)
        {

            using (StreamWriter sr = new StreamWriter(outputPath))
            {
                sr.WriteLine("Draugai ir jų junginiai, bei keliai:");
                sr.WriteLine($"{"Draugas",-20}|{"Ieškomas draugas:",-20}|{"Kelias:"}");
                foreach (Tuple<string, string> connection in connections)
                {
                    List<string> studentPath = new List<string>();
                    studentPath.Add(connection.Item1);
                    studentPath = FindConnection(connection.Item1, connection.Item2, studentPath, students);
                    string pathText = TaskUtils.CreatePathText(studentPath);
                    sr.WriteLine($"{connection.Item1,-20}|{connection.Item2,-20}|{pathText}");
                }
            }
        }
    }
}