using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Lab01
{
    /// <summary>
    /// InOutUtils class for reading and writing data from/to a file
    /// </summary>
    public static class InOutUtils
    {
        /// <summary>
        /// Creates a new empty file, ready for appending data
        /// </summary>
        /// <param name="path">path to the file</param>
        public static void CreateFile(string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.Create))
                new StreamWriter(fs, encoding: System.Text.Encoding.UTF8).Close();
        }


        /// <summary>
        /// appends initial student data to TXT file
        /// </summary>
        /// <param name="students">List of all students (Student object)</param>
        /// <param name="path">path to the file where information will be appended</param>
        public static void AppendInitStudents(List<Student> students, string path)
        {
            using (StreamWriter sr = new StreamWriter(path, append: true))
            {
                sr.WriteLine("Studentai ir jų draugai");
                sr.WriteLine($"{"Studentas",-20}|{"Draugų kiekis",-20}|{"Draugai:"}");
                foreach (Student student in students)
                    sr.WriteLine(student);
                sr.WriteLine();
            }
        }

        /// <summary>
        /// Appends initial connection data to output file
        /// </summary>
        /// <param name="connections">List of Tuples(string, string) that work as nodes from student a to student b while using DFS</param>
        /// <param name="path">path to the file where to append initial data</param>
        public static void AppendInitData(List<Tuple<string, string>> connections, string path)
        {
            using (StreamWriter sr = new StreamWriter(path, append: true))
            {
                sr.WriteLine("Studentai ir jų ieškomi draugai:");
                sr.WriteLine($"{"Studentas",-20} {"Ieškomas draugas",-20}");
                foreach (Tuple<string, string> connection in connections)
                    sr.WriteLine($"{connection.Item1,-20} {connection.Item2,-20}");
                sr.WriteLine();
            }
        }

        /// <summary>
        /// Appends output connection data to output file
        /// </summary>
        /// <param name="students">Dictionary, key -> string, name of the student, value -> Student class object of the student</param>
        /// <param name="connections">List of tuples(string, string) that is compromised of student names that work as nodes that are used for DFS</param>
        /// <param name="outputPath">output path to the txt file where data will be APPENDED</param>
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
                    studentPath = TaskUtils.FindConnection(connection.Item1, connection.Item2, studentPath, students);
                    string pathText = TaskUtils.CreatePathText(studentPath);
                    sr.WriteLine($"{connection.Item1,-20}|{connection.Item2,-20}|{pathText}");
                }
            }
        }

        /// <summary>
        /// Creates a name to Student class object relation dictionary
        /// </summary>
        /// <param name="path">Path to the the text file containing the data</param>
        /// <returns>Dictionary(key -> string, value -> Student class object) </returns>
        public static Dictionary<string, Student> ReadStudents(string path)
        {
            Dictionary<string, Student> students = new Dictionary<string, Student>();
            using (StreamReader sr = new StreamReader(path))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
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
                while ((line = sr.ReadLine()) != null)
                {
                    string[] elements = line.Split(' ');
                    conncetions.Add(new Tuple<string, string>(elements[0], elements[1]));
                }
            }

            return conncetions;
        }
    }
}