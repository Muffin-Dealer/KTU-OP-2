using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Lab01
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        private string studentInput = @"App_Data/students.txt";
        private string connectionInput = @"App_Data/connections.txt";
        private string initialDataPath = @"App_Data/initial_data.txt";
        private string outputDataPath = @"App_Data/result.txt";

        private Dictionary<string, Student> students;
        private List<Tuple<string, string>> connectionList;
        protected void Page_Load(object sender, EventArgs e)
        {
            // Initial Data
            InOutUtils.CreateFile(Server.MapPath(initialDataPath));
            students = InOutUtils.ReadStudents(Server.MapPath(studentInput));
            
            FillTableWithStudents(new List<Student>(students.Values), 
                                  StudentTable);

            InOutUtils.AppendInitStudents(new List<Student>(students.Values), 
                                               Server.MapPath(initialDataPath));

            connectionList = InOutUtils.ReadConnections(Server.MapPath(connectionInput));
            
            FillTableWithConnections(connectionList, 
                                     ConnectionTable);

            InOutUtils.AppendInitData(connectionList, 
                                     Server.MapPath(initialDataPath));


            FillPathTable(students, connectionList, PathTable);
            InOutUtils.CreateFile(Server.MapPath(outputDataPath));
            
            InOutUtils.AppendConnectionResults(students, 
                                              connectionList, 
                                              Server.MapPath(outputDataPath));

        }

        /// <summary>
        /// Used to show initial Student Data
        /// </summary>
        /// <param name="students">List Student data type</param>
        /// <param name="table">Table Object data type</param>
        protected void FillTableWithStudents(List<Student> students, Table table)
        {
            TableRow row = new TableRow();
            row.Cells.Add(CreateCell("Studentas"));
            row.Cells.Add(CreateCell("Draugų Kiekis"));
            row.Cells.Add(CreateCell("Studentų Draugai:"));
            table.Rows.Add(row);

            foreach (Student student in students)
            {
                row = new TableRow();
                row.Cells.Add(CreateCell(student.Name));
                row.Cells.Add(CreateCell(student.FriendCount.ToString()));
                row.Cells.Add(CreateCell(student.GetFriendsString()));
                table.Rows.Add(row);

            }
        }

        /// <summary>
        /// Used to show initial connection data
        /// </summary>
        /// <param name="connections">List of Tuples compromised of string, string containing the initial node and end node to use for DFS</param>
        /// <param name="table">Table object data type</param>
        protected void FillTableWithConnections(List<Tuple<string,
                                                           string>> connections, 
                                                Table table)
        {
            TableRow row = new TableRow();
            row.Cells.Add(CreateCell("Draugas"));
            row.Cells.Add(CreateCell("Ieškomas Draugas"));
            table.Rows.Add(row);

            foreach (Tuple<string, string> connection in connections)
            {
                row = new TableRow();
                row.Cells.Add(CreateCell(connection.Item1));
                row.Cells.Add(CreateCell(connection.Item2));
                table.Rows.Add(row);

            }
        }

        /// <summary>
        /// Creates A cell with provided Text
        /// </summary>
        /// <param name="text">text to be added to the Cell.text param</param>
        /// <returns>TableCell object</returns>
        protected TableCell CreateCell(string text)
        {
            TableCell cell = new TableCell();
            cell.Style.Add("padding", "5px");
            cell.Text = text;
            return cell;
        }

        /// <summary>
        /// Fills the table with paths from student a to b
        /// </summary>
        /// <param name="students"> Dictionary, key -> string of the student, value -> student object</param>
        /// <param name="connections">List of Tuples compromised of string, string containing the initial node and end node to use for DFS</param>
        /// <param name="table">Table object where the data will be added</param>
        protected void FillPathTable(Dictionary<string, Student> students, 
                                     List<Tuple<string, string>> connections, 
                                     Table table)
        {
            TableRow row = new TableRow();
            row.Cells.Add(CreateCell("Draugas"));
            row.Cells.Add(CreateCell("Ieškomas Draugas"));
            row.Cells.Add(CreateCell("Kelias: "));
            table.Rows.Add(row);

            foreach (Tuple<string, string> connection in connections)
            {    
                List<string> path = new List<string>();
                path.Add(connection.Item1);
                path = TaskUtils.FindConnection(connection.Item1, aaaaa
                                                connection.Item2, 
                                                path, students);

                string pathText = TaskUtils.CreatePathText(path);

                row = new TableRow();
                row.Cells.Add(CreateCell(connection.Item1));
                row.Cells.Add(CreateCell(connection.Item2));
                row.Cells.Add(CreateCell(pathText));
                table.Rows.Add(row);

            }
        }
    }
}