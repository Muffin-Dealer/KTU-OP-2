using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Lab02
{
    public partial class Lab01Form : System.Web.UI.Page
    { 
        private string taxDataInput = @"App_Data/U16a.txt";
        private string citizenDataInput = @"App_Data/U16b.txt";
        private string outputDataPath = @"App_Data/U16result.txt";
        protected void Page_Load(object sender, EventArgs e)
        {
            CitizenTaxData citizenTaxData = null;
            Tax taxInfo = null;
            if (File.Exists(Server.MapPath(taxDataInput)))
            {
                taxInfo = InOutUtils.ReadTaxData(Server.MapPath(taxDataInput));
                InOutUtils.WriteTaxData(Server.MapPath(outputDataPath), taxInfo, "Initial Tax Company Data:");
                FillTaxDataTable(taxInfo, InitTaxTable);
            }
            else
            {
                InitTaxLabel.Text = "";
            }

            if (File.Exists(Server.MapPath(citizenDataInput)))
            {
                citizenTaxData = InOutUtils.ReadCitizenTaxData(Server.MapPath(citizenDataInput));
                InOutUtils.WriteCitizenTaxData(Server.MapPath(outputDataPath), citizenTaxData, "Initial Citizen Tax Data:");
                FillCitizenTaxDataTable(citizenTaxData, InitCitizenTable);
            }
            else
            {
                InitCitizenLabel.Text = "";
            }

            if (citizenTaxData != null && taxInfo != null)
            {
                // Reads Initial Data and Outputs the Initial Data To WebForm and to text
                InOutUtils.CreateFile(Server.MapPath(outputDataPath));


                CitizenCalculations(taxInfo, citizenTaxData);
                CheckFiltered(taxInfo, citizenTaxData);
            }
            else
            {
                HeaderLabel.Text = "Plaese Upload remaining data files";
                CalculationsPanel.Visible = false;
            }
        }

        protected void CitizenCalculations(Tax taxInfo, CitizenTaxData citizenTaxData)
        {
            Citizen citizensAverage = citizenTaxData.CreateCitizenData(taxInfo); // For Above Average
            InOutUtils.WriteCitizenData(Server.MapPath(outputDataPath), citizensAverage, "Tax Sum of all citizens:");
            
            citizensAverage.Sort();
            InOutUtils.WriteCitizenData(Server.MapPath(outputDataPath), citizensAverage, "Tax Sum of all citizens SORTED A-Z:");
            FillCitizenTable(citizensAverage, CitizenTaxTable);

            double sum = citizensAverage.Sum();
            double average = citizensAverage.GetAverage();
            InOutUtils.WriteHeader(Server.MapPath(outputDataPath), $"All Citizen TOTAL Tax Sum: {sum:f}");
            InOutUtils.WriteHeader(Server.MapPath(outputDataPath), $"Average Tax Sum: {average:f}");
            AverageTax.Text = $"Average tax per citizen: {average}";
            TotalTaxSum.Text = $"Total tax sum: {sum}";
            
            citizensAverage.RemoveUnderAverage();
            InOutUtils.WriteCitizenData(Server.MapPath(outputDataPath), citizensAverage, "Citizens who paid above average:");
            FillCitizenTable(citizensAverage, AboveAverageTable);
        }
        protected void CheckFiltered(Tax taxInfo, CitizenTaxData citizenTaxData)
        {
            if (Session["TaxCode"] != null && Session["Month"] != null)
            {
                Citizen citizensFiltered = citizenTaxData.CreateCitizenData(taxInfo); // For Filter
                citizensFiltered.Sort();
                citizensFiltered.RemoveWhoDidNotPayTax(Session["TaxCode"].ToString(), Session["Month"].ToString(), citizenTaxData);
                InOutUtils.WriteCitizenData(Server.MapPath(outputDataPath), citizensFiltered, $"Citizens who paid TaxCode: \"{Session["TaxCode"]}\" on Month: \"{Session["Month"]}\"");
                FillCitizenTable(citizensFiltered, FilterTable);
            }
            else
            {
                FilterData.Text = "No Filter provided";
            }

            Session["TaxCode"] = null;
            Session["Month"] = null;
        }

        protected void FillCitizenTaxDataTable(CitizenTaxData data, Table table)
        {
            TableRow headerRow = new TableRow();
            headerRow.Cells.Add(TaskUtils.CreateCell("Last Name"));
            headerRow.Cells.Add(TaskUtils.CreateCell("First Name"));
            headerRow.Cells.Add(TaskUtils.CreateCell("Address"));
            headerRow.Cells.Add(TaskUtils.CreateCell("Month"));
            headerRow.Cells.Add(TaskUtils.CreateCell("Tax Code"));
            headerRow.Cells.Add(TaskUtils.CreateCell("Amount"));
            table.Rows.Add(headerRow);
            for (int i = 0; i < data.Count; i++)
            {
                table.Rows.Add(data.GetRow(i));
            }
        }

        protected void FillTaxDataTable(Tax data, Table table)
        {
            TableRow headerRow = new TableRow();
            headerRow.Cells.Add(TaskUtils.CreateCell("Tax Code"));
            headerRow.Cells.Add(TaskUtils.CreateCell("Tax Company Name:"));
            headerRow.Cells.Add(TaskUtils.CreateCell("Price:"));;
            table.Rows.Add(headerRow);
            for (int i = 0; i < data.Count; i++)
            {
                table.Rows.Add(data.GetRow(i));
            }
        }

        protected void FillCitizenTable(Citizen data, Table table)
        {
            TableRow headerRow = new TableRow();
            headerRow.Cells.Add(TaskUtils.CreateCell("Last Name"));
            headerRow.Cells.Add(TaskUtils.CreateCell("First Name"));
            headerRow.Cells.Add(TaskUtils.CreateCell("Address"));
            headerRow.Cells.Add(TaskUtils.CreateCell("Tax Sum"));
            table.Rows.Add(headerRow);
            for (int i = 0; i < data.Count; i++)
            {
                table.Rows.Add(data.GetRow(i));
            }
        }

        protected void ButtonFilter_Click(object sender, EventArgs e)
        {
            string taxCode = TaxCodeTextBox.Text;
            string month = TaxMonthTextBox.Text;
            if (month != "" && taxCode != null)
            {
                Session["TaxCode"] = TaxCodeTextBox.Text;
                Session["Month"] = TaxMonthTextBox.Text;
            }
            Response.Redirect("Lab01Form.aspx");
        }

        protected void DataButton_Click(object sender, EventArgs e)
        {
            if(FileUpload1.HasFile)
            {
                FileUpload1.SaveAs(Server.MapPath(taxDataInput));
            }
            if (FileUpload2.HasFile)
            {
                FileUpload2.SaveAs(Server.MapPath(citizenDataInput));
            }
            Response.Redirect("Lab01Form.aspx");
        }
    }
}