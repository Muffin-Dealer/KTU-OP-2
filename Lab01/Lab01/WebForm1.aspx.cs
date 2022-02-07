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
        private string outputPath = "App_Data/output.txt";

        private Dictionary<string, Student> students;
        private List<Tuple<string, string>> connectionList;
        protected void Page_Load(object sender, EventArgs e)
        {
            // Initial Data
            students = TaskUtils.ReadStudents(Server.MapPath(studentInput));
            FillTableWithStudents(new List<Student>(students.Values), StudentTable);
            connectionList = TaskUtils.ReadConnections(Server.MapPath(connectionInput));
            FillTableWithConnections(connectionList, ConnectionTable);
            FillPathTable(students, connectionList, PathTable);

        }

        /// <summary>
        /// Used to show initial Student Data
        /// </summary>
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
        protected void FillTableWithConnections(List<Tuple<string,string>> connections, Table table)
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
        protected TableCell CreateCell(string text)
        {
            TableCell cell = new TableCell();
            cell.Style.Add("padding", "5px");
            cell.Text = text;
            return cell;
        }

        protected void FillPathTable(Dictionary<string, Student> students, List<Tuple<string, string>> connections, Table table)
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
                path = TaskUtils.FindConnection(connection.Item1, connection.Item2, path, students);
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