using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SavarankiskasPirmas
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        private List<DataObject> data;
        protected void Page_Load(object sender, EventArgs e)
        {
            data = (List<DataObject>)Session["data"];
            if (data == null)
                data = new List<DataObject>();
            if (DropDownList1.Items.Count == 0)
            {
                DropDownList1.Items.Add("-");
                for (int i = 14; i < 25; i++)
                {
                    DropDownList1.Items.Add(i.ToString());
                }
            }

            LoadSessionData();
        }

        protected void Button_Register_Click(object sender, EventArgs e)
        {
            if (TestData())
            {
                data.Add(new DataObject(TextBox1.Text, int.Parse(DropDownList1.Text), GetCheckBoxList(CheckBoxList1)));
                Session["data"] = data;
                Response.Redirect("WebForm1.aspx");
            }
            else
            {
                Counter.Text = "Kodas aptiko neatitikmenų su įvestimi";
            }
        }

        protected List<string> GetCheckBoxList(CheckBoxList checkBoxList)
        {
            List<string> list = new List<string>();

            for (int i = 0; i < checkBoxList.Items.Count; i++)
                if (checkBoxList.Items[i].Selected)
                    list.Add(checkBoxList.Items[i].Text);

            return list;
        }

        protected void LoadSessionData()
        {
            Counter.Text = $"Užsiregistravusių skaičius: {data.Count.ToString()}";
            TableRow header = new TableRow();

            header.Cells.Add(CreateCell(""));
            header.Cells.Add(CreateCell("Vardas"));
            header.Cells.Add(CreateCell("Amžius"));
            header.Cells.Add(CreateCell("Programavimo Kalbos"));

            DataTable.Rows.Add(header);

            for (int i = 0; i < data.Count; i++)
            {
                DataTable.Rows.Add(CreateRow(data[i], i + 1));
            }

            if (data.Count > 0)
                CreateResetButton();


        }

        protected TableRow CreateRow(DataObject dataObject, int number)
        {
            TableRow row = new TableRow();
            row.Cells.Add(CreateCell(number.ToString()));
            row.Cells.Add(CreateCell(dataObject.Name.ToString()));
            row.Cells.Add(CreateCell(dataObject.Age.ToString()));
            row.Cells.Add(CreateCell(dataObject.GetLanguagesString()));
            return row;
        }

        protected TableCell CreateCell(string text)
        {
            TableCell cell = new TableCell();
            cell.Text = text;
            return cell;
        }

        protected void CreateResetButton()
        {
            Button resetButton = new Button();
            resetButton.Text = "Trinti duomenis";
            resetButton.CausesValidation = false;
            resetButton.Click += (s, e) =>
            {
                data.Clear();
                Response.Redirect("WebForm1.aspx");
            };
                form1.Controls.Add(resetButton);
        }

        protected void DeleteInputButton_Click(object sender, EventArgs e)
        {
            TextBox1.Text = "";
            DropDownList1.SelectedIndex = 0;
            for (int i = 0; i < CheckBoxList1.Items.Count; i++)
            {
                CheckBoxList1.Items[i].Selected = false;
            }
        }

        /// <summary>
        /// Tests if the input data is valid
        /// </summary>
        /// <returns>True if the data is Valid, False if the data is invalid</returns>
        protected bool TestData()
        {
            if (TextBox1.Text.Trim() == "")
                return false;

            int age;
            if (int.TryParse(DropDownList1.SelectedValue, out age))
            {
                if (age < 14 || age >= 25)
                    return false;
            }
            else
                return false;

            return true;
        }
    }
}