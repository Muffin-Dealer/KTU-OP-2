using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace Lab02
{
    /// <summary>
    /// TaskUtils static class for helper functions
    /// </summary>
    public static class TaskUtils
    {
        /// <summary>
        /// Creates TableCell from text to speed up TableCell creation
        /// </summary>
        /// <param name="text">string text to add to the table cell</param>
        /// <returns>TableCell class object</returns>
        public static TableCell CreateCell(string text)
        {
            TableCell cell = new TableCell();
            cell.Text = text;
            return cell;
        }
    }
}